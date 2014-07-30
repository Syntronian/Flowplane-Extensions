using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Asana.Responses.Projects
{
    public class GetTasks
    {
        private Newtonsoft.Json.Linq.JObject obj = null;
        private string api_key; // required to query for each task to get additional details

        public GetTasks(string json, string api_key)
        {
            this.obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
            this.api_key = api_key;
        }

        public List<Tasks.Get> tasks
        {
            get
            {
                // this is the synchronous approach
                //if (false)
                //{
                //    var ret = new List<Tasks.Get>();
                //    foreach (var item in this.obj.First.First)
                //    {
                //        var tsk = new Extensions.Asana.Tasks(this.api_key);
                //        ret.Add(tsk.Get(item.Value<string>("id")));
                //    }
                //    return ret;
                //}

                // get each task asynchronously, start the async tasks
                var asynctasks = new List<Task<Tasks.Get>>();
                foreach (var item in this.obj.First.First)
                {
                    Task<Tasks.Get> get = new Task<Tasks.Get>(() => this.GetTask(item.Value<string>("id")));
                    get.Start();
                    asynctasks.Add(get);
                }

                // check if any still running
                var stillRunning = true;
                while (stillRunning)
                {
                    stillRunning = false;
                    foreach (var get in asynctasks)
                    {
                        if (!get.IsCompleted)
                        {
                            // not completed, if also haven't failed still waiting.
                            if (!get.IsCanceled && !get.IsFaulted)
                            {
                                stillRunning = true;
                                break;
                            }
                        }
                    }
                }

                // return
                var ret = new List<Tasks.Get>();
                foreach (var get in asynctasks)
                {
                    ret.Add(get.Result);
                }
                return ret;
            }
        }

        private Tasks.Get GetTask(string id)
        {
            var tsk = new Extensions.Asana.Tasks(new Auth(this.api_key));
            return tsk.Get(id);
        }
    }
}
