using Sandbox;

namespace Roleplay
{
    public abstract partial class RoleplayGameManager : GameManager
    {
        public RoleplayGameManager() 
        { 
            if(Game.IsServer)
            {

            }
        }
    }
}
