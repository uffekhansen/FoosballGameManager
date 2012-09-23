using System;

namespace Domain.Extensions
{
	public static class IntExtensions
	{
		public static void TimesDo(this int @this, Action action)
		{
			for (int i = 0; i < @this; i++)
			{
				action();
			}
		}
	}
}
