using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio.Responses.Tasks
{
    public class Add
    {
        private PodioAPI.Models.Task task;

        public Add(PodioAPI.Models.Task task)
        {
            this.task = task;
        }

        public string id
        {
            get
            {
                return this.task.TaskId;
            }
        }

    }
}
