using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="PublicKeyFile" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface IPublicKeyFileProperty
    {
        /// <summary>
        /// Gets or sets the public key file.
        /// </summary>
        /// <value>
        /// The public key file.
        /// </value>
        string PublicKeyFile {get; set;}
    }
}