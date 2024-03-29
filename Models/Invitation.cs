﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Systems;

namespace Roleplay.Models
{

    public enum InvitationType
    {
        Business,
        PhoneFriend
    }

    public enum InvitationStatus
    {
        Waiting,
        Accepted,
        Refused,
    }
    public class Invitation : Table
    {
        public InvitationType InvitationType { get; set; }

        public Guid FromId { get; set; }

        public Guid ToId { get; set; }

        public Guid ForId { get; set; }

		public InvitationStatus Status { get; set; }

		public Invitation(Guid fromId, Guid toId, Guid forId, InvitationType invitationType) : base("invitation")
		{
			FromId = fromId;
			ToId = toId;
			ForId = forId;
			InvitationType = invitationType;
			Status = InvitationStatus.Waiting;
		}

		public Invitation() : base("invitation") { }


    }
}
