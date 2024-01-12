using e2.Licensing.Components;
using e2.Licensing.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Threading.Tasks;

namespace e2.Licensing.Helpers
{
    /// <summary>
    /// This class provides helper methods.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public static class CommandLineOptionsProcessorExtensions
    {
        /// <summary>
        /// Handles the specified arguments.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="commandLine">The command line.</param>
        /// <returns>
        /// <c>true</c> if handled the arguments; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// processor
        /// or
        /// commandLine
        /// </exception>
        /// <exception cref="System.ArgumentException">commandLine</exception>
        public static async Task<ICommandLineHandlerResult> HandleAsync(this ICommandLineOptionsProcessor processor, string commandLine)
        {
            if (processor == null) throw new ArgumentNullException(nameof(processor));
            if (commandLine == null) throw new ArgumentNullException(nameof(commandLine));
            if (CommandLineUtilities.SplitCommandLineIntoArguments(commandLine, out var args)) throw new ArgumentException(nameof(commandLine));

            return await processor.HandleAsync(args).ConfigureAwait(false);
        }
    }
}