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
    /// This class represents the handler to validate a license.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class ValidateLicenseHandler: CommandLineOptionsHandler<ValidateLicenseOptions>
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
        /// Initializes a new instance of the <see cref="ValidateLicenseHandler" /> class.
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
        public ValidateLicenseHandler(ICoreLicenseOperator licenseOperator, ICoreSerializerProvider serializerProvider, ICoreInstanceFactory<ICommandLineHandlerResult> resultInstanceFactory)
        {
            if (licenseOperator == null) throw new ArgumentNullException(nameof(licenseOperator));
            if (serializerProvider == null) throw new ArgumentNullException(nameof(serializerProvider));
            if (resultInstanceFactory == null) throw new ArgumentNullException(nameof(resultInstanceFactory));

            this._licenseOperator = licenseOperator;
            this._serializerProvider = serializerProvider;
            this._resultInstanceFactory = resultInstanceFactory;
        }

        /// <inheritdoc />
        public override Task<ICommandLineHandlerResult> Handle(ValidateLicenseOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var serializer = this._serializerProvider.GetSerializer<ICoreLicense>(options.Format);

            // Restore the validator.
            ICoreLicenseValidator licenseValidator;
            using (var fileStream = new FileStream(options.PublicKeyFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Console.WriteLine($"PublicKey file: {fileStream.Name}");
                licenseValidator = this._licenseOperator.RestoreValidator(fileStream);

                fileStream.Close();
            }

            // Read the license.
            ICoreLicense license;
            using (var fileStream = new FileStream(options.LicenseFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Console.WriteLine($"License file: {fileStream.Name}");
                license = serializer.Deserialize(fileStream) ?? throw new CoreInvalidOperationException(this);

                fileStream.Close();
            }

            Console.WriteLine("Validating license...");
            var isValid = licenseValidator.HasValidSignature(license);
            Console.WriteLine($"License is {(isValid ? "valid" : "invalid")}.");

            var result = this._resultInstanceFactory.CreateInstance();
            result = isValid
                ? result.SetSuccess()
                : result.SetFail(message: nameof(ValidateLicenseOptions.LicenseFile), exitCode: -5);
            return Task.FromResult(result);
        }
    }
}