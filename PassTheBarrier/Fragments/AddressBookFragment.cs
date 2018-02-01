using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarrier.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("passTheBarrier.droid.fragments.AddressBookFragment")]
    public class AddressBookFragment : BaseFragment<AddressBookViewModel>
    {
        protected override int FragmentId => Resource.Layout.AddressBookView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = GetString(Resource.String.address_book);

            return view;
        }
    }
}