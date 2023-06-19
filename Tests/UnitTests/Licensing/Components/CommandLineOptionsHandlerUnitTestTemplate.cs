using e2.Framework;
using e2.Framework.Components;
using e2.Framework.Helpers;
using e2.Framework.Models;
using e2.Licensing.Helpers;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents a template for a <see cref="ICommandLineOptionsHandler" /> unit test.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public abstract class CommandLineOptionsHandlerUnitTestTemplate: MSTestTemplate
    {
        /// <summary>
        /// Deletes the temporary folder.
        /// </summary>
        /// <param name="path">The path.</param>
        private static void DeleteTempFolder(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch
            {
                // Ignored.
            }
        }

        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterOwnerTokenFactoryModule()
                          .CoreFramework().RegisterRandomDataProviderModule()
                          .Licensing().RegisterCommandLineParserModule();

            base.RegisterDependencyModules(moduleRegistry);
        }

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">callback</exception>
        protected async Task RunTestAsync(Func<ICommandLineOptionsProcessor, string, Task> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            var commandLineOptionsProcessor = this.Factory.GetInstanceOf<ICommandLineOptionsProcessor>();
            using (this.CreateTempFolder().Deconstruct(out var path))
            {
                var currentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = path;

                try
                {
                    await callback.Invoke(commandLineOptionsProcessor, path);
                }
                finally
                {
                    Environment.CurrentDirectory = currentDirectory;
                }
            }
        }

        /// <summary>
        /// Creates a temporary folder.
        /// </summary>
        /// <returns>
        /// The token to delete the folder.
        /// </returns>
        [Pure]
        private ICoreOwnerToken<string> CreateTempFolder()
        {
            var randomDataProvider = this.Factory.GetInstanceOf<ICoreRandomDataProvider>();
            var tokenFactory = this.Factory.GetInstanceOf<ICoreOwnerTokenFactory>();

            var currentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location)!;
            var folderName = randomDataProvider.GenerateString(8, useLowerLetters: true);
            var path = Path.Combine(currentDirectory, "data", folderName);

            Directory.CreateDirectory(path);

            return tokenFactory.CreateRelayToken(path, DeleteTempFolder);
        }
    }
}