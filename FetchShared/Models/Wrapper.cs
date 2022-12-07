﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchShared.Models
{
    public class Wrapper<T>
    {
        public string Key { get; set; }
        public T Object { get; set; }
    }
}
