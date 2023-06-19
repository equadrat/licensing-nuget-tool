using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

#pragma warning disable SA1600

#if !NETCOREAPP
// ReSharper disable once CheckNamespace
namespace Microsoft.VisualStudio.TestPlatform.Utilities
{
    /// <summary>
    /// This class provides fallback implementations for legacy platforms.
    /// </summary>
    internal static class CommandLineUtilities
    {
        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        [Pure]
        internal static bool SplitCommandLineIntoArguments(string commandLine, out string[] arguments)
        {
            var argv = CommandLineToArgvW(commandLine, out var argc);
            if (argv == IntPtr.Zero)
            {
                arguments = null!;
                return false;
            }

            try
            {
                arguments = new string[argc];
                for (var i = 0; i < arguments.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    arguments[i] = Marshal.PtrToStringUni(p)!;
                }

                return false;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}
#endif