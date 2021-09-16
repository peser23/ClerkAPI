using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clerk.Business.Entity
{
    public class MemberByResponse
    {
        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("middlename")]
        public string MiddleName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("namelist")]
        public string NameList { get; set; }

        [JsonPropertyName("sort-name")]
        public string SortName { get; set; }

        [JsonPropertyName("official-name")]
        public string OfficialName { get; set; }

        [JsonPropertyName("formal-name")]
        public string FormalName { get; set; }


        [JsonPropertyName("bioguideID")]
        public string BioguideId { get; set; }
        [JsonPropertyName("party")]
        public string Party { get; set; }
        [JsonPropertyName("caucus")]
        public string Caucus { get; set; }
        [JsonPropertyName("townname")]
        public string TownName { get; set; }
        [JsonPropertyName("district")]
        public string District { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("elected-date")]
        public DateTime? ElectedDate { get; set; }

        [JsonPropertyName("sworn-date")]
        public DateTime? SwornDate { get; set; }

        
        [JsonPropertyName("office-zip-suffix")]
        public string OfficeZipSuffix { get; set; }

        [JsonPropertyName("office-zip")]
        public string OfficeZipCode { get; set; }

        [JsonPropertyName("office-room")]
        public string OfficeRoom { get; set; }


        [JsonPropertyName("office-building")]
        public string OfficeBuilding { get; set; }

      
     


        public StateResponse State { get; set; }

        [JsonPropertyName("committee-assignments")]
        public List<CommitteeAssignmentResponse> CommitteeAssignments { get; set; }
    }
}
