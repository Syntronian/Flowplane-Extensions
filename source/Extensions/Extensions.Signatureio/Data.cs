/**********************************************************************
Copyright :
**********************************************************************
* File           : Data.cs
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
*  1        VaishakhiP 10-Feb-2014    1.0         Created/ Added Data Transfer objects for SignUp
*  2        VaishakhiP 11-Feb-2014    1.1         Added Data transfer Objects for request/response
*                                                 to request document,shareURL and Events.
***********************************************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Signatureio
{
    /// <summary>
    /// 
    /// </summary>
    internal class SignupRequestDTO
    {
        public string first_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public SignupRequestDTO(string fn, string em, string pw)
        {
            first_name = fn;
            email = em;
            password = pw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SignupResponseDTO
    {
        public bool success { get; set; }
        public PersonDTO person { get; set; }
        public Error error { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PersonDTO
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public string secret_api_key { get; set; }
        public string public_api_key { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DocumentRequestDTO
    {
        public string url { get; set; }
        public DocumentRequestDTO(string du)
        {
            url = du;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DocumentResponseDTO
    {
        public bool success { get; set; }
        public DocumentDTO document { get; set; }
        public Error error { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DocumentDTO
    {
        public string id { get; set; }
        public string parent_id { get; set; }
        public string url { get; set; }
        public string pdf_url { get; set; }
        public string share_url { get; set; }
        public string filename { get; set; }
        public string created { get; set; }
        public string update { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class SessionRequestDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        public SessionRequestDTO(string em, string pw)
        {
            email = em;
            password = pw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SessionResponseDTO
    {
        public bool success { get; set; }
        public string token { get; set; }
        public string secret_api_key { get; set; }
        public string public_api_key { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EventsResponseDTO
    {
        public string success { get; set; }
        public string total_Count { get; set; }
        public string count { get; set; }
        public string offset { get; set; }
        public EventDTO[] events { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EventDTO
    {
        public string id { get; set; }
        public string type { get; set; }
        public string created { get; set; }
        public DataDTO data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataDTO
    {
        public object @object { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Error
    {
        public string message { get; set; }
    }
}
