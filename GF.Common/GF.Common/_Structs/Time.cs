using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    public struct Time
    {
        private const byte secondsInMinute = 60;
        private const byte minutesInHour = 60;
        private const uint secondsInHour = 3600;

        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds; //0 = No seconds | 1 = 15 seconds | 2 = 30 seconds | 3 = 45 seconds

        public Time(uint seconds)
        {
            this._hours = Convert.ToByte(Math.Floor((double)seconds/secondsInHour));
            this._minutes = Convert.ToByte(Math.Floor((double)((seconds - (this._hours * secondsInHour)) / secondsInMinute)));
            this._seconds = GetSecondFlag(Convert.ToByte(seconds - ((this._hours * secondsInHour) + (this._minutes * secondsInMinute))));
        }

        public Time(byte hours, byte minutes, byte seconds)
        {
            this._hours = hours;
            this._minutes = minutes;
            this._seconds = GetSecondFlag(seconds);
        }

        public override string ToString()
        {
            return $"{this.HoursToString()}:{this.MinutesToString()}{this.SecondsToString()}";
        }

        public string SecondsToString()
        {
            switch (this._seconds)
            {
                case 1:
                    return ((char)188).ToString();
                case 2:
                    return ((char)189).ToString();
                case 3:
                    return ((char)190).ToString();
                default:
                    return "";
            }
        }

        private string HoursToString()
        {
            return $"{new String('0', (2 - this._hours.ToString().Length))}{this._hours.ToString()}";
        }

        private string MinutesToString()
        {
            return $"{new String('0', (2 - this._minutes.ToString().Length))}{this._minutes.ToString()}";
        }

        private static byte GetSecondFlag(byte seconds)
        {
            if (seconds >= 1 && seconds <=15)
            {
                return 1;
            }

            if (seconds >= 16 && seconds <= 30)
            {
                return 2;
            }

            if (seconds >= 31 && seconds <= 45)
            {
                return 3;
            }

            return 0;
        }

    }
}
