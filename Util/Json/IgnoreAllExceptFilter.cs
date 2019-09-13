using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;

namespace Motivator.Util.Json
{
    public class IgnoreAllExceptFilter : IActionFilter
    {
        public Type Ignored { get; set; }

        public string[] Except { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Result == null) return;

            var settings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            settings.ContractResolver = new IgnoreAllExceptContractResolver()
                .IgnoreAll(Ignored)
                .Except(Ignored, Except);

            var formatter = new JsonOutputFormatter(settings, ArrayPool<Char>.Shared);
            var result = context.Result as ObjectResult;
            result.Formatters.Add(formatter);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
