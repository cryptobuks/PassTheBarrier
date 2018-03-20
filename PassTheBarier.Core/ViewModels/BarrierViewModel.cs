using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : BaseViewModel
    {
	    private readonly IModalLogic _modalLogic;
		private readonly IBarrierLogic _barrierLogic;

		public IMvxCommand LoadBarrierCommand { get; private set; }
		public IMvxCommand SaveBarrierCommand { get; private set; }

		public BarrierViewModel(IBarrierLogic barrierLogic, IModalLogic modalLogic)
		{
			_barrierLogic = barrierLogic;
			_modalLogic = modalLogic;

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
			//TODO: validation
			await _barrierLogic.SaveBarrierAsync(Barrier);
			_modalLogic.DisplayToast("Barrier saved successfully");
		}
	}
}