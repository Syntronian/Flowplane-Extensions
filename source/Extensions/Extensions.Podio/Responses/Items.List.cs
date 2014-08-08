﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio.Responses.Items
{
    public class Item
    {
        public Item()
        {
        }

        public Item(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string id { get; set; }
        public string name { get; set; }
    }



    public class List
    {
        public List<Item> items { get; set; }

        public List(PodioAPI.Utils.PodioCollection<PodioAPI.Models.Item> items)
        {
            this.items = new List<Item>();
            foreach (var item in items.Items)
            {
                this.items.Add(new Item(item.ItemId.ToString(), item.Title));
            }
        }
    }
}
