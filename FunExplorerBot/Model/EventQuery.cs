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
        
            
        [Prompt("What types of events are you interested in? {||} ")]
        public List<EventType> EventTypes;
        [Prompt("Choose tags you want to query {||}")]
       
        public List<Tag> Tags;

    }
}