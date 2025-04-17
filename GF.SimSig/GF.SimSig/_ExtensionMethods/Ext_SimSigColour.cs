using FluentValidation;
using GF.Common;
using MongoDB.Driver.Core.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    internal static class Ext_SimSigColour
    {
        /// <summary>
        /// Converts a <see cref="SimSigColour"/> to hex
        /// </summary>
        /// <param name="colour">The <see cref="SimSigColour"/> to convert</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Thrown if conversion is not implemented</exception>
        public static string GetHex(this SimSigColour colour, [CallerMemberName] string? callingMethod = null)
        {
            switch (colour)
            {
                case SimSigColour.White:
                    return "#FFFFFF";
                case SimSigColour.Grey:
                    return "#888888";
                case SimSigColour.Red:
                    return "#FF0000";
                case SimSigColour.Yellow:
                    return "#FFFF00";
                case SimSigColour.Green:
                    return "#00C000";
                case SimSigColour.Orange:
                    return "#FF8000";
                case SimSigColour.Cyan:
                    return "#00FFFF";
                case SimSigColour.Blue:
                    return "#00A0FF";
                case SimSigColour.Black:
                    return "#000000";
                default:
                    throw new NotImplementedException($"The colour {Enum.GetName(typeof(SimSigColour), colour)} is not handled in the {nameof(GF.SimSig.Ext_SimSigColour.GetHex)} method.");
            }
        }
    }
}
