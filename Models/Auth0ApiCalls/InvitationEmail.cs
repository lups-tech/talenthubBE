using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace talenthubBE.Models
{
    public class InvitationEmail
    {  
        [JsonPropertyName("inviter")]
        public required InviterClass Inviter { get; set; }
        [JsonPropertyName("invitee")]
        public required InviteeClass Invitee { get; set; }
        [JsonPropertyName("client_id")]
        public required String ClientId { get; set; }
        [JsonPropertyName("connection_id")]
        public required String ConnectionId { get; set; }
        [JsonPropertyName("ttl_sec")]
        public required int TtlSec { get; set; }
        [JsonPropertyName("roles")]
        public required String[] Roles { get; set; }
        [JsonPropertyName("send_invitation_email")]
        public required Boolean SendInvitationEmail { get; set; }

    }
        public class InviterClass {
            [JsonPropertyName("name")]
            public required String Name { get; set; }
        }
        public class InviteeClass {
            [JsonPropertyName("email")]
            public required String Email { get; set; }
        }   

}