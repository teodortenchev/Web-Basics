using NUnit.Framework;
using SIS.HTTP.Extensions;

namespace SIS.HTTP.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test, TestCaseSource("TestCases")]
        public void StringExtensionCapitalizeWorks(string testWord)
        {
            string expected = "Post";
            string actual = testWord.Capitalize();

            Assert.AreEqual(expected, actual, "Method is not Capitalizing correctly.");
        }

        static string[] TestCases = new string[] { "POST", "post", "pOsT", "Post" };
    }
}
