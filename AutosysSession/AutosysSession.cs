using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telnet;

namespace Autosys
{
    public class AutosysSession
    {
        public int CommandTimeoutMs { get; set; }
        public TelnetConnection Telnet { get; set; }


        /// <summary>
        /// Requires you to set the Telnet property yourself
        /// </summary>
        public AutosysSession()
        {
        }

        /// <summary>
        /// Start autosys session with default Telnet settings
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public AutosysSession(string hostname, string username, string password)
        {

            Telnet = new TelnetConnection(hostname, 23);
            //login to autosys            
            string serverOutput = Telnet.Login(username, password, 300);
            string prompt = serverOutput.TrimEnd();
            prompt = prompt.Substring(prompt.Length - 1, 1);
            if (prompt != "$" && prompt != ">")
            {
                throw new Exception("Connection failed");
            }
            //default to timeout of 1 second
            CommandTimeoutMs = 1000;
        }


        public string AutoRep(string jobName)
        {
            string command = string.Format("autorep -j {0}", jobName);
            string response = GetResponse(command);
            return response;
        }

        public string JIL(string jobName)
        {
            string command = string.Format("autorep -j {0} -q", jobName);
            string response = GetResponse(command);
            return response;
        }

        public string JobDepends(string jobName)
        {
            string command = string.Format("job_depends -c -j {0}", jobName);
            string response = GetResponse(command);
            return response;
        }


        private string GetResponse(string command)
        {
            Telnet.WriteLine(command);
            string response = Telnet.Read(CommandTimeoutMs);
            return response;
        }

    }
}
