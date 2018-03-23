using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarrier.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("passTheBarrier.droid.fragments.ContactFragment")]
    public class ContactFragment : BaseFragment<ContactViewModel>
    {
        protected override int FragmentId => Resource.Layout.ContactView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			ParentActivity.SupportActionBar.Title = GetString(Resource.String.contact);

	        return view;
        }
    }
}