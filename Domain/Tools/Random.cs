using System;

namespace Domain.Tools
{
	public class FoosballRandom : IRandom
	{
		private readonly Random _random = new Random();

		public int Next(int max)
		{
			return _random.Next(max);
		}
	}
}