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
    public static class CliBootstrapperModuleExtensions
    {
        /// <summary>
        /// Registers the CLI bootstrapper module.
        /// </summary>
        /// <param name="moduleSelector">The module selector.</param>
        /// <returns>
        /// The module registry.
        /// </returns>
        public static ICoreBootstrapperModuleRegistry RegisterLicenseCliModule(this LicensingModuleSelector moduleSelector)
        {
            return moduleSelector.ModuleRegistry.RegisterModule<CliBootstrapperModule, ICliBootstrapperModule>();
        }
    }
}