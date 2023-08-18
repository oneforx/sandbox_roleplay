using Roleplay.Bank;
using Roleplay.Engine;
using Roleplay.Engine.Utils;
using Sandbox;
using System;
using System.Collections.Generic;

namespace Roleplay.Business
{
    public partial class Business: BaseNetworkable {

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name;

        public long OwnerId;

        public IList<BusinessMember> Members { get; set; } = new List<BusinessMember>();

        public IList<Job> JobEntries { get; set; } = new List<Job>();

        public BankAccount ActiveBankAccount;

        public IList<BusinessMemberInvitation> MembersInvitations { get; set; } = new List<BusinessMemberInvitation>();

        public Business(string name, long ownerId) {
            this.Name = name;
            this.OwnerId = ownerId;
            Event.Run("business.common.joined", ownerId);
            ClientJoined(To.Single(ClientManager.GetClientById(ownerId)), this.Id, ownerId); ;
        }

        [ClientRpc]
        public static void ClientJoined(Guid businessId, long clientId)
        {
            Event.Run("business.common.joined", businessId, clientId);
        }

        public void AddJobToMember()
        {
            foreach(var member in Members)
            {
                member.SetActiveJob(JobLibrary.ButcherJob);
            }
        }

        public bool AddMember(BusinessMember member)
        {
            if (GetMemberById(member.ClientId) == null)
            {
                Members.Add(member);
                Event.Run("business.common.joined", this.Id, member.ClientId);
                ClientJoined(To.Multiple(GetMembersClients()), this.Id, member.ClientId);
                return true;
            }
            else
            {
                Log.Info("Player is already member");
                return false;
            }
        }

        public bool RemoveMember(BusinessMember member)
        {
            Members.Remove(member);
            return true;
        }

        public bool RemoveMemberById(long clientId)
        {
            BusinessMember businessMember = GetMemberById(clientId);
            if (businessMember != null)
            {
                return RemoveMember(businessMember);
            }
            else
            {
                Log.Info("Can't remove the client " + clientId + " because he doesn't exist as member");
                return false;
            }
        }

        public IReadOnlyCollection<IClient> GetMembersClients()
        {
            List<IClient> clients = new List<IClient>();
            foreach(var member in Members)
            {
                clients.Add(ClientManager.GetClientById(member.ClientId));
            }
            return clients;
        }

        public BusinessMember GetMemberById(long id)
        {
            foreach (var member in Members)
            {
                if (member.ClientId == id)
                {
                    return member;
                }
            }

            return null;
        }
        public BusinessMemberInvitation GetMemberInvitationById(long id)
        {
            foreach (var memberInvitation in MembersInvitations)
            {
                if (memberInvitation.ClientId == id)
                {
                    return memberInvitation;
                }
            }

            return null;
        }

        public bool AddMemberInvitation(BusinessMemberInvitation invitation)
        {
            if (GetMemberById(invitation.ClientId) == null && GetMemberInvitationById(invitation.ClientId) == null)
            {
                this.MembersInvitations.Add(invitation);
                return true;
            }
            else
            {
                if (GetMemberById(invitation.ClientId) != null)
                {
                    Log.Info("The player is already member");
                }

                if (GetMemberInvitationById(invitation.ClientId) != null)
                {
                    Log.Info("The player is already invited");
                }

                return false;
            }
        }

        public bool RemoveMemberInvitation(BusinessMemberInvitation memberInvitation)
        {
            MembersInvitations.Remove(memberInvitation);
            return true;
        }

        public bool RemoveMemberInvitationByClientId(long clientId)
        {
            BusinessMemberInvitation memberInvitation = GetMemberInvitationById(clientId);
            if (memberInvitation != null)
            {
                MembersInvitations.Remove(memberInvitation);
                return true;
            }
            else
            {
                Log.Info("The memberInvitation for clientId : " +  clientId  + " doesn't exist");
                return false;
            }
        }
    }
}