﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudsdale.lib
{
    class UserCheck
    {
        public static void Authorize(string user)
        {
            switch (user)
            {
                case "Zeeraw":
                    throw new UnhandledSwagEvent();
                case "Connorcpu":
                    throw new UnhandledSwagEvent();
                case "Aethe":
                    throw new UnhandledSwagEvent();
                case "Berwyn Codeweaver":
                    throw new UnhandledSwagEvent();
                case "Nitro":
                    throw new UnhandledSwagEvent();
                case "Colorswirl":
                    throw new UnhandledSwagEvent();
                case "Conji":
                    throw new UnhandledSwagEvent();
                default:
                    break;
            }
        }
    }
}
