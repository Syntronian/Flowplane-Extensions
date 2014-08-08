using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio.Responses.Tasks
{
    public class Get
    {
        private PodioAPI.Models.Task task;

        public Get(PodioAPI.Models.Task task)
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

        public string name
        {
            get
            {
                return this.task.Text;
            }
        }

        public string description
        {
            get
            {
                return this.task.Description;
            }
        }

        public bool complete
        {
            get
            {
                return this.task.CompletedOn.HasValue;
            }
        }
    }
}
