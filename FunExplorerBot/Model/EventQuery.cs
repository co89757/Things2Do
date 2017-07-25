using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace FunExplorerBot.Model
{
    [Serializable]
    public class EventQuery
    {
        public static IForm<EventQuery> BuildForm()
        {
            return new FormBuilder<EventQuery>().Message("Get ready to search some fun!").Build();
        }
            
        [Prompt("What types of events are you interested in? {||} ")]
        public List<EventType> EventTypes;
        [Prompt("Choose tags you want to query")]
        public List<string> Tags;

    }
}