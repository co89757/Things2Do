using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FunExplorerBot.Model
{   [JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        Eat = 1,
        Sports = 2,
        Game = 3,
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Tag
    {
        Movie = 1,
        Football,
        PingPong,
        Cardgame,
        Food,
        Basketball
    }

    [Serializable]
    public class Event
    {

        
        [Prompt("What is the type of your event? {||} ")]
        public EventType Type;
        [Prompt("When do you plan to start this event?")]
        public DateTime Time;
        
        [Prompt("Where do you want to have this event?")]
        public string Location;
        [Prompt("How would you describle your event?")]
        public string Description;
        [Prompt("Add tags for this event to help others find you!{||}")]
        public List<Tag> Tags;


        public string ChanelId;
        public string CreatorId;
        public string CreatorName;

        public string EventKey
        {
            get { return $"{ChanelId}:{Type}"; }
        }

    }
}