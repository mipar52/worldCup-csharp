using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Converter;
namespace WorldCupData.Model.GroupResults
{
    public partial class GroupResults
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("letter")]
        public string Letter { get; set; }

        [JsonProperty("ordered_teams")]
        public OrderedTeam[] OrderedTeams { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", Letter: " + Letter + ", OrderedTeams: [" + string.Join(", ", OrderedTeams.Select(t => t.ToString())) + "]";
        }
    }
    public partial class GroupResults
    {
        public static GroupResults[] FromJson(string json) => JsonConvert.DeserializeObject<GroupResults[]>(json, Converter.Converter.GroupResultsSettings);
    }

}
