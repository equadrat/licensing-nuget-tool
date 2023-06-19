using System;

namespace e2.Licensing.Models
{
    /// <summary>
    /// This interface describes the result of a command line handler.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandLineHandlerResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation succeeded.
        /// </summary>
        /// <value>
        /// <c>true</c> if the operation succeeded; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// The default value is <c>true</c>.
        /// </remarks>
        bool Success {get; set;}

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string? Message {get; set;}

        /// <summary>
        /// Gets or sets the exit code.
        /// </summary>
        /// <value>
        /// The exit code.
        /// </value>
        int? ExitCode {get; set;}
    }
}