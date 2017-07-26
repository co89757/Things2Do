using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AdaptiveCards;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
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
        [Prompt("Give your event a brief title")]
        public string Summary;
        [Prompt("How would you describle your event?")]
        public string Description;
        [Prompt("Add tags for this event to help others find you!{||}")]
        public List<Tag> Tags;


        public string ChanelId;
        public string CreatorId;
        public string CreatorName;

        public Attachment ToCard()
        {
             
            //TODO
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new TextBlock()
            {
                Text = $"{this.Type} Event: {this.Summary}",
                Size = TextSize.Large,
                Weight = TextWeight.Bolder
            });
            card.Body.Add(
                new TextBlock()
                {
                    Text = $"Location: {Location}",
                    Size = TextSize.Medium,
                    Weight = TextWeight.Normal
                }
                );
            card.Body.Add(
                new TextBlock()
                {
                    Text = $"When: {Time}",
                    Size = TextSize.Medium,
                    Weight = TextWeight.Normal
                }
                );
            card.Body.Add(
                new TextBlock()
                {
                    Text = $"What: {Description}",
                    Size = TextSize.Medium,
                    Weight = TextWeight.Normal
                }
                );
            card.Actions.Add(new HttpAction()
            {
                Title = "I'm interested"
                
            });
            card.Actions.Add(new HttpAction()
            {
                Title = "Not for me"
            });

            Attachment attachment = new Attachment()
            {
                ContentType =  AdaptiveCard.ContentType,
                Content = card
            };
            return attachment;
        }

        public string EventKey
        {
            get { return $"{ChanelId}:{Type}"; }
        }

    }
}