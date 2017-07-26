using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FunExplorerBot.Model;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace FunExplorerBot.Dialogs
{
    [Serializable]
    public class EventSearchDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Let's start the search!");
            var searchFormDialog = FormDialog.FromForm(BuildEventSearchForm, FormOptions.PromptInStart);
            context.Call(searchFormDialog, ResumeEventSearchFormDialog);

        }

        private IForm<EventQuery> BuildEventSearchForm()
        {

            return new FormBuilder<EventQuery>()
                .Message("Get ready to create your event!")
                .Field(nameof(EventQuery.EventType))
                .OnCompletion( processEventSearch )
                .Build();
        }

        private OnCompletionAsyncDelegate<EventQuery> processEventSearch = async (context, state) =>
        {
            //TODO business logic for event creation, like sending notification etc. 
            await context.PostAsync($"searching event for you...");
        };

        private async Task ResumeEventSearchFormDialog(IDialogContext context, IAwaitable<EventQuery> result)
        {
            //TODO show available events
            //var conn = new ConnectorClient(new Uri(context.Activity.ServiceUrl));
            EventQuery query = await result;
            query.ChanelId = context.Activity.ChannelId;
            var rd = new RedisManager();
            var events = rd.Db.SetMembers(query.ToKey());
            string firstEvent = (string) events.FirstOrDefault();
            if (string.IsNullOrEmpty(firstEvent))
            {
                await context.PostAsync("We find no matching event, sorry.. try a different search?");
            }
            else
            {
                int count = events.Count();
                await context.PostAsync($"We found {count} events matching your search!");
                Event[] results = events.Select(ee => JsonConvert.DeserializeObject<Event>(ee)).ToArray();
                //convert them to card attachments
                var resultCards = results.Select(r => r.ToCard());
                Event e = JsonConvert.DeserializeObject<Event>(firstEvent);
                var reply = context.MakeMessage();
                reply.AttachmentLayout = AttachmentLayoutTypes.List;
                reply.Attachments = new List<Attachment>(resultCards) ;
                await context.PostAsync(reply);
            }
            
            context.Done<object>(null);
        }
    }
}