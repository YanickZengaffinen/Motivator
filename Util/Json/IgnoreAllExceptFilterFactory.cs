using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Motivator.Util.Json
{
    public class IgnoreAllExceptFilterFactory : Attribute, IFilterFactory
    {
        public Type Ignored { get; set; }

        public string[] Except { get; set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = (IgnoreAllExceptFilter)serviceProvider.GetService(typeof(IgnoreAllExceptFilter));

            filter.Ignored = Ignored;
            filter.Except = Except;

            return filter;
        }
    }
}
