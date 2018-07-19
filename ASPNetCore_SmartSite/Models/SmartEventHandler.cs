using Microsoft.Bot.Builder.CognitiveServices.LuisActionBinding;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNetCore_SmartSite.Models
{
    public static class SmartEventHandler
    {
        public static LuisService luisService = new LuisService(new LuisModelAttribute(Program.Configuration["LuisConfiguration:AppId"], Program.Configuration["LuisConfiguration:AppSecret"]));

        internal static async Task<LuisResult> RunQueryAsync(string query) =>
            await luisService.QueryAsync(query, CancellationToken.None);

        public static LuisResult Run(string query)
        {
            Task<LuisResult> res = Task.Run(async () => {
                return await RunQueryAsync(query);
            });

            res.Wait();
            return res.Result;
        }
    }
}
