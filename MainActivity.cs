using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App.Job;
using Java.Interop;

namespace GBSchedule
{
    [Activity(Label = "GBSchedule", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.Main);

                Button button = FindViewById<Button>(Resource.Id.schedule_button);
                button.Click += delegate { ScheduleJob(); };

                var serviceIntent = new Intent(this, typeof(MyService));
                StartService(serviceIntent);

             

                //Intent intent = new Intent(this, typeof(MyBroadcastReceiver));
                //PendingIntent pi = PendingIntent.GetBroadcast(this, 0, intent, 0);


                //if(SessionStatic.myBroadcastReceiver != null)
                //     button.Text = "Stop  O";
                //else
                //    button.Text = "Start O";
            }
            catch (Exception ex)
            {
                

            }
        }

        public void ScheduleJob()
        {
            try
            {



                //if (SessionStatic.myBroadcastReceiver == null)
                //{
                //   SessionStatic.myBroadcastReceiver = new MyBroadcastReceiver();
                //   SessionStatic.myBroadcastReceiver.SetAlarm(this, 10);
                //}
                //else
                //{
                //    SessionStatic.myBroadcastReceiver.CancelAlarm(this);
                //}

                //Button button = FindViewById<Button>(Resource.Id.schedule_button);
                //if (SessionStatic.myBroadcastReceiver != null)
                //    button.Text = "Stop  O";
                //else
                //    button.Text = "Start O";
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message,
                      ToastLength.Short).Show();

            }
        }
        public void cancelScheduleJob()
        {
           
        }       
    }
}

