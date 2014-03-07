using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;

namespace CheckIn
{
    [Activity(Label = "CheckIn", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        bool Running = false;
        BluetoothAdapter bt;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            bt = BluetoothAdapter.DefaultAdapter;

            Receiver receiver = new Receiver();
            var filter = new IntentFilter(BluetoothDevice.ActionFound);
            RegisterReceiver(receiver, filter);
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.ScanButton);

            button.Click += delegate 
            {
                if (Running)
                {
                    button.Text = "Start Scan";
                    bt.CancelDiscovery();
                }
                else
                {
                    button.Text = "Stop Scan";
                    bt.StartDiscovery();
                }
                Running = !Running;
            };
        }

        private class Receiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                var device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                throw new NotImplementedException();
            }
        }

    }
}

