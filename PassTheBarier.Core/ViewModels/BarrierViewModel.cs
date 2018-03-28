using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using MvvmValidation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarier.Core.Messenger;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : InputViewModel
    {
		private readonly IModalLogic _modalLogic;
		private readonly IBarrierLogic _barrierLogic;
	    private readonly IMvxNavigationService _navigationService;
	    private readonly IMvxMessenger _messenger;

		public IMvxCommand LoadBarrierCommand { get; }
		public IMvxCommand SaveBarrierCommand { get; }

		public BarrierViewModel(IBarrierLogic barrierLogic, IModalLogic modalLogic, IMvxNavigationService navigationService, IMvxMessenger messenger)
		{
			_barrierLogic = barrierLogic;
			_modalLogic = modalLogic;
			_navigationService = navigationService;
			_messenger = messenger;

			SaveBarrierCommand = new MvxAsyncCommand(SaveBarrier);
			LoadBarrierCommand = new MvxAsyncCommand(LoadBarrier);
		}

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

	    public override Task Initialize()
	    {
			LoadBarrierCommand.Execute();

			return base.Initialize();
	    }

	    private async Task LoadBarrier()
		{
			_modalLogic.DisplayLoading();
			var barrier = await _barrierLogic.GetBarrierAsync();
			_modalLogic.HideLoading();
			if (barrier == null)
			{
				barrier = new BarrierModel();
			}
			Barrier = barrier;
		}

		private async Task SaveBarrier()
		{
			if (IsValid())
			{
				_modalLogic.DisplayLoading();
				await _barrierLogic.SaveBarrierAsync(Barrier);
				var message = new BarrierMessage(
					this,
					Barrier
				);
				_messenger.Publish(message);
				_modalLogic.HideLoading();
				_modalLogic.DisplayToast(Messages.BarrierSavedSuccessfully);

				await _navigationService.Close(this);
			}
		}

	    protected override bool IsValid()
	    {
		    var validator = new ValidationHelper();
		    var regex = new Regex(Constants.NumberRegex);

		    validator.AddRequiredRule(() => Barrier.Number, Messages.FieldRequired);
		    validator.AddRule(() => Barrier.Number,
			    () => RuleResult.Assert(regex.IsMatch(Barrier.Number), Messages.InvalidNumber));

		    validator.AddRequiredRule(() => Barrier.MessageText, Messages.FieldRequired);

		    var result = validator.ValidateAll();
		    Errors = result.AsObservableDictionary();

		    return result.IsValid;
		}
    }
}