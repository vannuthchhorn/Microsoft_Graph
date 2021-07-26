using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft_Planner_RestAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft_Planner_RestAPI.Controllers
{
    public class ProfileController : Controller
    {
        public static string baseUrl = "https://graph.microsoft.com/v1.0/me/";
        public static string TokenKey = "eyJ0eXAiOiJKV1QiLCJub25jZSI6InBmMk9ZaVpvc2hST0NzeE1OQUpab3BITkF3RmlzLWRCQ1ZSbHRKeFVLazQiLCJhbGciOiJSUzI1NiIsIng1dCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyIsImtpZCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9hZGZlZWIwMS02YTNhLTRjMTEtYjNmOC1mZjg5MDQyMTE0OTEvIiwiaWF0IjoxNjI3MDA2OTc1LCJuYmYiOjE2MjcwMDY5NzUsImV4cCI6MTYyNzAxMDg3NSwiYWNjdCI6MCwiYWNyIjoiMSIsImFjcnMiOlsidXJuOnVzZXI6cmVnaXN0ZXJzZWN1cml0eWluZm8iLCJ1cm46bWljcm9zb2Z0OnJlcTEiLCJ1cm46bWljcm9zb2Z0OnJlcTIiLCJ1cm46bWljcm9zb2Z0OnJlcTMiLCJjMSIsImMyIiwiYzMiLCJjNCIsImM1IiwiYzYiLCJjNyIsImM4IiwiYzkiLCJjMTAiLCJjMTEiLCJjMTIiLCJjMTMiLCJjMTQiLCJjMTUiLCJjMTYiLCJjMTciLCJjMTgiLCJjMTkiLCJjMjAiLCJjMjEiLCJjMjIiLCJjMjMiLCJjMjQiLCJjMjUiXSwiYWlvIjoiRTJaZ1lOQldlc3lqR0ZxWC9JaTNLbDJlY2M3aGllbEt5eXllYitaNHkzWjJxbnhQcEMwQSIsImFtciI6WyJwd2QiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggZXhwbG9yZXIgKG9mZmljaWFsIHNpdGUpIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IkNoaG9ybiIsImdpdmVuX25hbWUiOiJWYW5udXRoIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMTAzLjEyLjE2MS4xNCIsIm5hbWUiOiJWYW5udXRoIENoaG9ybiIsIm9pZCI6IjNhZjI4ZDdhLWRlOGEtNDc4NS05YjkzLTU3YzlhYTBlNTE1NSIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0xMjUxMDMyMjI2LTE5OTEwMTA3OTUtNDAzMDIyNzY5Ni00Nzc4IiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAwRTA1QjI3QUQiLCJyaCI6IjAuQVNvQUFldi1yVHBxRVV5ei1QLUpCQ0VVa2JYSWk5NzUyYkZJcUsyM1NOcHlVR1FxQUFJLiIsInNjcCI6Im9wZW5pZCBwcm9maWxlIFVzZXIuUmVhZCBlbWFpbCIsInN1YiI6InZjRzl3MEVhX0dMMG9CWnFEaU5BN2RSMmpHbk9WemxfSWVtelBTY0h6MmsiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiQVMiLCJ0aWQiOiJhZGZlZWIwMS02YTNhLTRjMTEtYjNmOC1mZjg5MDQyMTE0OTEiLCJ1bmlxdWVfbmFtZSI6InZhbm51dGguY2hob3JuQGZpcnN0LWNhbWJvZGlhLmNvbSIsInVwbiI6InZhbm51dGguY2hob3JuQGZpcnN0LWNhbWJvZGlhLmNvbSIsInV0aSI6IlM2bWg1dklLQ1VPTHI5VHlaTndmQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfc3QiOnsic3ViIjoieDhzNHRjQlJiLTJpMEVrVzRaSDRwWWJUQmZfbzYzS1R2MDRQRmJfbG5MZyJ9LCJ4bXNfdGNkdCI6MTUyMjIyNTIzOH0.HdJ8X8v-7RiWlJMREw7ICn4mm0ndgve4_n4VRZshMRS2uNU2PUSw2jg-CudzGNZZDr9lQGONlPQ4anS9hppgRKZD6GffiO37x0Ngdzwu_Iko6TnBXDcYKWkBdwSYtPq9FVuQw26nnpdVGdvLYnbPOM_50U9cQQtB5cW-tPhlRF5hGwEmQBA0-7vZf3QvymgTwUnawP73lesge9KQr7fpRBcw6ku0Un-oHRjwH1ycOx71J3ecRLw-L8GFuxhfwNCzNaU_XLcBj9nuwfMNx1svDPe9ahCPpZRyU6xOBDk0HZtpEGOLgrVZL5Wf0B93sWcf7KPldduM7AmzRWbC1mQmIA";
        public async Task<IActionResult> Index()
        {
            var profile = await GetProfiles();
            return View(profile);
        }

        [HttpGet]
        public async Task<Profile> GetProfiles(string Id)
        {
            Profile profileUser = new Profile();
            
            //var TokenKey = HttpContext.Session.GetString("JwtToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);
            //string jsonStr = await client.GetStringAsync(url);
            //var Res = JsonConvert.DeserializeObject<List<Profile>>(jsonStr).ToList();

            System.Net.Http.HttpResponseMessage Res = await client.GetAsync(string.Format(url));
            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
            var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject(EmpResponse).ToString();

            if (Res.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(resultObject);
                var result = (Newtonsoft.Json.Linq.JObject)data["responseResult"];

                var displayName = (string)result["displayName"];
                var givenName = (string)result["givenName"];
                var jobTitle = (string)result["jobTitle"];
                var mail = (string)result["mail"];
                var mobilePhone = (string)result["mobilePhone"];
                var officeLocation = (string)result["officeLocation"];
                var preferredLanguage = (string)result["preferredLanguage"];
                var surname = (string)result["surname"];
                var userPrincepalName = (string)result["userPrincipalName"];
                var id = (string)result["id"];

                var Respone = new Profile()
                {
                    displayName = displayName,
                    givenName = givenName,
                    jobTitle = jobTitle,
                    mail = mail,
                    mobilePhone = mobilePhone,
                    officeLocation = officeLocation,
                    preferredLanguage = preferredLanguage,
                    surname = surname,
                    userPrincipalName = userPrincepalName,
                    id = id
                };
                profileUser = Respone;
            }

            return Res;
        }
    }
}
