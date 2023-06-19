using e2.Framework.Components;
using e2.Licensing.Components;
using e2.Licensing.Helpers;
using System;
using System.Threading.Tasks;

namespace e2.Licensing
{
    /// <summary>
    /// This class provides the entry point of the application.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        [STAThread]
        public static async Task Main(string[] args)
        {
            var registry = new CoreIOCRegistry();
            var factory = registry.Factory;

            var bootstrapper = factory.CreateInstanceOf<ICoreBootstrapper>();
            bootstrapper.ModuleRegistry.Licensing().RegisterLicenseCliModule();

            int exitCode;

            using (bootstrapper.Startup(registry))
            {
                var processor = factory.GetInstanceOf<ICommandLineOptionsProcessor>();

                try
                {
                    var result = await processor.HandleAsync(args);
                    if (!string.IsNullOrEmpty(result.Message)) Console.WriteLine(result.Message);
                    exitCode = result.ExitCode ?? (result.Success ? 0 : -1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    exitCode = -1;
                }
            }

            Environment.Exit(exitCode);
        }
    }
}