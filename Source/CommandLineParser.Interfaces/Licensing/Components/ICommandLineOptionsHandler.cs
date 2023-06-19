using e2.Licensing.Models;
using System;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This interface describes a handler of the command line.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandLineOptionsHandler
    {
        /// <summary>
        /// Gets the type of the options.
        /// </summary>
        /// <value>
        /// The type of the options.
        /// </value>
        Type TypeOfOptions {get;}

        /// <summary>
        /// Handles the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        Task<ICommandLineHandlerResult> HandleObject(object options);
    }

    /// <summary>
    /// This interface describes a handler of the command line.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options.</typeparam>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandLineOptionsHandler<in TOptions>: ICommandLineOptionsHandler
        where TOptions: class
    {
        /// <summary>
        /// Handles the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        Task<ICommandLineHandlerResult> Handle(TOptions options);
    }
}