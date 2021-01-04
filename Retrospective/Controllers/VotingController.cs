using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Safeer2.UI.Controllers
{
    public class VotingController : Controller
    {
      public  static Dictionary<string, string> AgileTeamVoting = new Dictionary<string, string>() {
          { "Ali","11" },
          { "Amir", "11" },
          { "Ahmed","11" },
          { "Waad", "11" },
          { "Keerthi","11" },
          { "Mallesh", "11" },
          { "Asif","11" },
          { "Sarah", "11" },
          { "Amjad","11" },
          { "Alhanouf", "11" },
          { "Bashyer","11" },
          { "Abeer", "11" },
          { "Ala","11" }
      };
        // GET: Retrospective
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitVoting(string name,string Voting)
        {
            if (AgileTeamVoting.ContainsKey(name))
            {
                AgileTeamVoting.Remove(name);
            }
            AgileTeamVoting.Add(name, Voting);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult GetVoting()
        {
            return Json(AgileTeamVoting, JsonRequestBehavior.AllowGet);
        }
    }
}