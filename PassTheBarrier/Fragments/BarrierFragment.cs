using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarrier.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("passTheBarrier.droid.fragments.BarrierFragment")]
    public class BarrierFragment : BaseFragment<BarrierViewModel>
    {
        protected override int FragmentId => Resource.Layout.BarrierView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			ParentActivity.SupportActionBar.Title = GetString(Resource.String.barrier);

			return view;
		}
    }
}