using System;

namespace e2.Licensing.Models
{
    /// <summary>
    /// This class represents the result of a command line handler.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class CommandLineHandlerResult: ICommandLineHandlerResult
    {
        /// <inheritdoc />
        public bool Success {get; set;}

        /// <inheritdoc />
        public string? Message {get; set;}

        /// <inheritdoc />
        public int? ExitCode {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineHandlerResult" /> class.
        /// </summary>
        public CommandLineHandlerResult()
        {
            this.Success = true;
            this.Message = null;
            this.ExitCode = null;
        }
    }
}