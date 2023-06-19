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
    public static class CommandsBootstrapperModuleExtensions
    {
        /// <summary>
        /// Registers the commands bootstrapper module.
        /// </summary>
        /// <param name="moduleSelector">The module selector.</param>
        /// <returns>
        /// The module registry.
        /// </returns>
        public static ICoreBootstrapperModuleRegistry RegisterCommandsModule(this LicensingModuleSelector moduleSelector)
        {
            return moduleSelector.ModuleRegistry.RegisterModule<CommandsBootstrapperModule, ICommandsBootstrapperModule>();
        }
    }
}