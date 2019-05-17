using NUnit.Framework;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using System;
using System.Linq;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class HttpRequestTests
    {
        private HttpRequest httpRequest;
        private static string[] getRequest;
        private static string[] missingHostRequest; 
        [SetUp]
        public void Init()
        {
            string request = "GET home/index?search=nissan&category=SUV#hashtag HTTP/1.1" 
                            + GlobalConstants.HTTPNewLine 
                            + "Host: localhost:8000" 
                            + GlobalConstants.HTTPNewLine
                            + "Accept: text/plain"
                            + GlobalConstants.HTTPNewLine
                            + "Authorization: Basic dGVzdHVzZXIwMTpuZXRjb29s"
                            + GlobalConstants.HTTPNewLine
                            + "Connection: keep-alive";

            string missingHost = "GET home/index?search=nissan&category=SUV#hashtag HTTP/1.1"
                            + GlobalConstants.HTTPNewLine
                            + "Most: localhost:8000"
                            + GlobalConstants.HTTPNewLine
                            + "Accept: text/plain"
                            + GlobalConstants.HTTPNewLine
                            + "Authorization: Basic dGVzdHVzZXIwMTpuZXRjb29s"
                            + GlobalConstants.HTTPNewLine
                            + "Connection: keep-alive";

            missingHostRequest = missingHost.Split(new[] { GlobalConstants.HTTPNewLine }, StringSplitOptions.None);



            getRequest = request.Split(new[] { GlobalConstants.HTTPNewLine }, StringSplitOptions.None);


            httpRequest = new HttpRequest(request);
        }

        [Test]
        public void IsValidRequestLineIsFalseOnLessThan3Elements()
        {
            //Arrange
            string[] requestLine = "GET HTTP/1.1".
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Act
            
            MethodInfo testMethod = GetMethod("IsValidRequestLine");
            bool actual = (bool)testMethod.Invoke(httpRequest, new object[] { requestLine });

            //Assert
            Assert.That(!actual, "Did not return false on input containing <3 elements.");
        }

        [Test]
        public void IsValidRequestLineIsFalseOnMoreThan3Elements()
        {
            //Arrange
            string[] requestLine = "GET HTTP/1.1 Bimini Bumi".
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Act

            MethodInfo testMethod = GetMethod("IsValidRequestLine");
            bool actual = (bool)testMethod.Invoke(httpRequest, new object[] { requestLine });

            //Assert
            Assert.That(!actual, "Did not return false on input containing >3 elements.");
        }

        [Test]
        public void IsValidRequestLineIsFalseOnMisplacedHTTPVersion()
        {
            //Arrange
            string[] requestLine = "GET HTTP/1.1 Niki".
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Act

            MethodInfo testMethod = GetMethod("IsValidRequestLine");
            bool actual = (bool)testMethod.Invoke(httpRequest, new object[] { requestLine });

            //Assert
            Assert.That(!actual, "Did not return false when third element was not correct.");
        }

        [Test]
        public void IsValidRequestLineIsTrueOnValidRequestLine()
        {
            //Arrange
            string[] requestLine = "GET /separator/index.html HTTP/1.1".
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Act

            MethodInfo testMethod = GetMethod("IsValidRequestLine");
            bool actual = (bool)testMethod.Invoke(httpRequest, new object[] { requestLine });

            //Assert
            Assert.That(actual, "Valid input returns false. Check logic.");
        }

        [Test]
        public void ParseRequestMethodSetsRequestMethodCorrectly()
        {

            string[] request = "Post url HTTP/1.1".Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var expected = HttpRequestMethod.Post;

            MethodInfo testMethod = GetMethod("ParseRequestMethod");
            testMethod.Invoke(httpRequest, new object[] { request });

            var actual = httpRequest.RequestMethod;

            Assert.AreEqual(expected, actual, "Http Request method enum is not set correctly");

        }

        [Test]
        public void ParseHeadersThrowsBadRequestExceptionOnMissingHost()
        {
            string[] request = missingHostRequest.Skip(1).ToArray();

            MethodInfo testMethod = GetMethod("ParseHeaders");

            

            Assert.That(() => testMethod.Invoke(httpRequest, new object[] { request }),
                Throws.TargetInvocationException.With.InnerException.Message.EqualTo("The Request was malformed or contains unsupported elements."), "Did not throw expected exception.");
        }

        [Test]
        public void ParseHeadersAddsHeaderSuccessfully()
        {
            string[] request = getRequest.Skip(1).ToArray();

            MethodInfo testMethod = GetMethod("ParseHeaders");

            string expected = "Host: localhost:8000";

            testMethod.Invoke(httpRequest, new object[] { request });

            HttpHeader actualHeader = httpRequest.Headers.GetHeader("Host");

            if(actualHeader == null)
            {
                Assert.Fail("The header was not set correctly.");
            }

            string actual = actualHeader.ToString();

            Assert.AreEqual(expected, actual, "Hedaer was not set correctly", new[] { expected, actual });



        }

        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
                Assert.Fail("methodName cannot be null or whitespace");

            var method = this.httpRequest.GetType()
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
                Assert.Fail(string.Format("{0} method not found", methodName));

            return method;
        }
    }
}