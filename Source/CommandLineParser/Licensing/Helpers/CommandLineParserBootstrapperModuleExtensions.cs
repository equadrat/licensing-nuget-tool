using e2.Framework.Components;
using e2.Licensing.BootstrapperModules;
using e2.Licensing.Fluent;
using System;

namespace e2.Licensing.Helpers
{
    /// <summary>
    /// This class provides helper methods.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public static class CommandLineParserBootstrapperModuleExtensions
    {
        /// <summary>
        /// Registers the command line parser bootstrapper module.
        /// </summary>
        /// <param name="moduleSelector">The module selector.</param>
        /// <returns>
        /// The module selector.
        /// </returns>
        public static ICoreBootstrapperModuleRegistry RegisterCommandLineParserModule(this LicensingModuleSelector moduleSelector)
        {
            return moduleSelector.ModuleRegistry.RegisterModule<CommandLineParserBootstrapperModule, ICommandLineParserBootstrapperModule>();
        }
    }
}