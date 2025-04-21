using GF.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public struct Direction : IReciprocal<DirectionType>
    {
        public DirectionType Value { get; }

        public DirectionType ReciprocalValue { get; }

        public Direction (DirectionType directionType)
        {
            this.Value = directionType;
            this.ReciprocalValue = CalculateReciprocalValue(directionType);
        }

        public static DirectionType CalculateReciprocalValue (DirectionType directionType)
        {
            if (directionType == (DirectionType.Down | DirectionType.Up))
            {
                return directionType;
            }

            return directionType == DirectionType.Down ? DirectionType.Up : DirectionType.Down;
        }
    }
}
