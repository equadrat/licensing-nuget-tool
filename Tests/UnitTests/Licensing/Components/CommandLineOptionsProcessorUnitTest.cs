using e2.Framework;
using e2.Framework.Components;
using e2.Framework.Helpers;
using e2.Licensing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents the unit test for <see cref="CommandLineOptionsProcessor" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class CommandLineOptionsProcessorUnitTest: MSTestTemplate
    {
        /// <inheritdoc />
        protected override void RegisterDependencyModules(ICoreBootstrapperModuleRegistry moduleRegistry)
        {
            if (moduleRegistry == null) throw new ArgumentNullException(nameof(moduleRegistry));

            moduleRegistry.CoreFramework().RegisterLicensingModule()
                          .Licensing().RegisterCommandLineParserModule()
                          .Licensing().RegisterCommandsModule();

            base.RegisterDependencyModules(moduleRegistry);
        }

        /// <summary>
        /// Creates an instance.
        /// </summary>
        /// <param name="_">unused</param>
        /// <param name="inconclusive"><c>true</c> to let the test fail as inconclusive.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        [Pure]
        // ReSharper disable once UnusedParameter.Local
        private ICommandLineOptionsProcessor CreateInstance(NCObject? _ = null, bool inconclusive = true)
        {
            try
            {
                return this.Factory.GetInstanceOf<ICommandLineOptionsProcessor>();
            }
            catch
            {
                if (inconclusive) Assert.Inconclusive(nameof(this.Init_01));
                throw;
            }
        }

        /// <summary>
        /// This is a dummy unit test.
        /// </summary>
        [TestMethod]
        public void Init_01()
        {
            var instance = this.CreateInstance(inconclusive: false);
            Assert.IsInstanceOfType<CommandLineOptionsProcessor>(instance);
        }

        /// <summary>
        /// Tests that <see cref="CommandLineOptionsProcessor.HandleAsync" /> supports the help command.
        /// </summary>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        [TestMethod]
        public async Task HandleAsync_01()
        {
            var instance = this.CreateInstance(inconclusive: false);
            var result = await instance.HandleAsync(new[] { "--help" }).ConfigureAwait(false);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.ExitCode);
        }

        /// <summary>
        /// Tests that <see cref="CommandLineOptionsProcessor.HandleAsync" /> supports the help command.
        /// </summary>
        /// <returns>
        /// The state of the asynchronous process.
        /// </returns>
        [TestMethod]
        public async Task HandleAsync_02()
        {
            var instance = this.CreateInstance(inconclusive: false);
            var result = await instance.HandleAsync(new[] { "--version" }).ConfigureAwait(false);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.ExitCode);
        }
    }
}