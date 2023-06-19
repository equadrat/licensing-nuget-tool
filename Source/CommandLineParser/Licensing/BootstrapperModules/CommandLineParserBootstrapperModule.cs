using e2.Framework.Attributes;
using e2.Framework.Components;
using e2.Framework.Delegates;
using e2.Framework.Helpers;
using e2.Licensing.Components;
using e2.Licensing.Models;
using System;
using System.Collections.Generic;

namespace e2.Licensing.BootstrapperModules
{
    /// <summary>
    /// This class represents the bootstrapper module of the command line parser.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    [CoreImplicitBootstrapperModuleInterface(typeof(ICommandLineParserBootstrapperModule))]
    public class CommandLineParserBootstrapperModule: CoreBootstrapperModule,
                                                      ICommandLineParserBootstrapperModule
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterReflectionHelperModule();

            base.RegisterDependencyModules(moduleRegistry);
        }

        /// <inheritdoc />
        protected override IEnumerable<procBootstrapperRegister> GetRegisterMethods()
        {
            yield return this.RegisterCommandLineOptionsProcessor;
            yield return this.RegisterCommandLineHandlerResult;
            yield return this.RegisterCommandLineOptionsHandlers;
        }

        /// <inheritdoc />
        public virtual void RegisterCommandLineOptionsProcessor(ICoreIOCRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            registry.TryRegister<ICommandLineOptionsProcessor>()?.AsSingletonOf<CommandLineOptionsProcessor>();
        }

        /// <inheritdoc />
        public virtual void RegisterCommandLineHandlerResult(ICoreIOCRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            registry.TryRegister<ICommandLineHandlerResult>()?.AsInstancePerCallOf<CommandLineHandlerResult>();
        }

        /// <inheritdoc />
        public virtual void RegisterCommandLineOptionsHandlers(ICoreIOCRegistry registry)
        {
        }
    }
}