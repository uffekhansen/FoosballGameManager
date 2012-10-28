using Domain.Extensions;
using FluentAssertions;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Extensions
{
	public class IntExtensionsTest
	{
		private int _numberOfTimesInvoked;

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(8)]
		public void Given_Action_When_Calling_TimesDo_Then_Action_Is_Invoked_Exected_Number_Of_times(int expectedNumberOfInvokes)
		{
			expectedNumberOfInvokes.TimesDo(FunctionToInvoke);

			_numberOfTimesInvoked.Should().Be(expectedNumberOfInvokes);
		}

		private void FunctionToInvoke()
		{
			++_numberOfTimesInvoked;
		}
	}
}
