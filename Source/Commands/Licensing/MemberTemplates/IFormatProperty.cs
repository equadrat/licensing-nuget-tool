using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="Format" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface IFormatProperty
    {
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        string Format {get; set;}
    }
}