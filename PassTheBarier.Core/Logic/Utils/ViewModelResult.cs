namespace PassTheBarier.Core.Logic.Utils
{
	public class ViewModelResult<T>
	{
		public ViewModelResult()
		{
			
		}

		public ViewModelResult(T response)
		{
			Response = response;
		}

		public T Response { get; set; }
	}
}