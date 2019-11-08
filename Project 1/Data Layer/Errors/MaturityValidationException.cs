using System;

namespace Data_Layer.Errors
{
    [Serializable]
    public class MaturityValidationException : Exception
    {
        public MaturityValidationException() { }
       
        public MaturityValidationException(string message) : base(message) { }
        
        public MaturityValidationException(string message, Exception inner) : base(message, inner) { }
        
        protected MaturityValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}