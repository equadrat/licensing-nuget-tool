using e2.Framework.Components;
using e2.Licensing.Helpers;
using e2.Licensing.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents the handler to generate a new pair of keys.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewLicenseKeyPairHandler: CommandLineOptionsHandler<NewLicenseKeyPairOptions>
    {
        /// <summary>
        /// The license operator.
        /// </summary>
        private readonly ICoreLicenseOperator _licenseOperator;

        /// <summary>
        /// The result instance factory.
        /// </summary>
        private readonly ICoreInstanceFactory<ICommandLineHandlerResult> _resultInstanceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewLicenseKeyPairHandler" /> class.
        /// </summary>
        /// <param name="licenseOperator">The license operator.</param>
        /// <param name="resultInstanceFactory">The result instance factory.</param>
        /// <exception cref="ArgumentNullException">
        /// licenseOperator
        /// or
        /// resultInstanceFactory
        /// </exception>
        public NewLicenseKeyPairHandler(ICoreLicenseOperator licenseOperator, ICoreInstanceFactory<ICommandLineHandlerResult> resultInstanceFactory)
        {
            if (licenseOperator == null) throw new ArgumentNullException(nameof(licenseOperator));
            if (resultInstanceFactory == null) throw new ArgumentNullException(nameof(resultInstanceFactory));

            this._licenseOperator = licenseOperator;
            this._resultInstanceFactory = resultInstanceFactory;
        }

        /// <inheritdoc />
        public override Task<ICommandLineHandlerResult> Handle(NewLicenseKeyPairOptions options)
        {
            var factory = this._licenseOperator.CreateFactory();

            using (var fileStream = new FileStream(options.PrivateKeyFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                factory.SavePrivateKey(fileStream, options.PrivateKeyPassword);

                fileStream.Flush();
                fileStream.Close();

                Console.WriteLine($"PrivateKey file: {fileStream.Name}");
            }

            using (var fileStream = new FileStream(options.PublicKeyFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                factory.SavePublicKey(fileStream);

                fileStream.Flush();
                fileStream.Close();

                Console.WriteLine($"PublicKey file: {fileStream.Name}");
            }

            return Task.FromResult(this._resultInstanceFactory.CreateInstance().SetSuccess());
        }
    }
}