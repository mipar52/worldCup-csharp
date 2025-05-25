using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public partial class Team
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("goals")]
        public long Goals { get; set; }

        [JsonProperty("penalties")]
        public long Penalties { get; set; }

        [JsonProperty("alternate_name")]
        public object AlternateName { get; set; }

        [JsonProperty("fifa_code")]
        public string FifaCode { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("group_letter")]
        public string GroupLetter { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", Country: " + Country + ", Code: " + Code + ", Goals: " + Goals + ", Penalties: " + Penalties + ", AlternateName: " + AlternateName + ", FifaCode: " + FifaCode + ", GroupId: " + GroupId + ", GroupLetter: " + GroupLetter;
        }
    }

    public partial class Team
    {
        public static Team[] FromJson(string json) => JsonConvert.DeserializeObject<Team[]>(json, Converter.Converter.TeamSettings);
    }

}
