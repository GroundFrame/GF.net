using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common.UnitTests._ExtensionMethods
{
    public class String
    {
        /// <summary>
        /// Tests the <see cref="Common.ExtMethods.IsValidEmail(string)"/> method
        /// </summary>
        /// <param name="email"></param>
        /// <param name="expectedResponse"></param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("someemail", false)]
        [InlineData("someemail.com", false)]
        [InlineData("someemail@email", false)]
        [InlineData("someemail@email.com", true)]
        [InlineData("someemail@email.c", false)]
        public void Test_Method_IsValidEmail(string email, bool expectedResponse)
        {
            Assert.Equal(expectedResponse, email.IsValidEmail());
        }
    }
}
