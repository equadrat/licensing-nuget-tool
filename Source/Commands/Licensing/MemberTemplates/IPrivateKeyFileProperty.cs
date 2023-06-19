using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="PrivateKeyFile" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface IPrivateKeyFileProperty
    {
        /// <summary>
        /// Gets or sets the private key file.
        /// </summary>
        /// <value>
        /// The private key file.
        /// </value>
        string PrivateKeyFile {get; set;}
    }
}