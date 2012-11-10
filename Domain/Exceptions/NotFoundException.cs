using System;

namespace Domain.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string exceptionMessage) : base(exceptionMessage)
		{
		}
	}
}
