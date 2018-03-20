namespace PassTheBarier.Core.Logic.Interfaces
{
	public interface IModalLogic
	{
		void DisplayToast(string message);
		void DisplayLoading();
		void HideLoading();
	}
}