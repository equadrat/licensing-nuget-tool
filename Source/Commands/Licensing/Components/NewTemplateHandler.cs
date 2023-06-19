using e2.Framework.Components;
using e2.Framework.Models;
using e2.Licensing.Helpers;
using e2.Licensing.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents the handler to generate a new template.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewTemplateHandler: CommandLineOptionsHandler<NewTemplateOptions>
    {
        /// <summary>
        /// The license factory.
        /// </summary>
        private readonly ICoreInstanceFactory<ICoreLicense> _licenseFactory;

        /// <summary>
        /// The serializer provider.
        /// </summary>
        private readonly ICoreSerializerProvider _serializerProvider;

        /// <summary>
        /// The result instance factory.
        /// </summary>
        private readonly ICoreInstanceFactory<ICommandLineHandlerResult> _resultInstanceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTemplateHandler" /> class.
        /// </summary>
        /// <param name="licenseFactory">The license factory.</param>
        /// <param name="serializerProvider">The serializer provider.</param>
        /// <param name="resultInstanceFactory">The result instance factory.</param>
        /// <exception cref="ArgumentNullException">
        /// licenseFactory
        /// or
        /// serializerProvider
        /// or
        /// resultInstanceFactory
        /// </exception>
        public NewTemplateHandler(ICoreInstanceFactory<ICoreLicense> licenseFactory, ICoreSerializerProvider serializerProvider, ICoreInstanceFactory<ICommandLineHandlerResult> resultInstanceFactory)
        {
            if (licenseFactory == null) throw new ArgumentNullException(nameof(licenseFactory));
            if (serializerProvider == null) throw new ArgumentNullException(nameof(serializerProvider));
            if (resultInstanceFactory == null) throw new ArgumentNullException(nameof(resultInstanceFactory));

            this._licenseFactory = licenseFactory;
            this._serializerProvider = serializerProvider;
            this._resultInstanceFactory = resultInstanceFactory;
        }

        /// <inheritdoc />
        public override Task<ICommandLineHandlerResult> Handle(NewTemplateOptions options)
        {
            var serializer = this._serializerProvider.GetSerializer<ICoreLicense>(options.Format);

            var license = this._licenseFactory.CreateInstance();
            license.AddAttribute("lic-attr", "Some license attribute");

            var customer = license.EnsureCustomer();
            customer.Id = "customer-id";
            customer.AddAttribute("cust-attr", "Some customer attribute");

            var product = license.AddProduct();
            product.Id = "product-id";
            product.AddAttribute("prod-attr", "Some product attribute");

            var feature = product.AddFeature();
            feature.Id = "feature-id";
            feature.AddAttribute("feat-attr", "Some feature attribute");

            using (var fileStream = new FileStream(options.TemplateFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                serializer.Serialize(license, fileStream);

                fileStream.Flush();
                fileStream.Close();

                Console.WriteLine($"License file: {fileStream.Name}");
            }

            return Task.FromResult(this._resultInstanceFactory.CreateInstance().SetSuccess());
        }
    }
}