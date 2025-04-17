using GF.SimSig.Gateway;
using GF.SimSig.Gateway.Messages;
using GF.SimSig.Gateway.Stomp;
using GF.SimSig.Gateway.Stomp.Exceptions;
using GF.SimSig.Gateway.Stomp.IO;

namespace GF.SimSig.Bridge.Console
{
    internal class Program
    {       
        public static async Task Main()
        {
            try
            {
                /*
                using var gateway = new GatewayClient("localhost", 51515);
                await gateway.StartAsync(new List<GatewayTopic>() { GatewayTopic.Signalling, GatewayTopic.TrainMovements, GatewayTopic.HeartBeat }, new ConsoleWriterObserver());

                while (true)
                {
                    // Wait for messages to be received.
                    Thread.Sleep(1000);
                }
                */

                var character = new SimSigCharacter("0x70", "Signal Head Hollow", 0, 0, 0, 0, 60, 102, 195, 129, 129, 195, 102, 60, 0, 0, 0, 0);
                string text = character.RenderCharacterToSVGGroup("0x26");


                SimSigFont font = SimSigFont.BuildDefault();
                SimSigPanel panel = new("Royston");
                bool showTrackCircuitBreaks = true;

                int y = 0;

                //y = 0

                panel.AddPanelItem(new FreeTextPanelItem("ASHWELL&MORDENDOWN", 72, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("ASHWELL")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("XLITLINGTONTEXT", 100, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("LITLINGTON")}", SimSigColour.Grey, 0, 0)
                ])); 

                //y = 1
                y++;

                panel.AddPanelItem(new FreeTextPanelItem("Down line text", 2, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DOWN LINE")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new SignalHead("K959", 12, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K961", 26, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K963", 40, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K965", 54, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K965", 71, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K971", 83, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal | SimSigSignalHeadOption.Green, false));
                panel.AddPanelItem(new EmergencyReplacement("ER-K973", 96, y, SimSigOrientation.East, false));
                panel.AddPanelItem(new SignalHead("K973", 98, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Yellow | SimSigSignalHeadOption.IsAutoSignal, false));
                panel.AddPanelItem(new Platform("ASHWELL&MORDENDOWN", 74, y, 4));
                panel.AddPanelItem(new Platform("XLITLINGTOP", 105, y, 1));
                panel.AddPanelItem(new FreeTextPanelItem("AENDTOP", 118, 0,
                [
                    new ($"{SimSigFont.ConvertTextToHex("A")}", SimSigColour.Grey, 0, y)
                ]));
                //y= 2
                y++;

                panel.AddPanelItem(new TrackNoCircuit("DOWNENTRY",2, y, SimSigOrientation.East,4));
                var trackCircuit = new TrackWithCircuit("T3652", 6, y, SimSigOrientation.East, 10, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new (3,0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3751A", 16, y, SimSigOrientation.East, 15, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new (7, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3851A", 31, y, SimSigOrientation.East, 14, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(6, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3951A", 45, y, SimSigOrientation.East, 14, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(6, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4051A", 59, y, SimSigOrientation.East, 16, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(10, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4151A", 75, y, SimSigOrientation.East, 11, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(5, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4152A", 86, y, SimSigOrientation.East, 13, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, [new(8, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4251", 99, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4252", 103, y, SimSigOrientation.East, 3, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4351", 106, y, SimSigOrientation.East, 12, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new FreeTextPanelItem("AENDBREAK1", 118, 0,
                [
                    new ($"0x8A", SimSigColour.Grey, 0, y)
                ]));
                //y=3
                y++;

                panel.AddPanelItem(new FreeTextPanelItem("To Baldock text", 2, y,
                [
                    new($"0x8C0x61{SimSigFont.ConvertTextToHex(" TO BALDOCK")}", SimSigColour.Grey, 0, 0),
                ]));
                panel.AddPanelItem(new Platform("XLITLINGMIDDLE", 105, y, 1));
                panel.AddPanelItem(new FreeTextPanelItem("AENDBREAK2", 118, 0,
                [
                    new ($"0x8A", SimSigColour.Grey, 0, y)
                ]));

                //y=4
                y++;
                panel.AddPanelItem(new TrackNoCircuit("UPEXIT", 2, y, SimSigOrientation.West, 8));

                trackCircuit = new TrackWithCircuit("T3761", 10, y, SimSigOrientation.West, 3, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3762A", 13, y, SimSigOrientation.West, 12, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3861A", 25, y, SimSigOrientation.West, 14, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3862A", 39, y, SimSigOrientation.West, 14, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T3961A", 53, y, SimSigOrientation.West, 14, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(4, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4061A", 67, y, SimSigOrientation.West, 14, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(3, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4161A", 81, y, SimSigOrientation.West, 10, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(2  , 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T42161A", 91, y, SimSigOrientation.West, 14, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(5, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4361", 105, y, SimSigOrientation.West, 3, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4362A", 108, y, SimSigOrientation.West, 10, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, [new(2, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new FreeTextPanelItem("AENDBREAK3", 118, 0,
                [
                    new ($"0x8A", SimSigColour.Grey, 0, y)
                ]));
                //y=5
                y++;
                panel.AddPanelItem(new SignalHead("K960", 14, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K962", 29, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K964", 43, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K966", 57, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new SignalHead("K968", 70, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.IsAutoSignal, true));
                panel.AddPanelItem(new Platform("ASHWELL&MORDENUP", 74, y, 4));          
                panel.AddPanelItem(new SignalHead("K970", 83, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Green | SimSigSignalHeadOption.IsAutoSignal, false));
                panel.AddPanelItem(new SignalHead("K972", 96, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Green | SimSigSignalHeadOption.IsAutoSignal, false));
                panel.AddPanelItem(new Platform("XLITLINGTONBOTTOM", 105, y, 1));        
                panel.AddPanelItem(new SignalHead("K974", 108, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Green | SimSigSignalHeadOption.IsAutoSignal, false));
                panel.AddPanelItem(new EmergencyReplacement("ER-K974", 109, y, SimSigOrientation.West, false));
                panel.AddPanelItem(new FreeTextPanelItem("AENDBOTTOM", 118, 0,
                [
                    new ($"{SimSigFont.ConvertTextToHex("A")}", SimSigColour.Grey, 0, y)
                ]));

                //y=6
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ASHWELL&MORDENUP", 72, 0,
                [
                    new ($"{SimSigFont.ConvertTextToHex("& MORDEN")}", SimSigColour.Grey, 0, y)
                ]));
                panel.AddPanelItem(new AHB("LITTLINGTON", 100, y, SimSigAHB.Raised));

                //y=7
                y++;

                //y=8
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ONDUTY", 3, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("ON DUTY")}", SimSigColour.Yellow, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("BERTHGRAINSDG", 29, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("GRAIN SDG")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBGRAINSDG", 39, y));
                panel.AddPanelItem(new FreeTextPanelItem("BERTHDALGETY", 47, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DALGETY")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBDALGETY", 55, y));

                //y=9
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ONDUTYBOXTOP", 3, y,
                [
                    new ($"0x800x890x890x890x890x890x890x81", SimSigColour.Yellow, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("DOWNGRAINSDG", 29, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DOWN SDG")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBDOWNSDG", 39, y));
                panel.AddPanelItem(new FreeTextPanelItem("BERTHFREIGHT", 47, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("FREIGHT")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBFREIGHT", 55, y));

                //y=10
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ONDUTYBOXLEFT", 3, y,
                [
                    new ($"0x8A", SimSigColour.Yellow, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBONDUTY", 5, y, SimSigColour.Green));
                panel.AddPanelItem(new FreeTextPanelItem("ONDUTYBOXRIGHT", 10, y,
                [
                    new ($"0x8A", SimSigColour.Yellow, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("SHERRIFFS", 28, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("SHERRIFF'S")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBSHERRIFFS", 39, y));
                panel.AddPanelItem(new FreeTextPanelItem("BERTHDOWNLOOP", 45, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DOWN LOOP")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBDOWNLOOP", 55, y));

                //y=11
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ONDUTYBOXBOTTOM", 3, y,
                [
                    new ($"0x820x890x890x890x890x890x890x83", SimSigColour.Yellow, 0, 0)
                ]));
                panel.AddPanelItem(new BerthBlock("BBSCRATCH1", 39, y));
                panel.AddPanelItem(new BerthBlock("BBSCRATCH2", 55, y));

                //y=12
                y++;

                //y=13
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("LEGENDSHERRIFF", 59, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*SHF = SHERRIFF'S SDG.")}", SimSigColour.Grey, 0, 0)
                ]));

                //y=14
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("GRAIN", 22, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("GRAIN")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("KEYSHF", 30, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*SHF")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("KEYDAL", 38, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*DAL")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("KEYFRT", 43, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*FRT")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("LEGENDDLAGETY", 59, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*DAL = DALGETY SDG.")}", SimSigColour.Grey, 0, 0)
                ]));

                //y=15
                y++;
                panel.AddPanelItem(new TrackNoCircuit("GRAIN1", 22, y, SimSigOrientation.East, 3));
                panel.AddPanelItem(new TrackNoCircuit("GRAIN2", 25, y, SimSigOrientation.NorthWest, 1));
                panel.AddPanelItem(new SignalHead("K249", 26, y, SimSigOrientation.East, SimSigSignalHeadOption.Red | SimSigSignalHeadOption.ShuntAspect, false));
                panel.AddPanelItem(new TrackNoCircuit("SHF", 31, y, SimSigOrientation.NorthEast, 1));
                panel.AddPanelItem(new SignalHead("K244", 38, y, SimSigOrientation.West, SimSigSignalHeadOption.Red | SimSigSignalHeadOption.ShuntAspect | SimSigSignalHeadOption.IsReversed, false));
                panel.AddPanelItem(new TrackNoCircuit("DAL", 40, y, SimSigOrientation.NorthEast, 1));
                panel.AddPanelItem(new TrackNoCircuit("FRT", 43, y, SimSigOrientation.NorthEast, 1));
                panel.AddPanelItem(new SignalHead("K253", 48, y, SimSigOrientation.East, SimSigSignalHeadOption.Red | SimSigSignalHeadOption.ShuntAspect, false));
                panel.AddPanelItem(new FreeTextPanelItem("LEGENDFREIGHTDEPT", 59, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("*FRT = FREIGHT DEPOT")}", SimSigColour.Grey, 0, 0)
                ]));

                // y=16
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("IVY", 14, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("IVY")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("ARROWDNSDG", 20, y,
                [
                    new ($"0x8C", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new TrackNoCircuit("DNSIDING", 22, y, SimSigOrientation.East, 6));
                trackCircuit = new TrackWithCircuit("T4471", 28, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2370A", 29, y, SimSigOrientation.SouthEast, "T4471", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4471", 30, y, SimSigOrientation.East, 3, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4472", 33, y, SimSigOrientation.East, 2, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2371B", 35, y, SimSigOrientation.SouthWest, "T4472", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4472", 36, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new TrackNoCircuit("LOOP", 37, y, SimSigOrientation.East, 14));
                trackCircuit = new TrackWithCircuit("T4474", 51, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2376A", 52, y, SimSigOrientation.SouthEast, "T4474", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4474", 53, y, SimSigOrientation.East, 2, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new FreeTextPanelItem("ARROWUPHEADSHUNT", 56, y,
                [
                    new ($"0x8D", SimSigColour.Grey, 0, 0)
                ]));

                //y=17
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("IVYFARM2", 14, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("FARM")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new FreeTextPanelItem("DNSID.", 22, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DN.SDG")}", SimSigColour.Grey, 0, 0)
                ]));
                trackCircuit = new TrackWithCircuit("T4471", 29, y, SimSigOrientation.SouthEast, 3, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4472", 35, y, SimSigOrientation.SouthWest, 3, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new FreeTextPanelItem("DOWNLOOP", 40, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("DOWN LOOP")}", SimSigColour.Grey, 0, 0)
                ]));
                trackCircuit = new TrackWithCircuit("T4472", 52, y, SimSigOrientation.SouthEast, 3, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);

                //y=18
                y++;
                panel.AddPanelItem(new FreeTextPanelItem("ASTARTTOP", 0, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("A")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new AutoButton("AUTOK975", 7, y, SimSigOrientation.East, false));
                panel.AddPanelItem(new SignalHead("K975", 9, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Red, false));
                panel.AddPanelItem(new Platform("XIVYFARM", 15, y, 1));
                panel.AddPanelItem(new SignalHead("SIGLOS", 19, y, SimSigOrientation.West, SimSigSignalHeadOption.ShuntAspect | SimSigSignalHeadOption.IsReversed | SimSigSignalHeadOption.Red, false));
                panel.AddPanelItem(new FreeTextPanelItem("LOS", 20, y,
                [
                    new ($"{SimSigFont.ConvertTextToHex("LOS")}", SimSigColour.Grey, 0, 0)
                ]));
                panel.AddPanelItem(new SignalHead("K977", 24, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainWithCallOn | SimSigSignalHeadOption.Red, false));
                trackCircuit = new TrackWithCircuit("T4450", 31, y, SimSigOrientation.NorthWest, 1, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4450", 33, y, SimSigOrientation.NorthEast, 1, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4453", 54, y, SimSigOrientation.NorthWest, 1, SimSigTrackCircuitOption.BreakAtStart, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new SignalHead("K978", 57, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainWithCallOn | SimSigSignalHeadOption.Red | SimSigSignalHeadOption.IsReversed, false));
                panel.AddPanelItem(new Platform("ROYSTON2", 58, y, 11));
                panel.AddPanelItem(new FreeTextPanelItem("ROYSTON2_DIGIT", 63, y,
                [
                    new (SimSigFont.ConvertTextToHex("2"), SimSigColour.Black, 0, 0)
                ]));
                panel.AddPanelItem(new SignalHead("K981", 70, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Red, false));
                panel.AddPanelItem(new SignalHead("K982", 77, y, SimSigOrientation.West, SimSigSignalHeadOption.SingleMainWithCallOn | SimSigSignalHeadOption.IsReversed | SimSigSignalHeadOption.Red, false));
                panel.AddPanelItem(new AutoButton("AUTOCA108", 99, y, SimSigOrientation.East, true));
                panel.AddPanelItem(new SignalHead("CA103", 101, y, SimSigOrientation.East, SimSigSignalHeadOption.SingleMainAspect | SimSigSignalHeadOption.Green | SimSigSignalHeadOption.HasRouteSet, false));

                //y=19
                y++;

                panel.AddPanelItem(new FreeTextPanelItem("ASTARTBREAK1", 0, y,
                [
                    new ($"0x8A", SimSigColour.Grey, 0, 0)
                ]));
                trackCircuit = new TrackWithCircuit("T4351", 1, y, SimSigOrientation.East, 9, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, [new(3, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4352", 10, y, SimSigOrientation.East, 3, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4353_1", 13, y, SimSigOrientation.East, 3, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4353_2", 16, y, SimSigOrientation.East, 11, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, [new(5, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4450", 27, y, SimSigOrientation.East, 3, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2369B", 30, y, SimSigOrientation.SouthWest, "T4450", false, false, SimSigPointConfig.Auto));
                panel.AddPanelItem(new Point("P2370B", 31, y, SimSigOrientation.NorthWest, "T4450", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4450", 32, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2371A", 33, y, SimSigOrientation.NorthEast, "T4450", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4450", 34, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4451", 38, y, SimSigOrientation.East, 7, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new (2,0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2375A", 45, y, SimSigOrientation.SouthEast, "T4451", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4451", 46, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4453", 50, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2376B", 54, y, SimSigOrientation.NorthWest, "T4453", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4453", 55, y, SimSigOrientation.East, 2, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4454", 57, y, SimSigOrientation.East, 5, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, [new (1,0)]);
                trackCircuit.SetBerth(0, "AAAA");   
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4455", 62, y, SimSigOrientation.East, 7, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, [new(2, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4456", 69, y, SimSigOrientation.East, 5, SimSigTrackCircuitOption.BreakAtStart | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4456", 74, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4551", 75, y, SimSigOrientation.East, 8, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new (2,0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4552", 83, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2377B", 87, y, SimSigOrientation.SouthWest, "T4552", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4552", 88, y, SimSigOrientation.East, 1, SimSigTrackCircuitOption.NoBreak, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                panel.AddPanelItem(new Point("P2378A", 89, y, SimSigOrientation.SouthEast, "T4552", false, false, SimSigPointConfig.Auto));
                trackCircuit = new TrackWithCircuit("T4552", 90, y, SimSigOrientation.East, 4, SimSigTrackCircuitOption.BreakAtEnd, showTrackCircuitBreaks, false, false, []);
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4553", 94, y, SimSigOrientation.East, 10, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, false, [new(4, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);
                trackCircuit = new TrackWithCircuit("T4651", 104, y, SimSigOrientation.East, 10, SimSigTrackCircuitOption.BreakAtEnd | SimSigTrackCircuitOption.IsOverlap, showTrackCircuitBreaks, false, true, [new(4, 0)]);
                trackCircuit.SetBerth(0, "AAAA");
                panel.AddPanelItem(trackCircuit);

                System.Console.WriteLine($"{panel.GetPanelItemsOfType<ISimSigTrackCircuit>().Count()} of {panel.PanelItems.Count()} panel items are track circuits");
                panel.UpdateTrackCircuitOccupancy("T4450", true);

                //build SVG
                var styleClass = SimSigStyle.Instance;
                string svg = panel.BuildSVG(font, styleClass);
                string html = @$"<html><body style=""background:black"">{svg}</body></html>";
                await System.IO.File.WriteAllTextAsync(@"C:\Users\timca\Desktop\Test.html", html);
                await System.IO.File.WriteAllTextAsync(@"C:\Users\timca\Desktop\TestSVG.svg", svg);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(font, Newtonsoft.Json.Formatting.Indented);
                System.Console.WriteLine("Completed successfully");


                System.Console.ReadKey();

            }
            catch (ErrorFrameException ex) 
            { 
                System.Console.WriteLine(ex.ErrorFrame.GetBodyAsString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.GetType() == typeof(AggregateException))
                    {
                        AggregateException aggregateException = (AggregateException)ex.InnerException;

                        if (aggregateException.InnerExceptions != null && aggregateException.InnerExceptions.Count > 0)
                        {
                            if (aggregateException.InnerExceptions[0] is ErrorFrameException errorFrameException)
                            {
                                System.Console.WriteLine(errorFrameException.ErrorFrame.GetBodyAsString());
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
        }

        class ConsoleWriterObserver : IObserver<IStompMessage>
        {
            // Incoming messages are processed here.
            public void OnNext(IStompMessage message)
            {
                System.Console.WriteLine("MESSAGE: " + message.GetContentAsString());

                if (message.IsAcknowledgeable)
                    message.Acknowledge();
            }

            // Any ERROR frame or stream exception comes through here.
            public void OnError(Exception error)
            {
                if (error == null)
                    throw new ArgumentNullException(nameof(error));

                if (error.GetType() == typeof(ErrorFrameException))
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    System.Console.WriteLine((error as ErrorFrameException).ErrorFrame.GetBodyAsString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                else
                {
                    System.Console.WriteLine("ERROR 123: " + error.Message);
                }
            }

            // OnCompleted is invoked when unsubscribing.
            public void OnCompleted()
            {
                System.Console.WriteLine("UNSUBSCRIBED!");
            }
        }
    }
}