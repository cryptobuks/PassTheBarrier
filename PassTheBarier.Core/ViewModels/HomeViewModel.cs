using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarier.Core.Messenger;

namespace PassTheBarier.Core.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IBarrierLogic _barrierLogic;
		private readonly IActionHelper _actionHelper;
		private readonly MvxSubscriptionToken _barrierSubscriptionToken;

		private BarrierModel _barrier;
		public BarrierModel Barrier
		{
			get => _barrier;
			set
			{
				_barrier = value;
				RaisePropertyChanged(() => Barrier);
			}
		}

		public HomeViewModel(IBarrierLogic barrierLogic, IActionHelper actionHelper, IMvxMessenger messenger)
		{
			_barrierLogic = barrierLogic;
			_actionHelper = actionHelper;

			LoadDataCommand = new MvxAsyncCommand(() => _actionHelper.DoAction(LoadData));
			_barrierSubscriptionToken = messenger.Subscribe<BarrierMessage>(OnBarrierMessageReceived);
		}

		public IMvxAsyncCommand LoadDataCommand { get; set; }

		private bool _isServiceRunning;

		public bool IsServiceRunning
		{
			get => _isServiceRunning;
			set
			{
				_isServiceRunning = value;
				RaisePropertyChanged(() => IsServiceRunning);
			}
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			await LoadData();
		}

		private void OnBarrierMessageReceived(BarrierMessage barrierMessage)
		{
			Barrier = barrierMessage.Barrier;
		}

		private async Task LoadData()
		{
			Barrier = await _barrierLogic.GetBarrierAsync();
		}
	}
}