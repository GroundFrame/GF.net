using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Transactions;

namespace GF.Common.UnitTests._Structs
{
    public class Length
    {
        /// <summary>
        /// Tests passing invalid meters to the <see cref="Common.Length.Length(double)"/> constructor throws an <see cref="ArgumentException"/>
        /// </summary>
        /// <param name="meters">The meters to pass to <see cref="GF.Common.Length.Length(double)"/></param>
        [Theory]
        [InlineData(-10)]
        [InlineData(-104.54d)]
        public void Test_Constructor_Meters_ArgumentException(double meters)
        {
            Assert.Throws<ArgumentException>(() => new GF.Common.Length(meters));
        }

        /// <summary>
        /// Tests passing invalid meters to the <see cref="Common.Length.Length(int, byte)"/> constructor throws an <see cref="ArgumentException"/>
        /// </summary>
        /// <param name="meters">The meters to pass to <see cref="GF.Common.Length.Length(double)"/></param>
        [Theory]
        [InlineData(-10,130)]
        [InlineData(10,130)]
        [InlineData(-10,3)]
        public void Test_Constructor_MilesChains_ArgumentException(int miles, byte chains)
        {
            Assert.Throws<ArgumentException>(() => new GF.Common.Length(miles, chains));
        }

        /// <summary>
        /// Tests the <see cref="Common.Length.Length(double)"/> constructor
        /// </summary>
        /// <param name="meters">The meters to pass to the <see cref="GF.Common.Length.Length(double)"/></param>
        /// <param name="expectedMeters">The expected number of meters that the <see cref="GF.Common.Length.Meters"/> should return</param>
        /// <param name="expectedInches">The expected number of inches that the <see cref="GF.Common.Length.Inches"/> should return</param>
        /// <param name="expectedMilesAndChains">The expected number of miles and chains that the <see cref="GF.Common.Length.MilesAndChains"/> should return</param>
        [Theory]
        [InlineData(0, 0, 0, "0m 0ch")]
        [InlineData(1, 1, 39.370098114013672d, "0m 0ch")]
        [InlineData(10, 10, 393.70098114013672d, "0m 0ch")]
        [InlineData(100, 100, 3937.0098114013672d, "0m 4ch")]
        [InlineData(1000, 1000, 39370.098114013672d, "0m 49ch")]
        [InlineData(10000, 10000, 393700.98114013672d, "6m 17ch")]
        [InlineData(100000, 100000, 3937009.8114013672d, "62m 10ch")]
        [InlineData(1000000, 1000000, 39370098.114013672d, "621m 29ch")]
        [InlineData(10000000, 10000000, 393700981.14013672d, "6213m 57ch")]
        [InlineData(100000000, 100000000, 3937009811.4013672d, "62137m 11ch")]
        public void Test_Constructor_Meters(double meters, double expectedMeters, double expectedInches, string expectedMilesAndChains)
        {
            var testLength = new GF.Common.Length(meters);
            Assert.Equal(expectedMeters, testLength.Meters);
            Assert.Equal(expectedInches, testLength.Inches);
            Assert.Equal(expectedMilesAndChains, testLength.MilesAndChains);
        }

        /// <summary>
        /// Tests the <see cref="Common.Length.Length(int, byte)"/> constructor
        /// </summary>
        /// <param name="miles">The miles to pass to the <see cref="GF.Common.Length.Length(int, byte)"/></param>
        /// <param name="chains">The chains to pass to the <see cref="GF.Common.Length.Length(int, byte)"/></param>
        /// <param name="expectedMilesAndChains">The expected number of miles and chains that the <see cref="GF.Common.Length.MilesAndChains"/> should return</param>
        /// <param name="expectedMeters">The expected number of meters that the <see cref="GF.Common.Length.Meters"/> should return</param>
        /// <param name="expectedInches">The expected number of inches that the <see cref="GF.Common.Length.Inches"/> should return</param>
        [Theory]
        [InlineData(0, 0, "0m 0ch", 0, 0)]
        [InlineData(1, 40, "1m 40ch", 2414.015970826149d, 95040)]
        public void Test_Constructor_MilesChains(int miles, byte chains, string expectedMilesAndChains, double expectedMeters, double expectedInches)
        {
            var testLength = new GF.Common.Length(miles, chains);
            Assert.Equal(expectedMilesAndChains, testLength.MilesAndChains);
            Assert.Equal(expectedMeters, testLength.Meters);
            Assert.Equal(expectedInches, testLength.Inches);
        }

        /// <summary>
        /// Tests the <see cref="GF.Common.Length.Add(Common.Length)"/> method
        /// </summary>
        /// <param name="meters1">The number of meters of the first length</param>
        /// <param name="meters2">The number of meters of the second length</param>
        /// <param name="expectedLength">The expected length</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Add_Data))]
        public void Test_Method_Add(double meters1, double meters2, GF.Common.Length expectedLength)
        {
            var initialLength = new GF.Common.Length(meters1);
            Assert.Equal(expectedLength, initialLength.Add(new Common.Length(meters2)));
        }

        /// <summary>
        /// Generates the test data for the <see cref="Test_Method_Add(double, double, Common.Length)"/> method
        /// </summary>
        /// <returns><see cref="TheoryData{double, double, GF.Common.Length}"/></returns>
        public static TheoryData<double, double, GF.Common.Length> Get_Test_Method_Add_Data()
        {
            return new TheoryData<double, double, Common.Length>()
            {
                { 0, 1, new Common.Length(1) },
                { 0, 0, new Common.Length(0) },
                { 10, 21, new Common.Length(31) }
            };
        }
    }
}