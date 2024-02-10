using System;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotificationHandler : MonoBehaviour
{
    #if UNITY_ANDROID
    private const string _channelId = "Notification-Channel";

    public void SchedualGameReadyNotification(DateTime whenGameIsReady)
    {
        AndroidNotificationChannel _notificationChannel = new()
        {
            Id = _channelId,
            Name = "Notification Channel",
            Description = "Test",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(_notificationChannel);

        AndroidNotification _readyNotification = new()
        {
            Title = "Fuel Toped",
            Text = "Your car is fueled and ready!!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = whenGameIsReady
        };

        AndroidNotificationCenter.SendNotification(_readyNotification, _channelId);
    }
    #endif
}
