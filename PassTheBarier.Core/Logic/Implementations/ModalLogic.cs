using Acr.UserDialogs;
using PassTheBarier.Core.Logic.Interfaces;

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
			toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
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