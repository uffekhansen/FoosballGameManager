using System;

namespace Domain.Exceptions
{
	public class AlreadyExistsException : Exception
	{
		public AlreadyExistsException(string message)
			: base(message)
		{
		}
	}
}
