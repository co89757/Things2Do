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
        public EventType EventType;

        public string ChanelId;

        public string ToKey()
        {
            return $"{ChanelId}:{EventType}";
        }

    }
}