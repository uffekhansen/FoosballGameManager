using System;

namespace GameLogic.Exceptions
{
	public class TeamGenerationException : Exception
	{
		public TeamGenerationException(string message)
			: base(message)
		{
		}
	}
}