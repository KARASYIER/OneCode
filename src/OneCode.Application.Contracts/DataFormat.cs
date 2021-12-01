using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode
{
    public class DateFormat : IsoDateTimeConverter
    {
        /// <summary>
        /// 默认日期格式
        /// </summary>
        public DateFormat() { DateTimeFormat = "yyyy-MM-dd"; }
        /// <summary>
        /// 日期格式
        /// </summary>
        public DateFormat(string format) { DateTimeFormat = format; }
    }
}
