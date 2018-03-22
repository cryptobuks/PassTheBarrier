using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;

namespace PassTheBarier.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
	    private IBarrierLogic _barrierLogic;
	    private IContactLogic _contactLogic;

	    public HomeViewModel(IContactLogic contactLogic, IBarrierLogic barrierLogic)
	    {
		    _contactLogic = contactLogic;
		    _barrierLogic = barrierLogic;

			StartServiceCommand = new MvxCommand(StartService);
			StopServiceCommand = new MvxCommand(StopService);
	    }

	    public IMvxCommand StartServiceCommand { get; set; }
	    public IMvxCommand StopServiceCommand { get; set; }

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

	    private void StartService()
	    {
		    IsServiceRunning = true;
	    }

	    private void StopService()
	    {
		    IsServiceRunning = false;
	    }
    }
}