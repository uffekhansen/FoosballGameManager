using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Extensions;
using FluentAssertions;
using Xunit;

namespace Tests.UnitTests.Domain.Extensions
{
	public class EnumerableExtensionsTest
	{
		[Fact]
		public void Given_Action_And_Strings_When_Calling_Each_Then_Action_Is_Invoked_For_Each_Element()
		{
			IEnumerable<int> items = new List<int> { 1, 2, 3 };
			var visited = new List<int>();
			Action<int> action = visited.Add;

			items.Each(action);

			visited.Should().ContainInOrder(items);
			visited.Should().HaveCount(items.Count());
		}
	}
}
