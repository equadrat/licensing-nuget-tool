using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="LicenseFile" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ILicenseFileProperty
    {
        /// <summary>
        /// Gets or sets the license file.
        /// </summary>
        /// <value>
        /// The license file.
        /// </value>
        string LicenseFile {get; set;}
    }
}