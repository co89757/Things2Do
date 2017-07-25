using System;
using System.Threading.Tasks;
using FunExplorerBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace FunExplorerBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the fun explorer bot! How may I help you?");
            context.Wait(MessageReceivedAsync);           
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;


            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            context.Call(Util.Helper.MakeEventDialog(), ResumeAfterNewEventDialog );       
            
        }

        private async Task ResumeAfterNewEventDialog(IDialogContext context, IAwaitable<Event> result)
        {
            var ev = await result;
            await context.PostAsync($"You just completed your event creation. location: {ev.Location} ");
            context.Wait(MessageReceivedAsync);
        }
    }
}