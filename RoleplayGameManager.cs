using Sandbox;
using Roleplay.Map;
using System.Collections.Generic;

namespace Roleplay
{
    public abstract partial class RoleplayGameManager : GameManager
    {
        public static MapRegistry MapRegistry { get; set; }

        public RoleplayGameManager() 
        { 
            if(Game.IsServer)
            {

            }
        }
    }
}
