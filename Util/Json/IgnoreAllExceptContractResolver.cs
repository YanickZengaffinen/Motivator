using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Motivator.Util.Json
{
    /// <summary>
    /// Json Contract Resolver that ignores all properties of a type except some included ones
    /// </summary>
    public class IgnoreAllExceptContractResolver : DefaultContractResolver
    {
        private readonly IList<Type> ignoreAll;
        private readonly Dictionary<Type, HashSet<string>> except;

        public IgnoreAllExceptContractResolver()
        {
            ignoreAll = new List<Type>();
            except = new Dictionary<Type, HashSet<string>>();
        }

        /// <summary>
        /// All properties of the given type are ignored by default
        /// </summary>
        public IgnoreAllExceptContractResolver IgnoreAll(Type type)
        {
            if(!ignoreAll.Contains(type))
            {
                ignoreAll.Add(type);
            }

            return this;
        }

        /// <summary>
        /// The property will not be ignored on serialization
        /// </summary>
        public IgnoreAllExceptContractResolver Except(Type type, params string[] properties)
        {
            if(!except.ContainsKey(type))
            {
                except.Add(type, new HashSet<string>());
            }

            foreach(string p in properties)
            {
                if (!except[type].Contains(p))
                {
                    except[type].Add(p);
                }
            }

            return this;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if(IsGenerallyIgnored(property.DeclaringType))
            {
                if (IsException(property.DeclaringType, property.PropertyName))
                {
                    property.ShouldSerialize = i => true;
                    property.Ignored = false;
                }
                else
                {
                    property.ShouldSerialize = i => false;
                    property.Ignored = true;
                }
            }

            return property;
        }

        protected bool IsGenerallyIgnored(Type type)
        {
            return ignoreAll.Contains(type);
        }

        protected bool IsException(Type type, string property)
        {
            if(except.ContainsKey(type))
            {
                if(except[type].Contains(property))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
