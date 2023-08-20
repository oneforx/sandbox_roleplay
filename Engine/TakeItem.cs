using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Models;

namespace Roleplay.Engine
{
    public class TakeItem : Models.Action
    {
        public string ItemName { get; set; } = null;

        public Type ItemType { get; set; } = null;

        public TakeItem() : base("action.take_item")
        {

        }

        public TakeItem(string itemName) : base("action.take_item")
        {
            ItemName = itemName;
        }

        public TakeItem(Type itemType) : base("action.take_item")
        {
            ItemType = itemType;
        }

        public bool Accept(object type)
        {
            if(ItemType != null && type != null)
            {
                return ItemType.IsAssignableFrom(type.GetType());
            }
            return false;
        }
    }
}
