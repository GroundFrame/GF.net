using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;

namespace GF.Common.UnitTests._Structs
{
    public class Coordinates
    {
        /// <summary>
        /// Tests passing invalid latitudes / longitudes to the <see cref="Common.Coordinates.Coordinates(float, float)"/> constructor throws an <see cref="ArgumentException"/>
        /// </summary>
        /// <param name="latitude">The latitude to pass to the <see cref="GF.Common.Coordinates"/></param>
        /// <param name="longitude">The longitude to pass to the <see cref="GF.Common.Coordinates"/></param>
        [Theory]
        [InlineData(0,-200)]
        [InlineData(0, 200)]
        [InlineData(-100, -200)]
        [InlineData(100, 200)]
        [InlineData(-100, 0)]
        [InlineData(100, 0)]
        public void Test_Constructor_ArgumentException(float latitude, float longitude)
        {
            Assert.Throws<ArgumentException>(() => new GF.Common.Coordinates(latitude, longitude));
        }

        /// <summary>
        /// Tests the <see cref="Common.Coordinates.Coordinates(float, float)"/> constructor
        /// </summary>
        /// <param name="latitude">The latitude to pass to the <see cref="GF.Common.Coordinates"/></param>
        /// <param name="longitude">The longitude to pass to the <see cref="GF.Common.Coordinates"/></param>
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(45.5, 90.1)]
        [InlineData(-45.5, -90.1)]
        [InlineData(-89.123456, -179.123456)]
        public void Test_Constructor(float latitude, float longitude)
        {
            var testCoordinates = new GF.Common.Coordinates(latitude, longitude);
            Assert.Equal(latitude, testCoordinates.Latitude);
            Assert.Equal(longitude, testCoordinates.Longitude);
            Assert.Equal(latitude, testCoordinates.Y);
            Assert.Equal(longitude, testCoordinates.X);
        }

        /// <summary>
        /// Tests the <see cref="GF.Common.Coordinates.DistanceTo(ISpatial)"/> method
        /// </summary>
        /// <param name="fromLatitude"></param>
        /// <param name="fromLongitude"></param>
        /// <param name="toLatitude"></param>
        /// <param name="toLongitude"></param>
        /// <param name="expectedMeters"></param>
        [Theory]
        [MemberData(nameof(Test_DistanceToData))]
        public void Test_DistanceTo(float fromLatitude,  float fromLongitude, float toLatitude, float toLongitude, double expectedMeters)
        {
            var fromTestCordinates = new GF.Common.Coordinates(fromLatitude, fromLongitude);
            var toTestCordinates = new GF.Common.Coordinates(toLatitude, toLongitude);
            Assert.Equal(expectedMeters, fromTestCordinates.DistanceTo(toTestCordinates).Meters);
        }

        /// <summary>
        /// Builds the data for the <see cref="Test_DistanceTo(float, float, float, float, double)"/> method
        /// </summary>
        /// <returns><see cref="TheoryData{float, float, float, float, double}"/></returns>
        public static TheoryData<float, float, float, float, double> Test_DistanceToData()
        {
            return new ()
            {
                { 0.0f, 0.0f ,45.0f, 45.0f, 6677455.1852051066d },
            };
        }
    }
}