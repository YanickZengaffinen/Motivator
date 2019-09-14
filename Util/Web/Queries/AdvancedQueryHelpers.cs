using System.Collections.Generic;
using System.Text;

namespace Motivator.Util.Web.Queries
{
    public static class AdvancedQueryHelpers
    {
        /// <summary>
        /// Adds an array of parameters to a query string so that asp.net core understands it
        /// (?ids=14&ids=4&ids=99)
        /// </summary>
        public static string AddArrayParams<T>(string uri, string paramName, IEnumerable<T> values)
        {
            var sb = new StringBuilder(uri);
            if (!uri.Contains('?'))
            {
                sb.Append('?');
            }
            else if(!uri.EndsWith('?'))
            {
                sb.Append('&');
            }

            sb.Append(string.Join('&', values));

            return sb.ToString();
        } 
    }
}
