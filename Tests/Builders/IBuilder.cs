namespace Tests.Builders
{
	public interface IBuilder<T> where T : new()
	{
		T Build();
	}
}
