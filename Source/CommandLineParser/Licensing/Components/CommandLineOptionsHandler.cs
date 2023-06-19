using e2.Licensing.Models;
using System;
using System.Threading.Tasks;

namespace e2.Licensing.Components
{
    /// <summary>
    /// This class represents a handler of the command line.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options.</typeparam>
    [CLSCompliant(ProductAssemblyInfo.ClsCompliant)]
    public abstract class CommandLineOptionsHandler<TOptions>: ICommandLineOptionsHandler<TOptions>
        where TOptions: class
    {
        /// <summary>
        /// The type of options.
        /// </summary>
        private static readonly Type _typeOfOptions;

        /// <summary>
        /// Initializes static members of the <see cref="CommandLineOptionsHandler{TOptions}" /> class.
        /// </summary>
        static CommandLineOptionsHandler()
        {
            _typeOfOptions = typeof(TOptions);
        }

        /// <inheritdoc />
        public Type TypeOfOptions => _typeOfOptions;

        /// <inheritdoc />
        public abstract Task<ICommandLineHandlerResult> Handle(TOptions options);

        /// <inheritdoc />
        async Task<ICommandLineHandlerResult> ICommandLineOptionsHandler.HandleObject(object options)
        {
            return await this.Handle((TOptions)options);
        }
    }
}