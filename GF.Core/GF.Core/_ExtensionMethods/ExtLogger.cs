using Amazon.Runtime.Internal.Util;
using GF.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static GF.Core.Helpers;
using static System.Collections.Specialized.BitVector32;

namespace GF.Core
{
    internal static class ExtLogger
    {
        /// <summary>
        /// Returns a new <see cref="Serilog.ILogger"/> with the method or class context set
        /// </summary>
        /// <typeparam name="T">The class being logged</typeparam>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="invokingMethodName">The invoking method name</param>
        /// <param name="callingMethod">The calling method name. Do not pass.</param>
        /// <returns></returns>
        internal static Serilog.ILogger BuildMethodContext<T>(this Serilog.ILogger logger, string? invokingMethodName, [CallerMemberName] string? callingMethod = null)
        {
            return logger.ForContext<T>().ForContext("methodName", callingMethod).ForContext("invokingMethodName", invokingMethodName);
        }

        /// <summary>
        /// Logs a method argument to the logger
        /// </summary>
        /// <typeparam name="T">The argument type</typeparam>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="argumentName">The argument name</param>
        /// <param name="value">The argument value</param>
        internal static void LogMethodArgument<T>(this Serilog.ILogger logger, string argumentName, T value, [CallerMemberName] string? callingMethod = null)
        {
            //validate arguments
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(argumentName), callingMethod), "You must supply the argment name");
            }

            logger.Debug<string, T>("Argument {ArgumentName} value: {Value}", argumentName, value);
        }

        /// <summary>
        /// Logs the duration of a <see cref="Stopwatch"/>
        /// </summary>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="stopwatch">The <see cref="Stopwatch"/> to log</param>
        /// <remarks>If the <paramref name="stopwatch"/> is not running this will be logged accordingly</remarks>
        internal static void LogDuration(this Serilog.ILogger logger, Stopwatch stopwatch, [CallerMemberName] string? callingMethod = null)
        {
            if (stopwatch == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(stopwatch), callingMethod), "You must supply the stop watch");
            }

            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                logger.Debug<long>("Duration: {Duration}ms", stopwatch.ElapsedMilliseconds);
            }
            else
            {
                logger.Debug<string>("Duration: {Duration}ms", "Stopwatch Not Running");
            }

            
        }
    }
}
