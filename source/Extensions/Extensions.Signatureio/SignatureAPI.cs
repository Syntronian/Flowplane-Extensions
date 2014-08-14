/**********************************************************************
Copyright :
**********************************************************************
* File           : SignatureAPI.cs
* Created        : 10-Feb-2014
* Author         : Vaishakhi Panchmatia (vaishakhip@gmail.com)
* Business Owner : Steve Shearn - Client
*  
* Description    : 
* ***************************************************************************
*
* ***************************************************************************
* 
* Change History :
* 
* No.      Changed By  Date           Version     Remarks
* ---      ----------  --------       -------     ------------------------------
*  1        VaishakhiP 10-Feb-2014    1.0         Created / Wrote method for SignUp()
*  2        VaishakhiP 11-Feb-2014    1.1         Added new methods GetSession(), GetShareURL, and GetEvents()
***********************************************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;

namespace Extensions.Signatureio
{
    public class SignatureAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static SignupResponseDTO Signup(string firstname, string email, string password)
        {
            Uri uri = new Uri("https://www.signature.io/api/v0/people.json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var serializer = new JavaScriptSerializer();
                    SignupRequestDTO data = new SignupRequestDTO(firstname, email, password);
                    var serializedResult = serializer.Serialize(data);
                    StringContent content = new StringContent(serializedResult);
                    content.Headers.ContentType.MediaType = "application/json";

                    var response = client.PostAsync(uri, content).Result;
                    string st = response.Content.ReadAsStringAsync().Result;
                    SignupResponseDTO dto = serializer.Deserialize<SignupResponseDTO>(st);
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static SessionResponseDTO GetSession(string email, string password)
        {
            Uri uri = new Uri("https://www.signature.io/api/v0/sessions.json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var serializer = new JavaScriptSerializer();
                    SessionRequestDTO data = new SessionRequestDTO(email, password);
                    var serializedResult = serializer.Serialize(data);
                    StringContent content = new StringContent(serializedResult);
                    content.Headers.ContentType.MediaType = "application/json";

                    var response = client.PostAsync(uri, content).Result;
                    string st = response.Content.ReadAsStringAsync().Result;
                    SessionResponseDTO dto = serializer.Deserialize<SessionResponseDTO>(st);
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secret_api_key"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static DocumentResponseDTO GetShareURL(string secret_api_key, string uri)
        {
            Uri docUri = new Uri("https://www.signature.io/api/v0/documents.json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var serializer = new JavaScriptSerializer();
                    DocumentRequestDTO data = new DocumentRequestDTO(uri);
                    var serializedResult = serializer.Serialize(data);
                    StringContent content = new StringContent(serializedResult);
                    content.Headers.ContentType.MediaType = "application/json";

                    byte[] bytes = Encoding.ASCII.GetBytes(secret_api_key);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
                    client.DefaultRequestHeaders.Authorization = header;

                    var response = client.PostAsync(docUri, content).Result;
                    string st = response.Content.ReadAsStringAsync().Result;
                    DocumentResponseDTO dto = serializer.Deserialize<DocumentResponseDTO>(st);
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static DocumentResponseDTO GetShareURL(string email, string password, string uri)
        {
            return GetShareURL(GetSession(email, password).secret_api_key, uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static EventsResponseDTO GetEvents(string token)
        {
            Uri uri = new Uri("https://www.signature.io/api/v0/events.json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
                    client.DefaultRequestHeaders.Authorization = header;

                    var response = client.GetAsync(uri).Result;
                    string st = response.Content.ReadAsStringAsync().Result;
                    var serializer = new JavaScriptSerializer();
                    EventsResponseDTO dto = serializer.Deserialize<EventsResponseDTO>(st);
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static EventsResponseDTO GetEvents(string email, string password)
        {
            return GetEvents(GetSession(email, password).token);
        }
    }
}
