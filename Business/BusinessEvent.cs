using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Business
{
    public static class BusinessEvent
    {
        public static class Client
        {
            public class OnJoinAttribute
            {

            }

            // Reason
            public class OnLeaveAttribute
            {

            } 
        }

        public static class Common
        {
            public class ClientTaskCompletedAttribute
            {

            }

            public class ClientTaskNewAttribute
            {

            }
        }

        public static class Server
        {
            public class ClientJoinedAttribute
            {

            }

            public class ClientLeftAttribute
            {

            }
        }
    }
}
