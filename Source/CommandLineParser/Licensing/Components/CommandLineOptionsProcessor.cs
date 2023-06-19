using CommandLine;
using e2.Framework.Components;
using e2.Framework.Enums;
using e2.Framework.Helpers;
using e2.Framework.Models;
using e2.Licensing.Helpers;
using e2.Licensing.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents a processor to parse the command line.
    /// </summary>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public sealed class CommandLineOptionsProcessor: ICommandLineOptionsProcessor
    {
        /// <summary>
        /// The factory.
        /// </summary>
        private readonly ICoreIOCFactory _factory;

        /// <summary>
        /// The reflection helper.
        /// </summary>
        private readonly ICoreReflectionHelper _reflectionHelper;

        /// <summary>
        /// The result instance factory.
        /// </summary>
        private readonly ICoreInstanceFactory<ICommandLineHandlerResult> _resultInstanceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineOptionsProcessor" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="reflectionHelper">The reflection helper.</param>
        /// <param name="resultInstanceFactory">The result instance factory.</param>
        /// <exception cref="ArgumentNullException">
        /// factory
        /// or
        /// reflectionHelper
        /// or
        /// resultInstanceFactory
        /// </exception>
        public CommandLineOptionsProcessor(ICoreIOCFactory factory, ICoreReflectionHelper reflectionHelper, ICoreInstanceFactory<ICommandLineHandlerResult> resultInstanceFactory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (reflectionHelper == null) throw new ArgumentNullException(nameof(reflectionHelper));
            if (resultInstanceFactory == null) throw new ArgumentNullException(nameof(resultInstanceFactory));

            this._factory = factory;
            this._reflectionHelper = reflectionHelper;
            this._resultInstanceFactory = resultInstanceFactory;
        }

        /// <inheritdoc />
        public async Task<ICommandLineHandlerResult> HandleAsync(IEnumerable<string> args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            var handlerByTypeOfObject = this.GetHandlers()
                                            .GroupBy(x => x.TypeOfOptions, x => x.HandlerRegistration)
                                            .ToDictionary(x => x.Key, x => x.First());

            var optionsTypes = handlerByTypeOfObject.Keys.ToArray();
            if (optionsTypes.Length == 0) return this._resultInstanceFactory.CreateInstance().SetFail(message: "No commands registered.", exitCode: -3);

            var parserResult = Parser.Default.ParseArguments(args, optionsTypes);
            if (parserResult.Tag == ParserResultType.NotParsed)
            {
                var errors = parserResult.Errors
                                         .Where(x => (x.Tag != ErrorType.HelpVerbRequestedError) && (x.Tag != ErrorType.VersionRequestedError))
                                         .Select(x => x.ToString()!)
                                         .AsReadOnly();

                var result = this._resultInstanceFactory.CreateInstance();
                return errors.Count == 0
                    ? result.SetSuccess()
                    : result.SetFail(message: string.Join(", ", errors), exitCode: -2);
            }

            var options = parserResult.Value;
            var handlerRegistration = handlerByTypeOfObject[parserResult.TypeInfo.Current];
            var handler = (ICommandLineOptionsHandler)this._factory.ResolveRegistration(handlerRegistration);

            try
            {
                return await handler.HandleObject(options);
            }
            catch (Exception ex)
            {
                return this._resultInstanceFactory.CreateInstance().SetFail(message: ex.Message, exitCode: -4);
            }
        }

        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <returns>
        /// The handlers.
        /// </returns>
        [Pure]
        private IEnumerable<(Type TypeOfOptions, ICoreIOCRegistration HandlerRegistration)> GetHandlers()
        {
            var filter = eCoreIOCMappingFilter.Singleton | eCoreIOCMappingFilter.InstancePerCall | (this._factory.IsScope ? eCoreIOCMappingFilter.InstancePerScope : 0);
            return this._factory
                       .GetRegistrationsOf<ICommandLineOptionsHandler>(filter)
                       .Select(x => (TypeOfOptions: this.GetTypeOfOptions(x.TypeOfInstance), Registration: x))
                       .Where(x => x.TypeOfOptions != null)!;
        }

        /// <summary>
        /// Gets the type of options.
        /// </summary>
        /// <param name="typeOfHandler">The type of handler.</param>
        /// <returns>
        /// The type of options or <c>null</c>.
        /// </returns>
        [Pure]
        private Type? GetTypeOfOptions(Type typeOfHandler)
        {
            var types = this._reflectionHelper.GetGenericTypeArguments(typeOfHandler, typeof(ICommandLineOptionsHandler<>));
            return types.Count != 0 ? types[0] : null;
        }
    }
}