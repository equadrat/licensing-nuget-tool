using System;

namespace e2.Licensing.MemberTemplates
{
    /// <summary>
    /// This interface describes the <see cref="TemplateFile" /> property.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ITemplateFileProperty
    {
        /// <summary>
        /// Gets or sets the template file.
        /// </summary>
        /// <value>
        /// The template file.
        /// </value>
        string TemplateFile {get; set;}
    }
}