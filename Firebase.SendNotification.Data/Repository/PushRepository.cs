using Firebase.SendNotification.Data.Enum;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Firebase.SendNotification.Data.Repository
{
    public class PushRepository
    {
        private void SendNotification(string applicationID, string target, string msgTitle, string msgBody, FirebaseNotificationType type)
        {
            string Result = string.Empty;

            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                #region payload
                var payloadNotification = new
                {
                    to = target,
                    notification = new
                    {
                        body = msgBody,
                        title = msgTitle,
                    }
                };
                var payloadData = new
                    {
                        to = target,
                        data = new
                        {
                            body = msgBody,
                            title = msgTitle,
                        }
                    };
                #endregion

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = string.Empty;

                if (type == FirebaseNotificationType.data)
                    json = serializer.Serialize(payloadData);
                else if (type == FirebaseNotificationType.notification)
                    json = serializer.Serialize(payloadNotification);
                else
                    json = serializer.Serialize(payloadNotification);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                Result = tReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }

            Console.WriteLine(Result);
            Console.WriteLine("\n\nHit Enter to Exit");
            Console.ReadLine();
        }

        public void SendToFirebase(string applicationID, string target, string title, string body, FirebaseNotificationType type = FirebaseNotificationType.notification)
        {
            SendNotification(applicationID, target, title, body, type);
        }
    }
}
