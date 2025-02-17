using System;
using System.Runtime.Serialization;

namespace ECommerce.Infrastructure.Persistence.Exeptions
{
	public class DatabaseValidationException : Exception
	{
		public DatabaseValidationException()
		{
		}

        public DatabaseValidationException(string? message) : base(message)
        {
            //log
            //result
        }

        public DatabaseValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
            //log
            //result
        }

        protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            //log
            //result
        }
    }
}

