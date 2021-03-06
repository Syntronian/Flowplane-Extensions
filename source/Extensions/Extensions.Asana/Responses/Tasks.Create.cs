﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Asana.Responses.Tasks
{
    public class Create
    {
        private Newtonsoft.Json.Linq.JObject obj = null;

        public Create(string json)
        {
            this.obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
        }

        public string id
        {
            get
            {
                return this.obj.First.First.Value<string>("id");
            }
        }
    }
}
