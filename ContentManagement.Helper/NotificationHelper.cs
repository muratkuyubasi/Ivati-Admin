using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;

namespace ContentManagement.Helper
{
    public class NotificationHelper
    {

        //public static void CreateSegment(string name, string id)
        //{
        //    var appConfig = new Configuration();
        //    appConfig.BasePath = "https://onesignal.com/api/v1";
        //    appConfig.AccessToken = "ZWFhODIxMDYtNTgzZi00YmU4LWFjOWItNTk4ODI1YjMwZDk0";
        //    var appInstance = new DefaultApi(appConfig);
        //    var appId = "eb96ffde-2d83-45f8-9077-b326cd4bc791";
        //    var segment = new Segment(name:name,filters:new List<FilterExpressions>());
        //    //segment.Name = name;
        //    //segment.Id = id;
        //    //segment.Filters = new List<FilterExpressions>();


        //    //segment.Filters = new List<FilterExpressions>({
        //    //    field = null,
        //    //    key = null,
        //    //    value = null,
        //    //    RelationEnum relation = 0
        //    //});

        //    try
        //    {
        //        // Create Segments
        //        CreateSegmentSuccessResponse result = appInstance.CreateSegments(appId, segment);
        //        //Debug.WriteLine(result);
        //    }
        //    catch (ApiException e)
        //    {
        //        //Debug.Print("Exception when calling DefaultApi.CreateSegments: " + e.Message);
        //        //Debug.Print("Status Code: " + e.ErrorCode);
        //        //Debug.Print(e.StackTrace);
        //    }
        //}

        //public static void CreatePlayer()
        //{
        //    var appConfig = new Configuration();
        //    appConfig.BasePath = "https://onesignal.com/api/v1";
        //    appConfig.AccessToken = "ZWFhODIxMDYtNTgzZi00YmU4LWFjOWItNTk4ODI1YjMwZDk0";
        //    var appInstance = new DefaultApi(appConfig);
        //    var appId = "eb96ffde-2d83-45f8-9077-b326cd4bc791";

        //    var player = new Player(); // Player | 
        //    try
        //    {
        //        // Add a device
        //        CreatePlayerSuccessResponse result = appInstance.CreatePlayer(player);
        //    }
        //    catch (ApiException e)
        //    {
        //        //Debug.Print("Exception when calling DefaultApi.CreatePlayer: " + e.Message);
        //        //Debug.Print("Status Code: " + e.ErrorCode);
        //        //Debug.Print(e.StackTrace);
        //    }


        //}

        public async static void SendNotification(string title, string subTitle, string body, List<string> users)
        {
            var appConfig = new Configuration();
            appConfig.BasePath = "https://onesignal.com/api/v1";
            appConfig.AccessToken = "NzkzN2Q3ODktMTI0MC00MGI2LWI5ZmItNTBjMmE4NDI5MzVk";
            var appInstance = new DefaultApi(appConfig);

            var appId = "eb96ffde-2d83-45f8-9077-b326cd4bc791";


            try
            {
                var notification = new Notification(appId: appId)
                {
                    Contents = new StringMap(en: body),
                    Subtitle = new StringMap(en: subTitle),
                    IncludeExternalUserIds = users,
                    Headings = new StringMap(en: title)

                };

                var result = appInstance.CreateNotification(notification);
                var response = await appInstance.CreateNotificationAsync(notification);
            }
            catch (Exception e)
            {
                //_logger.LogError(e.Message, e);
            }

        }

        public async static void SendAllNotification(string title, string subTitle, string body)
        {
            Configuration appConfig = new Configuration();
            appConfig.BasePath = "https://onesignal.com/api/v1";
            appConfig.AccessToken = "NzkzN2Q3ODktMTI0MC00MGI2LWI5ZmItNTBjMmE4NDI5MzVk";
            var appInstance = new DefaultApi(appConfig);
            var appId = "eb96ffde-2d83-45f8-9077-b326cd4bc791";



            try
            {
                var notification = new Notification(appId:appId)
                {
                    Contents = new StringMap(en: body),
                    Subtitle = new StringMap(en: subTitle),
                    Headings = new StringMap(en: title),
               
                    //IncludedSegments = new List<string> { "Subscribed Users" }
                };
                CreateNotificationSuccessResponse result = await appInstance.CreateNotificationAsync(notification);
            }
            catch (Exception e)
            {
                //_logger.LogError(e.Message, e);
            }

        }

        public  static void SendN()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://onesignal.com/api/v1";
            // Configure Bearer token for authorization: app_key
            config.AccessToken = "NzkzN2Q3ODktMTI0MC00MGI2LWI5ZmItNTBjMmE4NDI5MzVk";

            var appId = "eb96ffde-2d83-45f8-9077-b326cd4bc791";

            var apiInstance = new DefaultApi(config);
            var notification = new Notification(appId:appId)
            {
                Contents = new StringMap(en: "Demo"),
                //Subtitle = new StringMap(en: "Demo"),
                //Headings = new StringMap(en: "Demo")

                //IncludedSegments = new List<string> { "Subscribed Users" }
            };
            try
            {
                // Create notification
                var result =  apiInstance.CreateNotification(notification);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.CreateNotification: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
