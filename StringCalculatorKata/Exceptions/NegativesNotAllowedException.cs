using System;

namespace StringCalculatorKata.Exceptions
{
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(string? message) 
            : base(message)
        {
        }
    }
}
