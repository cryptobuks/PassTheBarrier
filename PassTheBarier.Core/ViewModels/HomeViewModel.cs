using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Messenger;

namespace PassTheBarier.Core.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IModalLogic _modalLogic;
		private readonly IBarrierLogic _barrierLogic;
		private readonly IContactLogic _contactLogic;
		private readonly MvxSubscriptionToken _barrierSubscriptionToken;
		private readonly MvxSubscriptionToken _contactsSubscriptionToken;

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

		public IEnumerable<ContactModel> Contacts { get; set; }

		public HomeViewModel(IContactLogic contactLogic, IBarrierLogic barrierLogic, IModalLogic modalLogic, IMvxMessenger messenger)
		{
			_contactLogic = contactLogic;
			_barrierLogic = barrierLogic;
			_modalLogic = modalLogic;

			LoadDataCommand = new MvxAsyncCommand(LoadData);
			TriggerBarrierStateCommand = new MvxAsyncCommand(TriggerBarrierState);
			_barrierSubscriptionToken = messenger.Subscribe<BarrierMessage>(OnBarrierMessageReceived);
			_contactsSubscriptionToken = messenger.Subscribe<ContactsMessage>(OnContactsMessageReceived);
		}

		public IMvxAsyncCommand LoadDataCommand { get; set; }

		public IMvxAsyncCommand TriggerBarrierStateCommand { get; set; }

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

		private void OnContactsMessageReceived(ContactsMessage contactsMessage)
		{
			Contacts = contactsMessage.Contacts;
		}

		private async Task LoadData()
		{
			_modalLogic.DisplayLoading();
			Barrier = await _barrierLogic.GetBarrierAsync();
			Contacts = await _contactLogic.GetAllAsync();
			if (Barrier != null)
			{
				IsServiceRunning = Barrier.Enabled;
			}
			_modalLogic.HideLoading();
		}

		private async Task TriggerBarrierState()
		{
			Barrier.Enabled = !Barrier.Enabled;
			await _barrierLogic.SaveBarrierAsync(Barrier);
			IsServiceRunning = Barrier.Enabled;
		}
	}
}