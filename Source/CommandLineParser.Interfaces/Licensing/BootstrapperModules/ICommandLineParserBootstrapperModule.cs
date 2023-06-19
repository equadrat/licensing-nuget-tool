using e2.Framework.Components;
using System;

namespace e2.Licensing.BootstrapperModules
{
    /// <summary>
    /// This interface describes the bootstrapper module of the command line parser.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandLineParserBootstrapperModule
    {
        /// <summary>
        /// Registers the command line options processor.
        /// </summary>
        /// <param name="registry">The registry.</param>
        void RegisterCommandLineOptionsProcessor(ICoreIOCRegistry registry);

        /// <summary>
        /// Registers the command line handler result.
        /// </summary>
        /// <param name="registry">The registry.</param>
        void RegisterCommandLineHandlerResult(ICoreIOCRegistry registry);

        /// <summary>
        /// Registers the command line options handlers.
        /// </summary>
        /// <param name="registry">The registry.</param>
        void RegisterCommandLineOptionsHandlers(ICoreIOCRegistry registry);
    }
}