using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WorldCupData.Enums;
using WorldCupData.Converter;

namespace WorldCupData.Model
{
    public partial class Match
    {
        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("fifa_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FifaId { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("attendance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Attendance { get; set; }

        [JsonProperty("officials")]
        public string[] Officials { get; set; }

        [JsonProperty("stage_name")]
        public StageName StageName { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("winner_code")]
        public string WinnerCode { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        [JsonProperty("home_team_events")]
        public TeamEvent[] HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public TeamEvent[] AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatictics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatictics AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at")]
        public DateTimeOffset LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at")]
        public DateTimeOffset? LastScoreUpdateAt { get; set; }

        public override string ToString()
        {
            return $"Venue: {Venue}, Location: {Location}, Status: {Status}, Time: {Time}, FifaId: {FifaId}, Weather: {Weather}, Attendance: {Attendance}, Officials: [{string.Join(", ", Officials)}], StageName: {StageName}, HomeTeamCountry: {HomeTeamCountry}, AwayTeamCountry: {AwayTeamCountry}, Datetime: {Datetime}, Winner: {Winner}, WinnerCode: {WinnerCode}, HomeTeam: {HomeTeam}, AwayTeam: {AwayTeam}, HomeTeamEvents: {HomeTeamEvents}, AwayTeamEvents: {AwayTeamEvents}, HomeTeamStatistics: {HomeTeamStatistics}, AwayTeamStatistics: {AwayTeamStatistics}, LastEventUpdateAt: {LastEventUpdateAt}, LastScoreUpdateAt: {LastScoreUpdateAt}";
        }
    }
    public partial class Match
    {
        public static Match[] FromJson(string json) => JsonConvert.DeserializeObject<Match[]>(json, Converter.Converter.MatchSettings);
    }
}
