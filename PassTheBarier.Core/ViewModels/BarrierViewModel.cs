using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : BaseViewModel
    {
		private IUserDialogs _userDialogs;
		private IBarrierLogic _barrierLogic;
		private IMvxNavigationService _navigationService;

		public IMvxCommand FetchBarrierCommand { get; private set; }
		public IMvxCommand SaveBarrierCommand { get; private set; }

		public MvxNotifyTask FetchBarrierTask { get; private set; }
		public MvxNotifyTask SaveBarrierTask { get; private set; }

		public BarrierViewModel(IUserDialogs userDialogs, IBarrierLogic barrierLogic, IMvxNavigationService navigationService)
		{
			_userDialogs = userDialogs;
			_barrierLogic = barrierLogic;
			_navigationService = navigationService;

			FetchBarrierCommand = new MvxCommand(
			() =>
			{
				FetchBarrierTask = MvxNotifyTask.Create(LoadBarrier);
				RaisePropertyChanged(() => FetchBarrierTask);
			});

			SaveBarrierCommand = new MvxCommand(
			() =>
			{
				SaveBarrierTask = MvxNotifyTask.Create(SaveBarrier);
				RaisePropertyChanged(() => SaveBarrierTask);
			});
		}

		private BarrierModel _barrier;
		public BarrierModel Barrier
		{
			get { return _barrier; }
			set
			{
				_barrier = value;
				RaisePropertyChanged(() => Barrier);
			}
		}

		private async Task LoadBarrier()
		{
			var barrier = await _barrierLogic.GetBarrierAsync();
			if (barrier == null)
			{
				barrier = new BarrierModel();
			}
			Barrier = barrier;
		}

		private async Task SaveBarrier()
		{
			//validation
			await _barrierLogic.SaveBarrierAsync(Barrier);
			var toastConfig = new ToastConfig("Toasting...");
			toastConfig.SetDuration(3000);
			toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
			_userDialogs.Toast(toastConfig);
		}
	}
}