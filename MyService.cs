using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App.Job;

namespace GBSchedule
{
    [Service(Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    class MyService : JobService
    {
        MyBroadcastReceiver myBroadcastReceiver = new MyBroadcastReceiver();


        public override bool OnStartJob(JobParameters @params)
        {
            myBroadcastReceiver.SetAlarm(this, 30);
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, Android.App.StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }


        }
}