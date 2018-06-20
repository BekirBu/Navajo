using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace TimeKeeper.DAL
{
    public static class Utility
    {
        private static readonly log4net.ILog log = log4net.LogManager
                                                   .GetLogger(MethodBase.GetCurrentMethod()
                                                   .DeclaringType);

        public static void Log(string Message, string Level = "ERROR", Exception ex = null)
        {
            if (Level == "INFO") log.Info(Message);
            else log.Error(Message);
        }
    }
}