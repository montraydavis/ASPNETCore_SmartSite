using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore_SmartSite.Models
{
    public class SmartEvent : ISmartEvent
    {
        public string CallbackFunction { get; set; }
        public string[] CallbackFunctionParameters { get; set; }
    }
}
