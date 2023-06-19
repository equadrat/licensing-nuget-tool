using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="PrivateKeyPassword" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface IPrivateKeyPasswordProperty
    {
        /// <summary>
        /// Gets or sets the private key password.
        /// </summary>
        /// <value>
        /// The private key password.
        /// </value>
        string PrivateKeyPassword {get; set;}
    }
}