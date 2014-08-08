using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodioAPI.Models.Request;

namespace Extensions.Podio
{
    public class Tasks : Auth
    {
        public Tasks(Auth auth) : base(auth)
        {
        }

        public Responses.Tasks.Get Get(int id)
        {
            try
            {
                var rs = this.client.TaskService.GetTask(id);
                return new Responses.Tasks.Get(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }

        public Responses.Tasks.Add Add(int project_item_id,
                                       string name, int? assignee_id, int? daysDue)
        {
            DateTime? dd = null;
            if (daysDue.HasValue)
                dd = DateTime.UtcNow.AddDays((int)daysDue);

            try
            {
                var rs = this.client.TaskService.CreateTask(name, dd, null, assignee_id, true, "item", project_item_id);
                return new Responses.Tasks.Add(rs.First());
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }


        public void Complete(int task_id)
        {
            this.client.TaskService.CompleteTask(task_id);
        }
    }
}
