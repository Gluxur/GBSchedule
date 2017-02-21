using System;
using System.Collections.Generic;
using Android.App.Job;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using System.Threading;

namespace GBSchedule
{
    [Service(Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    public class TestJobService : JobService
    {
        private static string Tag = "SyncService";      
        MainActivity mainActivity;

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Info(Tag, "Service created");
        }

        public override void OnDestroy()
        {
            Log.Info(Tag, "Service destroyed");
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, Android.App.StartCommandFlags flags, int startId)
        {

            var callback = (Messenger)intent.GetParcelableExtra("messenger");
            var m = Message.Obtain();           
            m.Obj = this;
            try
            {
                callback.Send(m);
            }
            catch (RemoteException e)
            {
                Log.Error(Tag, e, "Error passing service object back to activity.");
            }



            Toast.MakeText(this, "OnStartCommand", ToastLength.Short).Show();
            return StartCommandResult.Sticky;
        }

        public override bool OnStartJob(JobParameters args)
        {       

            if (mainActivity != null)
            {
                mainActivity.OnReceivedStartJob(args);
                Console.WriteLine("creating thread");
                Thread t = new Thread(() =>
                {

                    Console.WriteLine("executing ThreadProc");
                    try
                    {
                        while (true)
                        {
                            Looper.Prepare();                         
                            Toast.MakeText(this, "loop OnStartJob", ToastLength.Short).Show();
                            Looper.Loop();
                            Thread.Sleep(5000);
                        }

                    }
                    finally
                    {
                        Console.WriteLine("finished executing ThreadProc");
                    }
                });
                t.IsBackground = true;
                t.Start();
                Console.WriteLine("started thread");







               
            }

            while (true)
            {
                

            }



            Log.Info(Tag, "on start job: " + args.JobId);
            Toast.MakeText(this, "on start job: " + args.JobId, ToastLength.Short).Show();
            //JobFinished(args, false);
            return true;
        }

        public override bool OnStopJob(JobParameters args)
        {
            if (mainActivity != null)
            {
                mainActivity.OnReceivedStopJob();
            }
            Log.Info(Tag, "on stop job: " + args.JobId);
            Toast.MakeText(this, "on stop job: " + args.JobId, ToastLength.Short).Show();
            return true;
        }
        public void setUiCallback(MainActivity activity)
        {
            mainActivity = activity;

            //OnStartJob(null);
        }

        /** Send job to the JobScheduler. */
        public void ScheduleJob(JobInfo t, JobScheduler tm)
        {
            var status = tm.Schedule(t);
            Toast.MakeText(this, "Scheduling job: " + (status == JobScheduler.ResultSuccess ? "Success" : "Failure"), ToastLength.Short).Show();

        }
    }
}