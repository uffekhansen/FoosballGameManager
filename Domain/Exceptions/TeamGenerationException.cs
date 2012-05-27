using System;

namespace Domain.Exceptions
{
	public class TeamGenerationException : Exception
	{
		public TeamGenerationException(string message)
			: base(message)
		{
		}
	}
}