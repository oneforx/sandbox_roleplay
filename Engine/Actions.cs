using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine
{
    public static class Actions
    {
        public static Dictionary<string, IAction> Library = new Dictionary<string, IAction>()
        {
            { "action.eat", new EatAction() }
        };
    
        public static bool Register(IAction action)
        {
            Library.Add(action.Name, action);
            return true;
        }
    }
}
