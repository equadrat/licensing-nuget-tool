using e2.Framework.Components;
using e2.Framework.Exceptions;
using e2.Framework.Models;
using e2.Licensing.Helpers;
using e2.Licensing.Models;
using System;
using System.IO;
using System.Threading.Tasks;

#pragma warning disable CA1303

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents the handler to sign a license.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class SignLicenseHandler: CommandLineOptionsHandler<SignLicenseOptions>
    {
        /// <summary>
        /// The license operator.
        /// </summary>
        private readonly ICoreLicenseOperator _licenseOperator;

        /// <summary>
        /// The serializer provider.
        /// </summary>
        private readonly ICoreSerializerProvider _serializerProvider;

        /// <summary>
        /// The result instance factory.
        /// </summary>
        private readonly ICoreInstanceFactory<ICommandLineHandlerResult> _resultInstanceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignLicenseHandler" /> class.
        /// </summary>
        /// <param name="licenseOperator">The license operator.</param>
        /// <param name="serializerProvider">The serializer provider.</param>
        /// <param name="resultInstanceFactory">The result instance factory.</param>
        /// <exception cref="ArgumentNullException">
        /// licenseOperator
        /// or
        /// serializerProvider
        /// or
        /// resultInstanceFactory
        /// </exception>
        public SignLicenseHandler(ICoreLicenseOperator licenseOperator, ICoreSerializerProvider serializerProvider, ICoreInstanceFactory<ICommandLineHandlerResult> resultInstanceFactory)
        {
            if (licenseOperator == null) throw new ArgumentNullException(nameof(licenseOperator));
            if (serializerProvider == null) throw new ArgumentNullException(nameof(serializerProvider));
            if (resultInstanceFactory == null) throw new ArgumentNullException(nameof(resultInstanceFactory));

            this._licenseOperator = licenseOperator;
            this._serializerProvider = serializerProvider;
            this._resultInstanceFactory = resultInstanceFactory;
        }

        /// <inheritdoc />
        public override Task<ICommandLineHandlerResult> Handle(SignLicenseOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var serializer = this._serializerProvider.GetSerializer<ICoreLicense>(options.Format);

            // Read the license.
            ICoreLicense license;
            using (var fileStream = new FileStream(options.TemplateFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Console.WriteLine($"Template file: {fileStream.Name}");
                license = serializer.Deserialize(fileStream) ?? throw new CoreInvalidOperationException(this);

                fileStream.Close();
            }

            // Restore the factory.
            ICoreLicenseFactory licenseFactory;
            using (var fileStream = new FileStream(options.PrivateKeyFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var password = new CoreSecureArray<char>(options.PrivateKeyPassword.ToCharArray(), ownsArray: true))
            {
                Console.WriteLine($"PrivateKey file: {fileStream.Name}");
                licenseFactory = this._licenseOperator.RestoreFactory(fileStream, password.Value);

                fileStream.Close();
            }

            Console.WriteLine("Signing license...");
            licenseFactory.SignLicense(license);
            Console.WriteLine("License signed.");

            using (var fileStream = new FileStream(options.LicenseFile, FileMode.Create, FileAccess.Write, FileShare.Read))
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