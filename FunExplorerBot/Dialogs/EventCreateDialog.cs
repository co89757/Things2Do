using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FunExplorerBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json;

namespace FunExplorerBot.Dialogs
{
    [Serializable]
    public class EventCreateDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the Event Creator!");

            var eventCreateDialog = FormDialog.FromForm(BuildEventCreateForm, FormOptions.PromptInStart);
            context.Call(eventCreateDialog, ResumeEventCreateFormDialog);

        }

        private IForm<Event> BuildEventCreateForm()
        {
            return new FormBuilder<Event>()
                .Message("Get ready to create your event!")
                .Field(nameof(Event.Type))
                .Field(nameof(Event.Time))
                .Field(nameof(Event.Location))
                .Field(nameof(Event.Summary))
                .Field(nameof(Event.Description))
                .Field(nameof(Event.Tags))
                .OnCompletion(processEventCreate)
                .Build();
        }

        private OnCompletionAsyncDelegate<Event> processEventCreate = async (context, ev) =>
        {
            //TODO business logic for event creation, like sending notification etc. 
            await context.PostAsync($"start to persist event !");

            ev.ChanelId = context.Activity.ChannelId;
            ev.CreatorId = context.Activity.From.Id;
            ev.CreatorName = context.Activity.From.Name;
            string ekey = ev.EventKey;
            var eventJson = JsonConvert.SerializeObject(ev);
            await context.PostAsync($"{eventJson}");
            //var ee = JsonConvert.DeserializeObject<Event>(eventJson);
            var db = new RedisManager();
            db.Db.SetAdd(ekey, eventJson);
            //var v = db.Db.SetMembers(ekey);
            await context.PostAsync($"Creating event for you...");
        };

        private async Task ResumeEventCreateFormDialog(IDialogContext context, IAwaitable<Event> result)
        {
            //TODO logic to send notif on resume
            //Event ev = await result;
            //    //notify 
            //ev.ChanelId = context.Activity.ChannelId;
            //ev.CreatorId = context.Activity.From.Id;
            //ev.CreatorName = context.Activity.From.Name;
            //var eventJson = JsonConvert.SerializeObject(ev);
            ////var ee = JsonConvert.DeserializeObject<Event>(eventJson);
            //var db = new RedisManager();
            //db.Db.SetAdd(ev.EventKey, eventJson);
            await context.PostAsync($"woohoo!!");
            context.Done<object>(null);
        }
    }
}