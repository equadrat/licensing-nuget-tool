using e2.Licensing.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This interface describes a processor to parse the command line.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandLineOptionsProcessor
    {
        /// <summary>
        /// Handles the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// <c>true</c> if handled the arguments; <c>false</c> otherwise.
        /// </returns>
        Task<ICommandLineHandlerResult> HandleAsync(IEnumerable<string> args);
    }
}