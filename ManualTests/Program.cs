using System;
using System.Collections.Generic;

namespace ManualTests
{
	public class test
	{
		public int PlayersPerTeam
		{
			get;
			set;
		}

		public int Players
		{
			get;
			set;
		}

		public test(int players,  int playersPerTeam)
		{
			Players = players;
			PlayersPerTeam = playersPerTeam;
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var list = new List<test>
			{
				new test(1, 1),
				new test( 2, 2),
				new test( 2, 1),
				new test( 2, 2),
				new test( 5, 1),
				new test( 6, 2),
			};

			foreach (var test in list)
			{
				Console.WriteLine(string.Format("{1} % {0} = {2}", test.PlayersPerTeam, test.Players, test.Players % test.PlayersPerTeam));
			}

			Console.ReadKey();
		}
	}
}
