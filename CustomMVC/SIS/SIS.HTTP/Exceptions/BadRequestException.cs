using System;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string ErrorText = "The Request was malformed or contains unsupported elements.";
        public BadRequestException() : base(ErrorText) { }
        
    }
}
