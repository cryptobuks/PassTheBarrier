using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;
using Refractored.Fab;

namespace PassTheBarrier.Fragments
{
	[MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
	[Register("passTheBarrier.droid.fragments.AddressBookFragment")]
	public class AddressBookFragment : BaseFragment<AddressBookViewModel>
	{
		protected override int FragmentId => Resource.Layout.AddressBookView;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			ParentActivity.SupportActionBar.Title = GetString(Resource.String.address_book);

			var listView = view.FindViewById<RecyclerView>(Resource.Id.contacts_recycler_view);
			var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToRecyclerView(listView);
			fab.Click += (sender, e) => { ViewModel.AddContactCommand.Execute(null); };

			return view;
		}
	}
}