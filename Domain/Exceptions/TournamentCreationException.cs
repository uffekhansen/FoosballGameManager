using System;

namespace Domain.Exceptions
{
	public class TournamentCreationException : Exception
	{
		public TournamentCreationException(string message)
			: base(message)
		{
		}
	}
}
