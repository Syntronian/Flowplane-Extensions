using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowplaneExtensions.Models.api.Flow;
using Newtonsoft.Json;

namespace FlowplaneExtensions.Controllers
{
    public class AsanaController : Controller
    {
        public ActionResult Auth()
        {
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Body()
        {
            return View();
        }

        public ActionResult FlowDetail(string fpxtpms)
        {
            if (string.IsNullOrEmpty(fpxtpms)) return View();

            ViewBag.pms = fpxtpms;
            return View(JsonConvert.DeserializeObject<Detail>(System.Uri.UnescapeDataString(fpxtpms)));
        }
    }
}