using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.NetworkInformation;

namespace GF.Common.UnitTests._Classes._Exceptions
{
    public class GFException
    {
        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.BuildArgumentName(string, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="argumentName">The name of the argument</param>
        /// <param name="callingMethod">The calling method name</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData("", "")]
        public void Test_Method_BuildArgumentName_ArgumentNullExceptions(string argumentName, string callingMethod)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.BuildArgumentName(argumentName, callingMethod));
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.BuildArgumentName(string, string?)"/> method
        /// </summary>
        /// <param name="argumentName">The name of the argument</param>
        /// <param name="callingMethod">The calling method name</param>
        [Theory]
        [InlineData("Some Argument Name", "Some Calling Method Name")]
        public void Test_Method_BuildArgumentName(string argumentName, string callingMethod)
        {
            Assert.Equal(Common.GFException.BuildArgumentName(argumentName, callingMethod), $"{argumentName} (called by {callingMethod})");
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.GFException(string, string?)"/> constuctor throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Test_Constructor_MessageOny_ArgumentNullExceptions(string message)
        {
            Assert.Throws<ArgumentNullException>(() => new Common.GFException(message));
        }

        /// <summary>
        /// Tests passing an empty string or null arguments to the <see cref="Common.GFException.GFException(string, Exception, string?)"/> constuctor throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        /// <param name="innerException">The inner exception</param>
        [Theory]
        [InlineData(null, "Some Message")]
        [InlineData(null, "")]
        [InlineData(null, null)]
        public void Test_Constructor_ExceptionMessage_ArgumentNullExceptions(Exception innerException, string message)
        {
            Assert.Throws<ArgumentNullException>(() => new Common.GFException(message, innerException));
        }

        /// <summary>
        /// Tests pass empty string or null arguments to the <see cref="Common.GFException.GFException(string, Exception, string, object[], string?)"/> constuctor throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="placeholders">The translation placeholders</param>
        [Theory]
        [MemberData(nameof(Get_Test_Constructor_ExceptionMessageTranslation_ArgumentNullExceptions_Data))]
        public void Test_Constructor_ExceptionMessageTranslation_ArgumentNullExceptions(string message, string translationKey, object[] placeholders)
        {
            Assert.Throws<ArgumentNullException>(() => new Common.GFException(message, translationKey, placeholders));
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Constructor_ExceptionMessageTranslation_ArgumentNullExceptions(string, string, object[])"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Constructor_ExceptionMessageTranslation_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>() {
                new object[] {
                    null,
                    null,
                    Array.Empty<object>(),
                },
                new object[] {
                    null,
                    string.Empty,
                    Array.Empty<object>()
                },
                new object[] {
                    null,
                    "Some.Key",
                    Array.Empty<object>()
                },
                new object[] {
                    string.Empty,
                    "Some.Key",
                    Array.Empty<object>(),
                },
                new object[] {
                    null,
                    null,
                     new object[]
                    {
                        "Some Placeholder"
                    }
                },
                new object[] {
                    null,
                    string.Empty,
                    new object[]
                    {
                        "Some Placeholder"
                    }
                },
                new object[] {
                    null,
                    "Some.Key",
                    new object[]
                    {
                        "Some Placeholder"
                    }
                },
                new object[] {
                    string.Empty,
                    "Some.Key",
                    new object[]
                    {
                        "Some Placeholder"
                    }
                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.GFException(string, string?)"/> constructor
        /// </summary>
        /// <param name="message">The exception message</param>
        [Theory]
        [InlineData("Some Message")]
        public void Test_Constructor_Message(string message)
        {
            Common.GFException testException = new(message);
            Assert.Equal(message, testException.Message);
            Assert.Null(testException.InnerException);
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.GFException(Exception, string, string?)"/> constructor
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        [Theory]
        [MemberData(nameof(Get_Test_Constructor_ExceptionMessage_Data))]
        public void Test_Constructor_ExceptionMessage(Exception innerException, string message)
        {
            Common.GFException testException = new(message, innerException);
            Assert.Equal(message, testException.Message);
            Assert.Equal(innerException.Message, testException.InnerException?.Message);
            Assert.IsType(innerException.GetType(), innerException);

        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Constructor_ExceptionMessage(Exception, string)"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Constructor_ExceptionMessage_Data()
        {
            return new List<object[]>() {
                new object[] {
                    new Exception("Some Inner Exception"),
                    "Some Message",
                },
                new object[] {
                    new ArgumentException("Some Inner ArgumentException", "Some Param Name"),
                    "Some Other Message",
                }
            };
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.GFException(string, string?)"/> constructor
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="placeholders">The placeholders</param>
        [Theory]
        [MemberData(nameof(Get_Test_Constructor_ExceptionMessageTranslation_Data))]
        public void Test_Constructor_ExceptionMessageTranslation(string message, string translationKey, object[] placeholders)
        {
            Common.GFException testException = new(message, translationKey, placeholders);
            Assert.Equal(message, testException.Message);
            Assert.Equal(translationKey, testException.MessageTranslationKey);

            if (placeholders == null)
            {
                Assert.Null(testException.MessageTranslationPlaceholders);
            }
            else
            {
                Assert.Equal(placeholders, testException.MessageTranslationPlaceholders);
            }
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Constructor_ExceptionMessageTranslation(string, string, object[])"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Constructor_ExceptionMessageTranslation_Data()
        {
            return new List<object[]>() {
                new object[] {
                    "Some Message",
                    "Some Key",
                    Array.Empty<object>()
                },
                new object[] {
                    "Some Other Message",
                    "Some Other Key",
                    Array.Empty<object>()
                },
                new object[] {
                    "Some Other Message",
                    "Some Other Key",
                    new object[]
                    {
                        "Some Placeholder"
                    }
                }
            };
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build(string, string, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        public void Test_Method_Build_MessageTranslation_ArgumentNullExceptions(string message, string translationKey)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(message, translationKey));
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build{T0}(string, string, T0, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_MessageTranslationArg0_ArgumentNullExceptions_Data))]
        public void Test_Method_Build_MessageTranslationArg0_ArgumentNullExceptions<T0>(string message, string translationKey, T0 arg0)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(message, translationKey, arg0));
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Method_Build_MessageTranslationArg0_ArgumentNullExceptions{T0}(string, string, T0)"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Method_Build_MessageTranslationArg0_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>() {
                new object[] {
                    null,
                    null,
                    null
                },
                new object[] {
                    "",
                    null,
                    null
                },
                new object[] {
                    "Some Message",
                    null,
                    null
                },
                new object[] {
                    "Some Message",
                    "",
                    null
                },
                new object[] {
                    "Some Other Message",
                    "Some Other Key",
                    null
                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build{T0}(string, string, T0, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        /// <param name="arg1">The second placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_MessageTranslationArg0Arg1_ArgumentNullExceptions_Data))]
        public void Test_Method_Build_MessageTranslationArg0Arg1_ArgumentNullExceptions<T0, T1>(string message, string translationKey, T0 arg0, T1 arg1)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(message, translationKey, arg0, arg1));
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Method_Build_MessageTranslationArg0Arg1_ArgumentNullExceptions{T0, T1}(string, string, T0, T1)"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Method_Build_MessageTranslationArg0Arg1_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>() {
                new object[] {
                    null,
                    null,
                    null,
                    null
                },
                new object[] {
                    "",
                    null,
                    null,
                    null
                },
                new object[] {
                    "Some Message",
                    null,
                    null,
                    null
                },
                new object[] {
                    "Some Message",
                    "",
                    null,
                    null

                },
                new object[] {
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    null

                },
                new object[] {
                    "Some Message",
                    "",
                    "Some Placeholder",
                    null

                },
                new object[] {
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    "Some Placeholder"

                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build(Exception, string, string, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_ExceptionMessageTranslation_ArgumentNullExceptions_Data))]
        public void Test_Method_Build_ExceptionMessageTranslation_ArgumentNullExceptions(Exception innerException, string message, string translationKey)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(innerException, message, translationKey));
        }

        /// <summary>
        /// Generates the test data for the <see cref="Test_Method_Build_ExceptionMessageTranslation_ArgumentNullExceptions(Exception, string, string)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_ExceptionMessageTranslation_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>(){
                new object[] {
                    null,
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    null
                },
                new object[] {
                    null,
                    "",
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "",
                    null
                },
                new object[] {
                    null,
                    null,
                    ""
                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    ""
                },
                new object[] {
                    null,
                    "Some Message",
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    null
                },
                new object[] {
                    null,
                    "Some Message",
                    ""
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    ""
                },
                new object[] {
                    null,
                    null,
                    "Some Translation Key"
                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    "Some Translation Key"
                },
                new object[] {
                    null,
                    null,
                    "Some Translation Key"
                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    "Some Translation Key"
                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build{T0}(Exception, string, string, T0, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_ExceptionMessageTranslationArg0_ArgumentNullExceptions_Data))]
        public void Test_Method_Build_ExceptionMessageTranslationArg0_ArgumentNullExceptions<T0>(Exception innerException, string message, string translationKey, T0 arg0)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(innerException, message, translationKey, arg0));
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Method_Build_ExceptionMessageTranslationArg0_ArgumentNullExceptions{T0}(Exception string, string, T0)"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Method_Build_ExceptionMessageTranslationArg0_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>() {
                new object[] {
                    null,
                    null,
                    null,
                    null
                },
                new object[] {
                    null,
                    "",
                    null,
                    null
                },
                new object[] {
                    null,
                    "Some Message",
                    null,
                    null
                },
                new object[] {
                    null,
                    "Some Message",
                    "",
                    null
                },
                new object[] {
                    null,
                    "Some Other Message",
                    "Some Other Key",
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "",
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    "",
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Other Message",
                    "Some Other Key",
                    null
                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests passing empty string or null arguments to the <see cref="Common.GFException.Build{T0, T1}(Exception, string, string, T0, T1, string?)"/> method throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        /// <param name="message">The message argument</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        /// <param name="arg1">The second placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_ExceptionMessageTranslationArg0Arg1_ArgumentNullExceptions_Data))]
        public void Test_Method_Build_ExceptionMessageTranslationArg0Arg1_ArgumentNullExceptions<T0, T1>(Exception innerException, string message, string translationKey, T0 arg0, T1 arg1)
        {
            Assert.Throws<ArgumentNullException>(() => Common.GFException.Build(innerException, message, translationKey, arg0, arg1));
        }

        /// <summary>
        /// Generates the data for the <see cref="Test_Method_Build_ExceptionMessageTranslationArg0Arg1_ArgumentNullExceptions{T0,T1}(Exception, string, string, T0, T1)"/> method
        /// </summary>
        /// <returns><see cref="List{object{}}"/></returns>
        public static List<object[]> Get_Test_Method_Build_ExceptionMessageTranslationArg0Arg1_ArgumentNullExceptions_Data()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new List<object[]>() {
                new object[] {
                    null,
                    null,
                    null,
                    null,
                    null
                },
                new object[] {
                    null,
                    "",
                    null,
                    null,
                    null
                },
                new object[] {
                    null,
                    "Some Message",
                    null,
                    null,
                    null
                },
                new object[] {
                    null,
                    "Some Message",
                    "",
                    null,
                    null

                },
                new object[] {
                    null,
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    null

                },
                new object[] {
                    null,
                    "Some Message",
                    "",
                    "Some Placeholder",
                    null

                },
                new object[] {
                    null,
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    "Some Placeholder"

                },
                new object[] {
                    new Exception("Some Exception"),
                    null,
                    null,
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "",
                    null,
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    null,
                    null,
                    null
                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    "",
                    null,
                    null

                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    null

                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Message",
                    "",
                    "Some Placeholder",
                    null

                },
                new object[] {
                    new Exception("Some Exception"),
                    "Some Other Message",
                    "Some Other Key",
                    null,
                    "Some Placeholder"

                }
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build(string, string, string?)"/> method
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        [Theory]
        [InlineData("Some Message", "Some Translation Key")]
        public void Test_Method_Build_MessageTranslation(string message, string translationKey)
        {
            var testTranslation = Common.GFException.Build(message, translationKey);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Empty(testTranslation.MessageTranslationPlaceholders!);
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build(Exception, string, string, string?)"/> method
        /// </summary>
        /// <param name="innerException">The inner excetion</param>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_MessageTranslation_Data))]
        public void Test_Method_Build_ExceptionMessageTranslation(Exception innerException, string message, string translationKey)
        {
            var testTranslation = Common.GFException.Build(innerException, message, translationKey);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Empty(testTranslation.MessageTranslationPlaceholders!);
            Assert.Equal(innerException, testTranslation.InnerException);
        }

        /// <summary>
        /// Gets the data for the <see cref="Test_Method_Build_ExceptionMessageTranslation(Exception, string, string)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_MessageTranslation_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key"
                }
            };
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build{T0}(string, string, T0, string?)"/> method
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_MessageTranslationArg0_Data))]
        public void Test_Method_Build_MessageTranslationArg0<T0>(string message, string translationKey, T0 arg0)
        {
            var testTranslation = Common.GFException.Build(message, translationKey, arg0);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Single(testTranslation.MessageTranslationPlaceholders!);
            Assert.Equal(arg0, testTranslation.MessageTranslationPlaceholders![0]);
        }

        /// <summary>
        /// Gets the data for the <see cref="Test_Method_Build_MessageTranslationArg0{T0}(string, string, T0)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_MessageTranslationArg0_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    DateTime.Today
                },
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    1
                },
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    "Some Placeholder"
                }
            };
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build{T0}(Exception, string, string, T0, string?)"/> method
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_ExceptionMessageTranslationArg0_Data))]
        public void Test_Method_Build_ExceptionMessageTranslationArg0<T0>(Exception innerException, string message, string translationKey, T0 arg0)
        {
            var testTranslation = Common.GFException.Build(innerException, message, translationKey, arg0);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Single(testTranslation.MessageTranslationPlaceholders!);
            Assert.Equal(arg0, testTranslation.MessageTranslationPlaceholders![0]);
            Assert.Equal(innerException, testTranslation.InnerException);
        }

        /// <summary>
        /// Gets the data for the <see cref="Test_Method_Build_ExceptionMessageTranslationArg0{T0}(Exception, string, string, T0)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_ExceptionMessageTranslationArg0_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    DateTime.Today
                },
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    1
                },
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    "Some Placeholder"
                }
            };
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build{T0, T1}(string, string, T0, T1, string?)"/> method
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        /// <param name="arg1">The second placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_MessageTranslationArg0Arg1_Data))]
        public void Test_Method_Build_MessageTranslationArg0Arg1<T0, T1>(string message, string translationKey, T0 arg0, T1 arg1)
        {
            var testTranslation = Common.GFException.Build(message, translationKey, arg0, arg1);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Equal(2, testTranslation.MessageTranslationPlaceholders!.Length);
            Assert.Equal(arg0, testTranslation.MessageTranslationPlaceholders![0]);
            Assert.Equal(arg1, testTranslation.MessageTranslationPlaceholders![1]);
        }

        /// <summary>
        /// Gets the data for the <see cref="Test_Method_Build_MessageTranslationArg0Arg1{T0, T1}(string, string, T0, T1)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_MessageTranslationArg0Arg1_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    DateTime.Today,
                    1
                },
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    1,
                    DateTime.Today
                },
                new object[]
                {
                    "Some Message",
                    "Some Translation Key",
                    "Some Placeholder",
                    false
                }
            };
        }

        /// <summary>
        /// Tests the <see cref="Common.GFException.Build{T0, T1}(Exception, string, string, T0, T1, string?)"/> method
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="arg0">The first placeholder</param>
        /// <param name="arg1">The second placeholder</param>
        [Theory]
        [MemberData(nameof(Get_Test_Method_Build_ExceptionMessageTranslationArg0Arg1_Data))]
        public void Test_Method_Build_ExceptionMessageTranslationArg0Arg1<T0, T1>(Exception innerException, string message, string translationKey, T0 arg0, T1 arg1)
        {
            var testTranslation = Common.GFException.Build(innerException, message, translationKey, arg0, arg1);
            Assert.IsType<Common.GFException>(testTranslation);
            Assert.Equal(message, testTranslation.Message);
            Assert.Equal(translationKey, testTranslation.MessageTranslationKey);
            Assert.Equal(2, testTranslation.MessageTranslationPlaceholders!.Length);
            Assert.Equal(arg0, testTranslation.MessageTranslationPlaceholders![0]);
            Assert.Equal(arg1, testTranslation.MessageTranslationPlaceholders![1]);
            Assert.Equal(innerException, testTranslation.InnerException);
        }

        /// <summary>
        /// Gets the data for the <see cref="Test_Method_Build_ExceptionMessageTranslationArg0Arg1{T0, T1}(Exception, string, string, T0, T1)"/> method
        /// </summary>
        /// <returns></returns>
        public static List<object[]> Get_Test_Method_Build_ExceptionMessageTranslationArg0Arg1_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    DateTime.Today,
                    1
                },
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    1,
                    DateTime.Today
                },
                new object[]
                {
                    new Exception("Some Exception"),
                    "Some Message",
                    "Some Translation Key",
                    "Some Placeholder",
                    false
                }
            };
        }
    }
}