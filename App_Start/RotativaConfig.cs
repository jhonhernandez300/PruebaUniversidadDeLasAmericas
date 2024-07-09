using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Rotativa;

namespace PruebaU.App_Start
{
    public class RotativaConfig
    {
        public static void Configure()
        {
            string rotativaPath = HostingEnvironment.MapPath("~/bin/Rotativa");
            //RotativaConfiguration.Setup(rotativaPath);
        }
    }
}