using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;

namespace Motivator.Util.Json
{
    public class IgnoreAllExceptAttribute : ActionFilterAttribute
    {
        private readonly Type ignored;
        private readonly string[] except;

        public IgnoreAllExceptAttribute(Type ignored, params string[] except)
        {
            this.ignored = ignored;
            this.except = except;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Result == null) return;

            var settings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            settings.ContractResolver = new IgnoreAllExceptContractResolver()
                .IgnoreAll(ignored)
                .Except(ignored, except);

            var formatter = new JsonOutputFormatter(settings, ArrayPool<Char>.Shared);
            var result = context.Result as ObjectResult;
            result.Formatters.Add(formatter);
        }
    }
}
