using CommandLine;
using e2.Licensing.MemberTemplates;
using System;

#pragma warning disable CS8618

namespace e2.Licensing.Models
{
    /// <summary>
    /// This class represents the options to validate a license.
    /// </summary>
    [Verb("v", aliases: new[] { "validate" }, HelpText = "Validates a license.")]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public class ValidateLicenseOptions: ILicenseFileProperty,
                                         IFormatProperty,
                                         IPublicKeyFileProperty
    {
        /// <inheritdoc />
        [Option('l', "license", HelpText = "The license file.", Required = true)]
        public string LicenseFile {get; set;}

        /// <inheritdoc />
        [Option('f', "format", HelpText = "The format.", Required = false, Default = "XML")]
        public string Format {get; set;}

        /// <inheritdoc />
        [Option("public", HelpText = "The public key file.", Required = true)]
        public string PublicKeyFile {get; set;}
    }
}