using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Assertions;

namespace Tests.Extensions
{
	public static class FluentAssertionsExtensions
	{
		public static AndConstraint<GenericCollectionAssertions<T>> ContainExactlyInOrder<T>(this GenericCollectionAssertions<T> @this, IEnumerable<T> expectedItemsList)
		{
			return @this.ContainInOrder(expectedItemsList).And.HaveSameCount(expectedItemsList);
		}
	}
}
