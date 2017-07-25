using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FunExplorerBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

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
                .OnCompletion(processEventCreate)
                .Build();
        }

        private OnCompletionAsyncDelegate<Event> processEventCreate = async (context, state) =>
        {
            //TODO business logic for event creation, like sending notification etc. 
            await context.PostAsync($"Creating event for you...");
        };

        private async Task ResumeEventCreateFormDialog(IDialogContext context, IAwaitable<Event> result)
        {
            //TODO logic to send notif on resume
            context.Done<object>(null);
        }
    }
}