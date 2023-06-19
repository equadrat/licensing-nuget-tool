using e2.Framework.Components;
using e2.Framework.MemberTemplates;
using System;

namespace e2.Licensing.BootstrapperModules
{
    /// <summary>
    /// This interface describes the bootstrapper module of the commands.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public interface ICommandsBootstrapperModule
    {
        /// <summary>
        /// Registers the command line options handlers.
        /// </summary>
        /// <param name="registry">The registry.</param>
        void RegisterCommandLineOptionsHandlers(ICoreIOCRegistry registry);

        /// <summary>
        /// Registers the license model.
        /// </summary>
        /// <param name="registry">The registry.</param>
        void RegisterLicenseModel(ICoreIOCRegistry registry);

        /// <summary>
        /// Initializes the serializer provider.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="lifetimeObjects">The lifetime objects.</param>
        void InitSerializerProvider(ICoreIOCFactory factory, ICorePushOne<IDisposable> lifetimeObjects);
    }
}