namespace GF.SimSig.UnitTests._Classes._PanelItems
{
    /// <summary>
    /// Tests <see cref="GF.SimSig.AHB"/>
    /// </summary>
    public class AHB
    { 
        private readonly ISimSigFont _font;
        private readonly GF.SimSig.SimSigStyle _style;

        public AHB()
        {
            this._font = GF.SimSig.SimSigFont.BuildDefault();
            this._style = GF.SimSig.SimSigStyle.Instance;
        }


        [Theory]
        [InlineData("someId1", 0, 0, SimSigAHB.Raised, 4)]
        [InlineData("someId2", 10, 15, SimSigAHB.Lowered, 4)]
        [InlineData("someId3", 5, 15, SimSigAHB.Lowered, 4)]
        [InlineData("someId4", 10, 15, SimSigAHB.Working, 4)]
        public void Test_Constructor(string id, ushort xOffset, ushort yOffset, SimSigAHB config, byte fontConfigCount)
        {
            var testAHB = new GF.SimSig.AHB(id, xOffset, yOffset, config);
            Assert.Equal(id, testAHB.SimSigID);
            Assert.Equal(xOffset, testAHB.XOffSet);
            Assert.Equal(yOffset, testAHB.YOffSet);
            Assert.Equal(config, testAHB.AHBConfig);
            Assert.NotEmpty(testAHB.Config);
            Assert.Equal(fontConfigCount, testAHB.Config.Count());
        }

        [Theory]
        [InlineData("someId1", 0, 0, SimSigAHB.Raised, 4)]
        [InlineData("someId2", 10, 15, SimSigAHB.Lowered, 4)]
        [InlineData("someId3", 5, 15, SimSigAHB.Lowered, 4)]
        [InlineData("someId4", 10, 15, SimSigAHB.Working, 4)]
        public void Test_Method_GenerateSVG(string id, ushort xOffset, ushort yOffset, SimSigAHB config, short expectedSVGElements)
        {
            var testAHB = new GF.SimSig.AHB(id, xOffset, yOffset, config);
            var testSVG = testAHB.GenerateSVG(this._font, this._style, []);
            Assert.NotNull(testSVG);
            //test the SVG is valid XML. A dummy root element is added because at this stage the generated SVG is not wrapped into the final panel SVG.
            var testSVGXML = XDocument.Parse($"<root>{testSVG}</root>");
            Assert.NotNull(testSVGXML);
            Assert.Equal(expectedSVGElements, testSVGXML.Descendants("root").Elements().Count());
        }
    }
}
