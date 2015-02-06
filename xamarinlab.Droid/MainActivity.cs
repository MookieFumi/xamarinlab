using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using xamarinlab.Droid.Resources.adapters;
using xamarinlab.Model.model;
using xamarinlab.Services;
using LoginScreen;

namespace xamarinlab.Droid
{
    [Activity(Label = "Main", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private List<Member> _members = new List<Member>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.getMembers);
            button.Click += async (sender, e) =>
            {
                _members = await new MembersService().GetAll();
                LoadMembers(_members);
            };

            var btnClear = FindViewById<Button>(Resource.Id.btnClear);
            btnClear.Click += (sender, e) =>
            {
                _members = Enumerable.Empty<Member>().ToList();
                LoadMembers(_members);
            };

            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += (sender, e) =>
            {
				LoginScreenControl<CredentialsProvider>.Activate(this);
            };
        }

        private void LoadMembers(List<Member> members)
        {
            var lstMembers = FindViewById<ListView>(Resource.Id.lstMembers);
            lstMembers.Adapter = new MembersListViewAdapter(this, members);
            lstMembers.ItemClick += (o, args) =>
            {
                var l = _members[args.Position];
                Android.Widget.Toast.MakeText(this, l.Name + ' ' + l.Surname, Android.Widget.ToastLength.Short).Show();
            };
        }
    }
}

