using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string ErrorText = "The server has encountered an error.";

        public InternalServerErrorException() : base(ErrorText) { }

    }
}
