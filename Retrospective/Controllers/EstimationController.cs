using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Safeer2.UI.Controllers
{
    public class EstimationController : Controller
    {
      public  static Dictionary<string, string> AgileTeamWeights = new Dictionary<string, string>() {
          { "Ali","80" },
          { "Amir", "80" },
          { "Ahmed","80" },
          { "Waad", "80" },
          { "Keerthi","80" },
          { "Mallesh", "80" },
          { "Asif","80" },
          { "Sarah", "80" },
          { "Amjad","80" },
          { "Alhanouf", "80" },
          { "Bashyer","80" },
          { "Abeer", "80" },
          { "Ala","80" }
      };
        // GET: Retrospective
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitWeight(string name,string weight)
        {
            if (AgileTeamWeights.ContainsKey(name))
            {
                AgileTeamWeights.Remove(name);
            }
            AgileTeamWeights.Add(name, weight);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult GetWeight()
        {
            return Json(AgileTeamWeights,JsonRequestBehavior.AllowGet);
        }
    }
}