using System;

namespace Domain.Tools
{
	public class FoosBallRandom : IRandom
	{
		private readonly Random _random = new Random();

		public int Next(int max)
		{
			return _random.Next(max);
		}
	}
}