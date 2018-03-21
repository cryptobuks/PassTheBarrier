using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : InputViewModel
    {
	    private readonly IModalLogic _modalLogic;
		private readonly IBarrierLogic _barrierLogic;
	    private readonly IMvxNavigationService _navigationService;

		public IMvxCommand LoadBarrierCommand { get; }
		public IMvxCommand SaveBarrierCommand { get; }

		public BarrierViewModel(IBarrierLogic barrierLogic, IModalLogic modalLogic, IMvxNavigationService navigationService)
		{
			_barrierLogic = barrierLogic;
			_modalLogic = modalLogic;
			_navigationService = navigationService;

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