using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGalerie.Helpers
{
    public static class JsonHelper
    {
        public static string ToJsonString(this object value)
        {
            if (value == null) return null;
            return JsonConvert.SerializeObject(value);
        }
    }
}