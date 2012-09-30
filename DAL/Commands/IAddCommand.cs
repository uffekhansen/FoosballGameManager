namespace DAL.Commands
{
	public interface IAddCommand<T>
	{
		void Execute(T t);
	}
}
