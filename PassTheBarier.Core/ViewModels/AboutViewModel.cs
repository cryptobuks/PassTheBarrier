using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
	    private string _applicationVersion;
		public string ApplicationVersion
		{
			get => _applicationVersion;
			set
			{
				_applicationVersion = value;
				RaisePropertyChanged(() => ApplicationVersion);
			}
		}

	    public AboutViewModel(IAppInfo appInfo)
	    {
		    ApplicationVersion = appInfo.Version;
	    }
    }
}