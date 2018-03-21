namespace PassTheBarier.Core.Logic.Interfaces
{
	public interface IModalLogic
	{
		int DisplayToast(string message);
		void DisplayLoading();
		void HideLoading();
	}
}