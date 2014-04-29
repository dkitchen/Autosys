using Autosys4.App_Start;
using Autosys4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autosys4.Controllers
{
    public class AutosysController : Controller
    {

        //
        // GET: /Autosys/
        public ActionResult Index()
        {
            var emptyModel = new AutosysModel();
            emptyModel.Response = "Example jobs: SM_EFM_COGS_CALC_ACTLBx, SM_EFM_COGS_CALC_ACRLBx";
            emptyModel.Html = "Example Urls:<br/><a href='Autosys/aj/SM_EFM_COGS_CALC_ACTLBx'>Autosys/aj/SM_EFM_COGS_CALC_ACTLBx</a><br/><a href='Autosys/jd/SM_EFM_COGS_CALC_ACTLBx'>Autosys/jd/SM_EFM_COGS_CALC_ACTLBx</a><br/><a href='Autosys/jil/SM_EFM_COGS_CALC_ACTLBx'>Autosys/jil/SM_EFM_COGS_CALC_ACTLBx</a>";
            return View(emptyModel);
        }


        //
        // GET: /Autosys/AutoRep/command
        public ActionResult AutoRep(AutosysModel viewModel)
        {
            ViewBag.Title = "AutoRep";
            viewModel.Response = AutosysConfig.AutosysSession.AutoRep(viewModel.JobName);
            return View("Index", viewModel);
        }

        //alias
        public ActionResult AJ(AutosysModel viewModel)
        {
            return AutoRep(viewModel);
        }


        //
        // GET: /Autosys/JobDepends/command
        public ActionResult JobDepends(AutosysModel viewModel)
        {
            ViewBag.Title = "JobDepends";
            viewModel.Response = AutosysConfig.AutosysSession.JobDepends(viewModel.JobName);
            return View("Index", viewModel);
        }
        //alias
        public ActionResult JD(AutosysModel viewModel)
        {
            return JobDepends(viewModel);
        }

        //
        // GET: /Autosys/JIL/command
        public ActionResult JIL(AutosysModel viewModel)
        {
            ViewBag.Title = "JIL";
            //slow this one down... 5000 should do it
            int oldTimeout = AutosysConfig.AutosysSession.CommandTimeoutMs;
            AutosysConfig.AutosysSession.CommandTimeoutMs = 5000;
            viewModel.Response = AutosysConfig.AutosysSession.JIL(viewModel.JobName);
            AutosysConfig.AutosysSession.CommandTimeoutMs = oldTimeout;
            return View("Index", viewModel);

        }

    }
}
