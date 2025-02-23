using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Helper
{
    public class PushNotifyHelper
    {
        // Test Send MO notifications
        public static async Task SendNotification(string title, string body)
        {
            var topic = "Registration";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                //Token = "fn_cAty6RbeNwwrOg_lUYK:APA91bHIUtiDWcuUDI9405Y91f5fPWdOUbjTy2UnNRwTv3UY5VCi8vkI-F25xlrSHVEitZWVPBWGWLI7uFrbQWfKDJJhqcDNr59xbs9iZXTJhYi7jaRdf8E",
                Topic = topic,
            };

            // Send a message to the devices subscribed to the provided topic.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + response);

        }
    }
}
