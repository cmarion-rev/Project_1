using System;

namespace Data_Layer.Errors
{
    [Serializable]
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() { }
 
        public InvalidAmountException(string message) : base(message) { }
        
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
        
        protected InvalidAmountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}