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
    /// This class represents the unit test for <see cref="SignLicenseHandler" />.
    /// </summary>
    [TestClass]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class SignLicenseHandlerUnitTest: CommandLineOptionsHandlerUnitTestTemplate
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
                await NewLicenseKeyPair(processor, path).ConfigureAwait(false);
                await NewTemplate(processor, path).ConfigureAwait(false);
                await SignTemplate(processor, path).ConfigureAwait(false);
            }

            static async Task NewLicenseKeyPair(ICommandLineOptionsProcessor processor, string path)
            {
                const string commandLine = "k --public public.key --private private.key --password \"MyPassword\"";

                var result = await processor.HandleAsync(commandLine).ConfigureAwait(false);
                if (!result.Success || !File.Exists(Path.Combine(path, "public.key")) || !File.Exists(Path.Combine(path, "private.key"))) Assert.Inconclusive(nameof(NewLicenseKeyPairHandlerUnitTest));
            }

            async Task NewTemplate(ICommandLineOptionsProcessor processor, string path)
            {
                var commandLine = $"t --template template-{format}.txt --format \"{format}\"";

                var result = await processor.HandleAsync(commandLine).ConfigureAwait(false);
                if (!result.Success || !File.Exists(Path.Combine(path, $"template-{format}.txt"))) Assert.Inconclusive(nameof(NewTemplateHandlerUnitTest));
            }

            async Task SignTemplate(ICommandLineOptionsProcessor processor, string path)
            {
                var commandLine = $"s --template template-{format}.txt --license license-{format}.txt --format \"{format}\" --private private.key --password \"MyPassword\"";

                var result = await processor.HandleAsync(commandLine).ConfigureAwait(false);
                Assert.IsTrue(result.Success);

                Assert.IsTrue(File.Exists(Path.Combine(path, $"license-{format}.txt")));
            }

            await this.RunTestAsync(Test).ConfigureAwait(false);
        }
    }
}