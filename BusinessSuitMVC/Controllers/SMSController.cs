using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using Flurl.Http;

namespace BusinessSuitMVC.Controllers
{
    
    public class SMSController : Controller
    {
        private DBContext DB = new DBContext();

        [Authorize]
        [HttpGet]
        public ActionResult SingleSMS()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            var isClient = bool.Parse(Session["Is_Client"].ToString());
            if (isClient == true)
            {
                var clientId = int.Parse(Session["Profile_Id"].ToString());
                int sms = (int)DB.Client_Inventory.Where(x => x.Client_Id == clientId).Select(x => x.Free_Sms).FirstOrDefault();

                ViewData["free_sms"] = sms + " remaining sms";

            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SingleSMS(System.Web.Mvc.FormCollection collection)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            string token = "9b8a6934e6c7e83dc79728274677b8f2";
            string number = collection["nc"];
            string message = collection["message"];

            if (number == "" || number == null || number.Length != 11)
            {
                ViewBag.msg = "please enter e valid number";
                return View();
            }
            else if(message.Length > 134)
            {
                ViewBag.msg = "in test message you can send less than 134 character sms";
                return View();
            }

            var isClient = bool.Parse(Session["Is_Client"].ToString());
            
            Client_Inventory clientInventory = new Client_Inventory();
            if (isClient == true)///condition only applied for client
            {
                var clientId = int.Parse(Session["Profile_Id"].ToString());

                clientInventory = DB.Client_Inventory.Where(x => x.Client_Id == clientId).FirstOrDefault();
                ViewData["free_sms"] = clientInventory.Free_Sms + " remaining sms";
                if (clientInventory.Free_Sms <= 0)
                {
                    ViewData["msg"] = "You have finished your free sms";
                    return View();
                }

                clientInventory.Sms_Sent = clientInventory.Sms_Sent + 1;
                clientInventory.Free_Sms = clientInventory.Free_Sms - 1;
                clientInventory.Updated_By = clientId;
                clientInventory.Updated_On = DateTime.Now;
                DB.SaveChanges();
                ViewData["free_sms"] = clientInventory.Free_Sms + " remaining sms";
            }

            var result = SendSms(number, message, token);
            ViewBag.msg = result;

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult BulkSMS()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> BulkSMS(System.Web.Mvc.FormCollection collection)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            Task<string> result = null ;

            string token = "9b8a6934e6c7e83dc79728274677b8f2";
            string number = collection["nc"];
            string message = collection["message"];
            try
            {
                await Task.Run(() => result  = sendSingleSmsMethod3(number, message, token));
                // ViewBag.msg = result.;
                return Json( result.Result, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //sendSingleSmsMethod2(number, message, token);
            //result  = SendSms(number, message, token);
            

            
        }

        //public ActionResult Gizmos()
        //{
        //    ViewBag.SyncOrAsync = "Synchronous";
        //    var gizmoService = new asyncService();
        //    return View("Gizmos", gizmoService.GetGizmos());
        //}

        //public async Task<ActionResult> GizmosAsync()
        //{
        //    ViewBag.SyncOrAsync = "Asynchronous";
        //    var gizmoService = new asyncService();
        //    return View("Gizmos", await gizmoService.sendSingleSmsMethod2());
        //}
        public string SendSms(string mobile, string msg, string token)
        {
            string URL = string.Format("http://sms.greenweb.com.bd/api.php?token={0}&to={1}&message={2}", token, mobile, msg);
            string DATA = "";
            string response = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(DATA);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                            //return Json("ok " + response, JsonRequestBehavior.AllowGet);
                            return response;
                            //Console.Out.WriteLine(response);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }


            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(URL);

            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //// List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!

            return response;
        }
        ///in this method we can not send /,- type charaters for datetime
        public string sendSingleSmsMethod1(string number, string message, string token)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String to = number; //Recipient Phone Number multiple number must be separated by comma
                //String token = "9b8a6934e6c7e83dc79728274677b8f2"; //generate token from the control panel
                //প্রিয় শিক্ষার্থীবৃন্দ,\nসাময়িক অসুবিধার কারণে শনিবার(০৩/০৩/২০১৮) স্কুলে আইডি কার্ড বিতরণ করা হবে না।\n-সালাহউদ্দিন আহমেদ উচ্চ বিদ্যালয়
                //String message = System.Uri.EscapeUriString(@"প্রিয় শিক্ষার্থীবৃন্দ,\nসাময়িক অসুবিধার কারণে শনিবার(০৩-) স্কুলে আইডি কার্ড বিতরণ করা হবে না। -সালাহউদ্দিন আহমেদ উচ্চ বিদ্যালয়"); //do not use single quotation (') in the message to avoid forbidden result
                String url = "http://sms.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + HttpUtility.UrlEncodeUnicode(message)  ;
                request = WebRequest.Create(url);

                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                //Console.WriteLine(result);
                reader.Close();
                stream.Close();
                return result;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return exp.Message.ToString();
                ///Console.ReadKey();
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return "No response";
        }

        private static readonly HttpClient client = new HttpClient();
        public static string responseString = "";
        public static string responseStringFull = "";

        [HttpPost]
        //in method we can send /,- type charaters for datetime
        public async void sendSingleSmsMethod2(string number,string message, string token)//,string token
        {
            //environment.newline counts '\n' as 2 character. which is same to sms gateway
            // or we can use \r\n and this is same as using environment.newline
            //string message = "প্রিয় শিক্ষার্থীবৃন্দ," + Environment.NewLine + "সাময়িক অসুবিধার কারণে শনিবার(০৩/০৩/২০১৮) স্কুলে আইডি কার্ড বিতরণ করা হবে না।" + Environment.NewLine + "-সালাহউদ্দিন আহমেদ উচ্চ বিদ্যালয়";
            
            var values = new Dictionary<string, string>(){
                                           { "token", token },
                                           { "to", number },
                                           { "message", message }
                                        };

                var content = new FormUrlEncodedContent(values);

                var response =await client.PostAsync("http://sms.greenweb.com.bd/api.php?", content);

                responseString = await response.Content.ReadAsStringAsync();
            
        }

        public async Task<string> sendSingleSmsMethod3(string number, string msg, string token)//,string token
        {
            //environment.newline counts '\n' as 2 character. which is same to sms gateway
            // or we can use \r\n and this is same as using environment.newline
            //string message = "প্রিয় শিক্ষার্থীবৃন্দ," + Environment.NewLine + "সাময়িক অসুবিধার কারণে শনিবার(০৩/০৩/২০১৮) স্কুলে আইডি কার্ড বিতরণ করা হবে না।" + Environment.NewLine + "-সালাহউদ্দিন আহমেদ উচ্চ বিদ্যালয়";

            var responseString = await "http://sms.greenweb.com.bd/api.php?".PostUrlEncodedAsync(new { token = token, to = number, message = msg})
     .ReceiveString();

            return responseString;
        }
        [HttpGet]
        public ActionResult tokenCreate()
        {
            return View();
        }
        [HttpPost]
        public JsonResult tokenCreate(Token tok)
        {
            DBContext DB = new DBContext();
            DB.Tokens.Add(tok);
            DB.SaveChanges();
            return Json("true",JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult tokenEdit(int id)
        {
            DBContext DB = new DBContext();
            Token token = (from tok in DB.Tokens
                           where tok.Id == id
                           select tok).FirstOrDefault();

            return View(token);
        }
        [HttpPost]
        public JsonResult tokenEdit(Token toke)
        {
                DBContext DB = new DBContext();
                Token token = (from tok in DB.Tokens
                               where tok.Id == toke.Id
                               select tok).FirstOrDefault();
                token.Token_Data = toke.Token_Data;
                token.Display_Name = toke.Display_Name;
                token.Is_Active = toke.Is_Active;
                DB.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            
        }
    }
}