using e2.Framework.Components;
using e2.Framework.Helpers;
using e2.Licensing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents the unit test for <see cref="NewLicenseKeyPairHandler" />.
    /// </summary>
    [TestClass]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewLicenseKeyPairHandlerUnitTest: CommandLineOptionsHandlerUnitTestTemplate
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterLicensingModule()
                          .Licensing().RegisterCommandsModule();

            base.RegisterDependencyModules(moduleRegistry);
        }

        /// <summary>
        /// Tests that <see cref="NewLicenseKeyPairHandler.Handle" /> works as expected.
        /// </summary>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        [TestMethod]
        public async Task Handle_01()
        {
            static async Task Test(ICommandLineOptionsProcessor processor, string path)
            {
                const string commandLine = "k --public public.key --private private.key --password \"MyPassword\"";

                var result = await processor.HandleAsync(commandLine);
                Assert.IsTrue(result.Success);

                Assert.IsTrue(File.Exists(Path.Combine(path, "public.key")));
                Assert.IsTrue(File.Exists(Path.Combine(path, "private.key")));
            }

            await this.RunTestAsync(Test);
        }
    }
}