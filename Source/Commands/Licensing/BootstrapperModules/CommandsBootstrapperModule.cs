using e2.Framework.Attributes;
using e2.Framework.BootstrapperModules;
using e2.Framework.Components;
using e2.Framework.Delegates;
using e2.Framework.Helpers;
using e2.Framework.MemberTemplates;
using e2.Framework.Models;
using e2.Licensing.Components;
using e2.Licensing.Helpers;
using System;
using System.Collections.Generic;

namespace e2.Licensing.BootstrapperModules
{
    /// <summary>
    /// This class represents the bootstrapper module of the commands.
    /// </summary>
    [CoreImplicitBootstrapperModuleInterface(typeof(ICommandsBootstrapperModule))]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public class CommandsBootstrapperModule: CoreBootstrapperModule,
                                                    ICommandsBootstrapperModule
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterLicensingModule()
                          .CoreFramework().RegisterSerializerModule()
                          .Licensing().RegisterCommandLineParserModule();
        }

        /// <inheritdoc />
        protected override IEnumerable<procBootstrapperRegister> GetRegisterMethods()
        {
            yield return this.RegisterCommandLineOptionsHandlers;
            yield return this.RegisterLicenseModel;
        }

        /// <inheritdoc />
        protected override IEnumerable<procBootstrapperInitialize> GetInitializeMethods()
        {
            yield return this.InitSerializerProvider;
        }

        /// <inheritdoc />
        protected override void SetupDependencies(ICoreBootstrapperDependencyTracker dependencyTracker)
        {
            if (dependencyTracker == null) throw new ArgumentNullException(nameof(dependencyTracker));

            dependencyTracker.Call(this.RegisterCommandLineOptionsHandlers).Before<ICommandLineParserBootstrapperModule>(x => x.RegisterCommandLineOptionsHandlers);
            dependencyTracker.Call(this.InitSerializerProvider).After<ICoreFrameworkLicensingBootstrapperModule>(x => x.InitSerializerProvider);
        }

        /// <inheritdoc />
        public virtual void RegisterCommandLineOptionsHandlers(ICoreIOCRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            registry.TryRegister<NewLicenseKeyPairHandler>()?.AsSingleton();
            registry.TryRegister<NewTemplateHandler>()?.AsSingleton();
            registry.TryRegister<SignLicenseHandler>()?.AsSingleton();
            registry.TryRegister<ValidateLicenseHandler>()?.AsSingleton();
        }

        /// <inheritdoc />
        public virtual void RegisterLicenseModel(ICoreIOCRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            registry.TryRegister<ICoreLicense>()?.AsInstancePerCallOf(() => new CoreLicense());
        }

        /// <inheritdoc />
        public virtual void InitSerializerProvider(ICoreIOCFactory factory, ICorePushOne<IDisposable> lifetimeObjects)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (lifetimeObjects == null) throw new ArgumentNullException(nameof(lifetimeObjects));

            var serializerProvider = factory.GetInstanceOf<ICoreSerializerProvider>();

            foreach (var serializerFormat in serializerProvider.Formats)
            {
                if (serializerProvider.HasSerializer<ICoreLicense>(serializerFormat)) continue;

                var serializer = serializerFormat.CreateSerializer<ICoreLicense, CoreLicense>();
                serializerProvider.AddSerializer(serializer);
            }
        }
    }
}