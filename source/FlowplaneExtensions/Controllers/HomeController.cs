using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using FlowplaneExtensions.Models.api.Flow;
using Newtonsoft.Json;

namespace FlowplaneExtensions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlowDetail(string fpxtpms)
        {
            if (string.IsNullOrEmpty(fpxtpms)) return RedirectToAction("Index");

            var pms = JsonConvert.DeserializeObject<Detail>(System.Uri.UnescapeDataString(fpxtpms));

            if (pms.extensionCode.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToAction("FlowDetail", "Asana", new {fpxtpms = fpxtpms});

            if (pms.extensionCode.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToAction("FlowDetail", "Paymo", new { fpxtpms = fpxtpms });

            if (pms.extensionCode.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToAction("FlowDetail", "Podio", new { fpxtpms = fpxtpms });

            if (pms.extensionCode.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToAction("FlowDetail", "Twitter", new { fpxtpms = fpxtpms });

            if (pms.extensionCode.Equals(new Extensions.Wrike.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToAction("FlowDetail", "Wrike", new { fpxtpms = fpxtpms });

            return RedirectToAction("Index");
        }
    }
}