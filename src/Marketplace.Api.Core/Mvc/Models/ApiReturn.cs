﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Marketplace.Api.Mvc.Models
{
    public class ApiReturn<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ApiReturn : ApiReturn<object>
    {
    }
}