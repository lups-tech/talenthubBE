using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace talenthubBE.Models
{
    public class InvitationEmail
    {  
        [JsonProperty("inviter")]
        public required InviterClass Inviter { get; set; }
        [JsonProperty("invitee")]
        public required InviteeClass Invitee { get; set; }
        [JsonProperty("client_id")]
        public required String ClientId { get; set; }
        [JsonProperty("connection_id")]
        public required String ConnectionId { get; set; }
        [JsonProperty("ttl_sec")]
        public required int TtlSec { get; set; }
        [JsonProperty("roles")]
        public required String[] Roles { get; set; }
        [JsonProperty("send_invitation_email")]
        public required Boolean SendInvitationEmail { get; set; }

    }
        public class InviterClass {
            [JsonProperty("name")]
            public required String Name { get; set; }
        }
        public class InviteeClass {
            [JsonProperty("email")]
            public required String Email { get; set; }
        }   

}