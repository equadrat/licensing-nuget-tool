using e2.Framework.Components;
using System;

namespace e2.Licensing.Fluent
{
    /// <summary>
    /// This structure represents the selector for Licensing bootstrapper modules.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public readonly struct LicensingModuleSelector
    {
        /// <summary>
        /// Gets the module registry.
        /// </summary>
        /// <value>
        /// The module registry.
        /// </value>
        public ICoreBootstrapperModuleRegistry ModuleRegistry {get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensingModuleSelector" /> struct.
        /// </summary>
        /// <param name="moduleRegistry">The module registry.</param>
        /// <exception cref="System.ArgumentNullException">moduleRegistry</exception>
        public LicensingModuleSelector(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            this.ModuleRegistry = moduleRegistry;
        }
    }
}