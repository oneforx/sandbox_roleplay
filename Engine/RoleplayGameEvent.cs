﻿using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine
{
    public interface IRoleplayGameEvent
    {
        string Name { get; }

        public void On();

        public void Bind(object? instance);
    }
    public static class RoleplayGameEvent
    {

        public static class Common
        {
            [MethodArguments(typeof(Business.Business), typeof(long))]
            public class ClientJoinedBusiness : EventAttribute
            {
                public ClientJoinedBusiness() : base("business.common.joined")
                {

                }
            }
        }
    }
}