using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

/// <summary>
/// Standard Android Notification Handler. 
/// Only notificaiton currently is when the energy timer goes off and is replenished.
/// </summary>

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannelId = "notification_channel";

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool focus)
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Channel for Energy Alerts",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Your Energy is fully recharged! Time to get Risky!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }
#endif
}
