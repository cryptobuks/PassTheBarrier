using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.ViewModels
{
	public abstract class InputViewModel: BaseViewModel
	{
		private ObservableDictionary<string, string> _errors;

		public ObservableDictionary<string, string> Errors
		{
			get => _errors;
			set
			{
				_errors = value;
				RaisePropertyChanged(() => Errors);
			}
		}

		protected abstract bool IsValid();
	}

	public abstract class InputViewModel<T> : BaseViewModel<T>
	{
		private ObservableDictionary<string, string> _errors;

		public ObservableDictionary<string, string> Errors
		{
			get => _errors;
			set
			{
				_errors = value;
				RaisePropertyChanged(() => Errors);
			}
		}

		protected abstract bool IsValid();
	}

	public abstract class InputViewModel<TParameter, TResult> : BaseViewModel<TParameter, TResult>
	{
		private ObservableDictionary<string, string> _errors;

		public ObservableDictionary<string, string> Errors
		{
			get => _errors;
			set
			{
				_errors = value;
				RaisePropertyChanged(() => Errors);
			}
		}

		protected abstract bool IsValid();
	}
}