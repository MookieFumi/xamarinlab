using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using xamarinlab.Model.model;

namespace xamarinlab.Droid.Resources.adapters
{
    public class MembersListViewAdapter : BaseAdapter<Member>
    {
        readonly List<Member> _items;
        readonly Activity _activity;

        public MembersListViewAdapter(Activity activity, List<Member> items)
            : base()
        {
            this._activity = activity;
            this._items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = convertView;
            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Resource.Layout.MembersListView, null);
            }

            view.FindViewById<TextView>(Resource.Id.txtName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.txtSurname).Text = item.Surname;
            view.FindViewById<TextView>(Resource.Id.txtDepartment).Text = item.Departament;

            var imageIdentifier = _activity.Resources.GetIdentifier("user", "drawable", _activity.PackageName);
            view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(imageIdentifier);

            return view;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override Member this[int position]
        {
            get { return _items[position]; }
        }
    }
}
