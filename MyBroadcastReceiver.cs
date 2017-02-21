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

namespace GBSchedule
{
    [BroadcastReceiver(Permission = "android.permission.WAKE_LOCK")]
    public class MyBroadcastReceiver : BroadcastReceiver
    {       
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wl = pm.NewWakeLock(WakeLockFlags.Partial, "");
            wl.Acquire();
            Toast.MakeText(context, "MyAlarmManager:OnReceive " + SessionStatic.counter, ToastLength.Short).Show();

            var uri = Android.Net.Uri.Parse("http://www.xamarin.com");
            var newIntent = new Intent(Intent.ActionView, uri);
            context.StartActivity(newIntent);

            SetAlarm(context, SessionStatic.alertTime);
            SessionStatic.counter++;
            wl.Release();
        }
        public void SetAlarm(Context context, int alertTime)
        {
            SessionStatic.alertTime = alertTime;
            long now = SystemClock.ElapsedRealtime();
            AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(context, this.Class);
            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);
            am.Set(AlarmType.ElapsedRealtimeWakeup, now + ((long)(alertTime * 1000)), pi);
        }
        public void CancelAlarm(Context context)
        {
            Intent intent = new Intent(context, this.Class);
            PendingIntent sender = PendingIntent.GetBroadcast(context, 0, intent, 0);
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(sender);
        }
    }
}