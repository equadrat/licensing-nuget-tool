using CommandLine;
using e2.Licensing.MemberTemplates;
using System;

#pragma warning disable CS8618

namespace e2.Licensing.Models
{
    /// <summary>
    /// This class represents the options to generate a new template.
    /// </summary>
    [Verb("t", aliases: new[] { "template" }, HelpText = "Generates a new template.")]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewTemplateOptions: ITemplateFileProperty,
                                            IFormatProperty
    {
        /// <inheritdoc />
        [Option('t', "template", HelpText = "The template file.", Required = true)]
        public string TemplateFile {get; set;}

        /// <inheritdoc />
        [Option('f', "format", HelpText = "The format.", Required = false, Default = "XML")]
        public string Format {get; set;}
    }
}