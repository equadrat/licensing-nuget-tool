using e2.Framework.Attributes;
using e2.Framework.Components;
using e2.Framework.Helpers;
using e2.Licensing.Helpers;
using System;

namespace e2.Licensing.BootstrapperModules
{
    /// <summary>
    /// This class represents the bootstrapper module of the CLI.
    /// </summary>
    [CoreImplicitBootstrapperModuleInterface(typeof(ICliBootstrapperModule))]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public class CliBootstrapperModule: CoreBootstrapperModule,
                                               ICliBootstrapperModule
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterBaseModule()
                          .CoreFramework().RegisterJsonNewtonsoftSerializerModule()
                          .Licensing().RegisterCommandLineParserModule()
                          .Licensing().RegisterCommandsModule();
        }
    }
}