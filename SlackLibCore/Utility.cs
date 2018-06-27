using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Slack
{


    public class Utility
    {


        public static dynamic TryGetProperty(dynamic dynamicObject, string propertyName)
        {
            return TryGetProperty(dynamicObject, propertyName, "");
        }


        public static dynamic TryGetProperty(dynamic dynamicObject, string propertyName, dynamic Default)
        {
            try
            {
                if (!HasProperty(dynamicObject, propertyName))
                {
                    return Default;
                }

                if (dynamicObject.GetType() == typeof(JObject))
                {
                    var jsonObj = (JObject)dynamicObject;

                    var propValue = jsonObj.Value<dynamic>(propertyName) ?? Default;

                    return propValue;
                }

                if (dynamicObject.GetType() == typeof(System.Collections.IDictionary))
                {
                    return (Dictionary<string, object>)dynamicObject[propertyName];
                }

                return Default;
            }
            catch (Exception ex)
            {
                throw new Exception($"TryGetProperty(): Could not determine if dynamic object has property {propertyName}", ex);
            }
        }


        public static dynamic HasProperty(dynamic dynamicObject, string propertyName)
        {
            try
            {
                if (dynamicObject.GetType() == typeof(JObject))
                {
                    var obj = (JObject)dynamicObject;

                    if (obj.ContainsKey(propertyName)) return true;
                }
                else if (dynamicObject.GetType() == typeof(System.Collections.IDictionary))
                {
                    if (((IDictionary<string, object>)dynamicObject).ContainsKey(propertyName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"HasProperty(): Could not determine if dynamic object has property {propertyName}", ex);
            }
        }


    }


}
