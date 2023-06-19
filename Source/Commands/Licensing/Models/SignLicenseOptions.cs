using CommandLine;
using e2.Licensing.MemberTemplates;
using System;

#pragma warning disable CS8618

namespace e2.Licensing.Models
{
    /// <summary>
    /// This class represents the options to sign a license.
    /// </summary>
    [Verb("s", aliases: new[] { "sign" }, HelpText = "Signs a license.")]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public class SignLicenseOptions: ITemplateFileProperty,
                                     ILicenseFileProperty,
                                     IFormatProperty,
                                     IPrivateKeyFileProperty,
                                     IPrivateKeyPasswordProperty
    {
        /// <inheritdoc />
        [Option('t', "template", HelpText = "The template file.", Required = true)]
        public string TemplateFile {get; set;}

        /// <inheritdoc />
        [Option('l', "license", HelpText = "The license file.", Required = true)]
        public string LicenseFile {get; set;}

        /// <inheritdoc />
        [Option('f', "format", HelpText = "The format.", Required = false, Default = "XML")]
        public string Format {get; set;}

        /// <inheritdoc />
        [Option("private", HelpText = "The private key file.", Required = true)]
        public string PrivateKeyFile {get; set;}

        /// <inheritdoc />
        [Option("password", HelpText = "The password for the private key file.", Required = true)]
        public string PrivateKeyPassword {get; set;}
    }
}