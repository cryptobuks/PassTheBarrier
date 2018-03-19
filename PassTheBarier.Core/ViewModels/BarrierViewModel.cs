using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : BaseViewModel
    {
		private IBarrierLogic _barrierLogic;
		private IMvxNavigationService _navigationService;

		public IMvxCommand FetchBarrierCommand { get; private set; }

		public MvxNotifyTask FetchBarrierTask { get; private set; }

		public BarrierViewModel(IBarrierLogic barrierLogic, IMvxNavigationService navigationService)
		{
			_barrierLogic = barrierLogic;
			_navigationService = navigationService;

			FetchBarrierCommand = new MvxCommand(
			() =>
			{
				FetchBarrierTask = MvxNotifyTask.Create(LoadBarrier);
				RaisePropertyChanged(() => FetchBarrierTask);
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
	}
}