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
    /// This class represents the unit test for <see cref="NewTemplateHandler" />.
    /// </summary>
    [TestClass]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class NewTemplateHandlerUnitTest: CommandLineOptionsHandlerUnitTestTemplate
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterLicensingModule()
                          .CoreFramework().RegisterJsonNewtonsoftSerializerModule()
                          .Licensing().RegisterCommandsModule();

            base.RegisterDependencyModules(moduleRegistry);
        }

        /// <summary>
        /// Tests that <see cref="NewLicenseKeyPairHandler.Handle" /> works as expected.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        [DataTestMethod]
        [DataRow("XML")]
        [DataRow("JSON")]
        public async Task Handle_01(string format)
        {
            async Task Test(ICommandLineOptionsProcessor processor, string path)
            {
                var commandLine = $"t --template template-{format}.txt --format \"{format}\"";

                var result = await processor.HandleAsync(commandLine);
                Assert.IsTrue(result.Success);

                Assert.IsTrue(File.Exists(Path.Combine(path, $"template-{format}.txt")));
            }

            await this.RunTestAsync(Test);
        }
    }
}