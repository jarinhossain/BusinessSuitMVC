using System.Collections.Generic;
using System.Net.Http;

namespace BusinessSuitMVC.ModelClasses
{
    internal class asyncService
    {
        
        private static readonly HttpClient client = new HttpClient();
        public static string responseString = "";
        public static string responseStringFull = "";

        public asyncService()
        {

        }

        public async void sendSingleSmsMethod2()//,string token
        {
            //environment.newline counts '\n' as 2 character. which is same to sms gateway
            // or we can use \r\n and this is same as using environment.newline
            //string message = "প্রিয় শিক্ষার্থীবৃন্দ," + Environment.NewLine + "সাময়িক অসুবিধার কারণে শনিবার(০৩/০৩/২০১৮) স্কুলে আইডি কার্ড বিতরণ করা হবে না।" + Environment.NewLine + "-সালাহউদ্দিন আহমেদ উচ্চ বিদ্যালয়";
            string token = "9b8a6934e6c7e83dc79728274677b8f2";
            string number = "01676797123";
            string message = "test";
            Dictionary<string, string> values = new Dictionary<string, string>(){
                                           { "token", token },
                                           { "to", number },
                                           { "message", message }
                                        };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://sms.greenweb.com.bd/api.php?", content);

            responseString = await response.Content.ReadAsStringAsync();


        }
    }
}