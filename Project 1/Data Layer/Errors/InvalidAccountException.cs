using System;

namespace Data_Layer.Errors
{
    [Serializable]
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException() { }
 
        public InvalidAccountException(string message) : base(message) { }
        
        public InvalidAccountException(string message, Exception inner) : base(message, inner) { }
        
        protected InvalidAccountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
