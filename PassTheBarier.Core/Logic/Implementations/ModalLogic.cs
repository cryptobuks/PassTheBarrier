using Acr.UserDialogs;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.Logic.Implementations
{
	public class ModalLogic : IModalLogic
	{
		private readonly IUserDialogs _userDialogs;

		public ModalLogic(IUserDialogs userDialogs)
		{
			_userDialogs = userDialogs;
		}

		public int DisplayToast(string message)
		{
			var toastConfig = new ToastConfig(message);
			toastConfig.SetDuration(2500);
			toastConfig.SetBackgroundColor(Constants.AccentColor); //accent color
			toastConfig.SetMessageTextColor(Constants.TextColor);
			_userDialogs.Toast(toastConfig);

			return 2500;
		}

		public void DisplayLoading()
		{
			_userDialogs.ShowLoading("Loading");
		}

		public void HideLoading()
		{
			_userDialogs.HideLoading();
		}
	}
}