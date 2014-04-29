using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autosys;

namespace Autosys4.App_Start
{
    public class AutosysConfig
    {
        private const string AutosysSession_KEY = "AutosysSession";

        public static AutosysSession AutosysSession
        {
            get
            {
                return HttpContext.Current.Session[AutosysSession_KEY] as AutosysSession;
            }
            set
            {
                HttpContext.Current.Session[AutosysSession_KEY] = value;
            }
        }
    }
}