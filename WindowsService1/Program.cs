﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

         //System.Diagnostics.Debugger.Launch();
           ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 

            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}