using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// A non track circuited section of track
    /// </summary>
    public class TrackNoCircuit : PanelItemBase, ISimSigPanelItem
    {
        public TrackNoCircuit(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, int length) : base (simSigID, xOffSet, yOffSet, SimSigColour.Grey)
        {
            int i = 0; //keeps track of the length iterator
            int x = 0; //keeps track of the x offset increment inside the loop
            int y = 0; //keeps track of the y offset increment inside the loop

            switch (orientation)
            {
                case SimSigOrientation.East:
                    base.AddConfig(new SimSigFontTokenConfig(String.Concat(Enumerable.Repeat("0x78", length)), this.Colour, 0, 0));
                    break;
                case SimSigOrientation.West:
                    base.AddConfig(new SimSigFontTokenConfig(String.Concat(Enumerable.Repeat("0x78", length)), this.Colour, 0, 0));
                    break;
                case SimSigOrientation.NorthWest:
                    while (i < length)
                    {
                        //first character
                        if (i == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x75"), this.Colour, i, i));
                            i++;

                            if (i == length)
                            {
                                break;
                            }
                        }

                        //odd numbered iterator 
                        if (i % 1 == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x74"), this.Colour, -(i - x), i - (i + y)));
                            i++;
                            y++;
                            x++;

                            if (i == length)
                            {
                                break;
                            }
                        }

                        //even numbered iterator 
                        if (i % 2 == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x75"), this.Colour, -(i / 2), -(i / 2)));
                            i++;

                            if (i == length)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case SimSigOrientation.NorthEast:
                    while (i < length)
                    {
                        //first character
                        if (i == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x76"), this.Colour, i, i));
                            i++;

                            if (i == length)
                            {
                                break;
                            }
                        }

                        //odd numbered iterator
                        if (i % 1 == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x77"), this.Colour, i - x, i - (i + y)));
                            i++;
                            y++;
                            x+=2;

                            if (i == length)
                            {
                                break;
                            }
                        }

                        //even numbered iterator
                        if (i % 2 == 0)
                        {
                            base.AddConfig(new SimSigFontTokenConfig(String.Concat("0x76"), this.Colour, -(i / 2), -(i/2)));
                            i++;

                            if (i == length)
                            {
                                break;
                            }
                        }
                    }
                    break;
            }          
        }
    }
}
