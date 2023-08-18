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

        public IList<Job> Jobs { get; set; } = new List<Job>();

        public BankAccount ActiveBankAccount;

        public IList<BusinessMemberInvitation> MembersInvitations { get; set; } = new List<BusinessMemberInvitation>();

        /// <summary>
        /// <para>Create a business based on name (business name) and OwnerId (SteamID)</para>
        /// Also run [RoleplayGameEvent.Common.ClientJoinedBusiness]
        /// </summary>
        public Business(string name, long ownerId) {
            this.Name = name;
            this.OwnerId = ownerId;
            Event.Run("business.common.joined", this, ownerId);
            ClientJoined(To.Single(ClientManager.GetClientById(ownerId)), this, ownerId); ;
        }

        [ClientRpc]
        public static void ClientJoined(Business businessId, long clientId)
        {
            Event.Run("business.common.joined", businessId, clientId);
        }

        /// <summary>
        /// Add a job to all members of your business 
        /// </summary>
        public void AddJobToMembers(Job job)
        {
            foreach(var member in Members)
            {
                member.SetActiveJob(job);
            }
        }

        /// <summary>
        /// Add a job to a member of your business according to the ClientId
        /// </summary>
        public bool AddJobToMember(Job job, long clientId)
        {
            var member = GetMemberById(clientId);

            if (member == null || member.GetActiveJobById(job.Id).Id == job.Id)
                return false;

            member.SetActiveJob(job);
            return true;
        }

        /// <summary>
        /// Add a member to your business
        /// </summary>
        public bool AddMember(BusinessMember member)
        {
            if (GetMemberById(member.ClientId) == null)
            {
                Members.Add(member);
                Event.Run("business.common.joined", this, member.ClientId);
                ClientJoined(To.Multiple(GetMembersClients()), this, member.ClientId);
                return true;
            }
            else
            {
                Log.Info("Player is already member");
                return false;
            }
        }

        /// <summary>
        /// Remove a member from your business
        /// </summary>
        public bool RemoveMember(BusinessMember member)
        {
            Members.Remove(member);
            return true;
        }

        /// <summary>
        /// Remove a member from your business by id (SteamID)
        /// </summary>
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

        /// <summary>
        /// Get all members of your business
        /// </summary>
        public IReadOnlyCollection<IClient> GetMembersClients()
        {
            List<IClient> clients = new List<IClient>();
            foreach(var member in Members)
            {
                clients.Add(ClientManager.GetClientById(member.ClientId));
            }
            return clients;
        }

        /// <summary>
        /// Get a member from clientId (SteamID)
        /// </summary>
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

        /// <summary>
        /// Get a member invitation from clientId (SteamID)
        /// </summary>
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

        /// <summary>
        /// Add a member invitation for your business
        /// </summary>
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

        /// <summary>
        /// Remove a member invitation
        /// </summary>
        public bool RemoveMemberInvitation(BusinessMemberInvitation memberInvitation)
        {
            MembersInvitations.Remove(memberInvitation);
            return true;
        }

        /// <summary>
        /// Remove a member invitation by ClientId (SteamID)
        /// </summary>
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