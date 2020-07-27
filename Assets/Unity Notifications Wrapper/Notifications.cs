using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;

public class Notifications : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    private int NotificationDelay;
    // Start is called before the first frame update
    private void InitializeNotifications()
    {
        GameNotificationChannel channel = new GameNotificationChannel("Notifications", "LOSE TOP 10", "Notification of out of TOP10");
        notificationsManager.Initialize(channel);
    }
    public void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationsManager.CreateNotification();
        if(notification!=null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;
            notificationsManager.ScheduleNotification(notification);
        }

    }
    public void CreateNotification()
    {
        CreateNotification("DODO AR", "Ты вылетел из топ 10", DateTime.Now.AddSeconds(1));

    }
    void Start()
    {
        InitializeNotifications();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
