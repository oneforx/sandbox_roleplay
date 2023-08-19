using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine
{
    public static class Actions
    {
        public static Dictionary<string, Roleplay.Schemas.Action> Library = new Dictionary<string, Roleplay.Schemas.Action>()
        {
            { "action.take_item", new TakeItem() }
        };
    
        public static bool Register(Roleplay.Schemas.Action action)
        {
            Library.Add(action.Name, action);
            return true;
        }
    }
}
