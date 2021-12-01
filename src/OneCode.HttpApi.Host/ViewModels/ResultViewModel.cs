using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCode.ViewModels
{
    public class ResultViewModel<T>
    {
        public bool Success { get; set; }

        public string ResultMsg { get; set; }

        public List<T> ResultData { get; set; }
    }
}
