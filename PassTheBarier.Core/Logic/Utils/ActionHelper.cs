using System;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace PassTheBarier.Core.Logic.Utils
{
	public class ActionHelper : IActionHelper
	{
		private readonly IUserDialogs _userDialogs;

		public ActionHelper(IUserDialogs userDialogs)
		{
			_userDialogs = userDialogs;
		}

		public async Task DoAction(Func<Task> action)
		{
			try
			{
				DisplayLoading();

				await action.Invoke();
			}
			catch (Exception ex)
			{
				await DisplayErrorModal(ex.Message);
			}
			finally
			{
				HideLoading();
			}
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

		private void DisplayLoading()
		{
			_userDialogs.ShowLoading(ResourcesFiles.Messages.Loading);
		}

		private void HideLoading()
		{
			_userDialogs.HideLoading();
		}

		private async Task DisplayErrorModal(string message)
		{
			await _userDialogs.AlertAsync(message, ResourcesFiles.Messages.Error, ResourcesFiles.Messages.Ok);
		}
	}
}