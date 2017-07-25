using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace FunExplorerBot.Model
{
    public enum EventType
    {
        Eat = 1,
        Sports = 2,
        Game = 3,
    }

    [Serializable]
    public class Event
    {

        public static IForm<Event> BuildForm()
        {
            return new FormBuilder<Event>()
                .Message("Get ready to create your event!")
                .Build();

        }
        [Prompt("What is the type of your event? {||} ")]
        public EventType Type;
        [Prompt("When do you plan to start this event?")]
        public DateTime Time;
        [Prompt("What is the duration of this event?")]
        public DateTimeOffset Duration;
        [Prompt("Where do you want to have this event?")]
        public string Location;
        [Prompt("How would you describle your event?")]
        public string Description;
        [Prompt("Add tags for this event to help others find you!")]
        public List<string> Tags;

    }
}