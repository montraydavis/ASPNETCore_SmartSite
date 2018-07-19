using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore_SmartSite.Models
{

    internal interface ISmartEvent
    {
        string CallbackFunction { get; set; }
        string[] CallbackFunctionParameters { get; set; }
    }
}
