using CommandLine;
using e2.Licensing.MemberTemplates;
using System;

#pragma warning disable CS8618

namespace e2.Licensing.Models
{
    /// <summary>
    /// This class represents the options to create a new pair of keys.
    /// </summary>
    [Verb("k", aliases: new[] { "keypair" }, HelpText = "Generates a new key-pair.")]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewLicenseKeyPairOptions: IPublicKeyFileProperty,
                                                  IPrivateKeyFileProperty,
                                                  IPrivateKeyPasswordProperty
    {
        /// <inheritdoc />
        [Option("public", HelpText = "The public key file.", Required = true)]
        public string PublicKeyFile {get; set;}

        /// <inheritdoc />
        [Option("private", HelpText = "The private key file.", Required = true)]
        public string PrivateKeyFile {get; set;}

        /// <inheritdoc />
        [Option("password", HelpText = "The password for the private key file.", Required = true)]
        public string PrivateKeyPassword {get; set;}
    }
}