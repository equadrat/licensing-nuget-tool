using e2.Framework.Components;
using e2.Licensing.Fluent;
using System;
using System.Diagnostics.Contracts;

namespace e2.Licensing.Helpers
{
    /// <summary>
    /// This class provides helper methods.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public static class LicensingModuleSelectorExtensions
    {
        /// <summary>
        /// Selects Licensing bootstrapper modules.
        /// </summary>
        /// <param name="moduleRegistry">The module registry.</param>
        /// <returns>
        /// The next step of the fluent API.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">moduleRegistry</exception>
        [Pure]
        public static LicensingModuleSelector Licensing(this ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));
            return new LicensingModuleSelector(moduleRegistry);
        }
    }
}