using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT.UnitTests
{
    public class SignalBox
    {
        [Fact]
        public void Test_Construction()
        {
            GF.Rails.Operations.BRRegion southernRegion = new BRRegion("Southern Region", "SR");
            GF.Rails.Network.SandT.SignalBox testSignalBox = new GF.Rails.Network.SandT.SignalBox("Salisbury", new GF.Common.Coordinates(51.07038027651842, -1.805274136780084), new DateTime(1981,8,19), southernRegion, GF.Rails.Network.SandT.SignalBoxType.PowerSignalBox, "SY");
            var testSignalBoxPanelOrWorkstation = testSignalBox.AddPanelOrWorkstation(new DateTime(1981, 8, 19), null, "Salisbury", String.Empty);
            
            
            
            var colourLightMain3AspectRYG = new ColourLightMainAspect(ColourLightBulbType.MASIncandescent, SignalStateOptions.Red | SignalStateOptions.SingleYellow | SignalStateOptions.Green, SignalStateOptions.SingleYellow | SignalStateOptions.Green, SignalStateOptions.Red);
            var colourLightShuntRW = new ColourLightShunt(ColourLightBulbType.MASIncandescent, SignalStateOptions.RedWhite | SignalStateOptions.DoubleWhite, SignalStateOptions.DoubleWhite, SignalStateOptions.RedWhite);

            //signal blocks
            var signalBlockTBE = new GF.Rails.Network.SandT.SignalBlock("TBE", "SYTBE", new Common.Coordinates(51.070477547247336, -1.8066212535715676), new Common.Length(250), "4", "DM");
            var signalBlockTBF = new GF.Rails.Network.SandT.SignalBlock("TBF", "SYTBF", new Common.Coordinates(51.07085540515768, -1.809727968029587), new Common.Length(100));
            var signalBlockTBG = new GF.Rails.Network.SandT.SignalBlock("TBG", "SYTBG", new Common.Coordinates(51.07101374280826, -1.8105636699651926), new Common.Length(100));
            var signalBlockTBH = new GF.Rails.Network.SandT.SignalBlock("TBH", "SYTBH", new Common.Coordinates(51.0712622716991, -1.811911319079835), new Common.Length(100));
            var signalBlockTBJ = new GF.Rails.Network.SandT.SignalBlock("TBJ", "SYTBJ", new Common.Coordinates(51.071462741875685, -1.8131098443844864), new Common.Length(100));
            var signalBlockTBK = new GF.Rails.Network.SandT.SignalBlock("TBK", "SYTBK", new Common.Coordinates(51.07330745872764, -1.8190187393017603), new Common.Length(100));

            var signalSY49 = new GF.Rails.Network.SandT.Signal("49", new GF.Common.Coordinates(51.07077078961868, -1.8093689168436529), new DateTime(1981, 8, 19), testSignalBoxPanelOrWorkstation, SignalHeadType.MainAspect, colourLightMain3AspectRYG, signalBlockTBE, DirectionType.Down);
            var signalSY53 = new GF.Rails.Network.SandT.Signal("53", new GF.Common.Coordinates(51.07483128957385, -1.8220901972694354), new DateTime(1981, 8, 19), testSignalBoxPanelOrWorkstation, SignalHeadType.MainAspect, colourLightMain3AspectRYG, signalBlockTBK, DirectionType.Down);
            var signalSY240 = new GF.Rails.Network.SandT.Signal("240", new GF.Common.Coordinates(51.071653732177964, -1.8140176071399292), new DateTime(1981, 8, 19), testSignalBoxPanelOrWorkstation, SignalHeadType.MainAspect, colourLightShuntRW, signalBlockTBK, DirectionType.Up);

            Assert.Equal(3, testSignalBoxPanelOrWorkstation.PanelConfig.PanelItems.Count());
        }
    }
}
