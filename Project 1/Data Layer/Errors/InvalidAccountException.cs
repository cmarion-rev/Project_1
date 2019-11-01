using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Database_Repository.Partials
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
