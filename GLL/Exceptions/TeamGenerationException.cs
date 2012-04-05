using System;

namespace GLL.Exceptions
{
	public class TeamGenerationException : Exception
	{
		public TeamGenerationException(string message) 
			: base(message)
		{
		}
	}
}