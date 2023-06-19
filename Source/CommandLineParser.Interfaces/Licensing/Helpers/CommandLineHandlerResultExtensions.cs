using e2.Framework;
using e2.Licensing.Models;
using System;
using System.Diagnostics.Contracts;

namespace e2.Licensing.Helpers
{
    /// <summary>
    /// This class provides helper methods.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public static class CommandLineHandlerResultExtensions
    {
        /// <summary>
        /// Sets the success.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="_">unused</param>
        /// <param name="message">The message.</param>
        /// <param name="exitCode">The exit code.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">result</exception>
        [Pure]
        public static ICommandLineHandlerResult SetSuccess(this ICommandLineHandlerResult result, NCObject? _ = null, string? message = null, int? exitCode = null)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            result.Success = true;
            result.Message = message;
            result.ExitCode = exitCode;

            return result;
        }

        /// <summary>
        /// Sets the fail.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="_">unused</param>
        /// <param name="message">The message.</param>
        /// <param name="exitCode">The exit code.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">result</exception>
        [Pure]
        public static ICommandLineHandlerResult SetFail(this ICommandLineHandlerResult result, NCObject? _ = null, string? message = null, int? exitCode = null)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            result.Success = false;
            result.Message = message;
            result.ExitCode = exitCode;

            return result;
        }
    }
}