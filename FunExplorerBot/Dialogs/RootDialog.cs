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
        public const string CreateEventOption = "CreateEvent";
        public const string SearchEventOption = "SearchEvent";

        public async Task StartAsync(IDialogContext context)
        {
           // await context.PostAsync("Welcome to the fun explorer bot! How may I help you?");
            context.Wait(MessageReceivedAsync);           
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await context.PostAsync("Welcome to fun explorer bot! How may I help you?");

           
           // context.Call(Util.Helper.MakeEventDialog(), ResumeAfterOptionDialog ); 
           this.ShowOptions(context);      
            
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, 
                this.OnOptionSelected, 
                new string[] {CreateEventOption, SearchEventOption}, 
                "What do you want to do?",
                "Sorry, not a valid choice",
                3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case CreateEventOption:
                        context.Call(Util.Helper.MakeEventDialog()   , this.ResumeAfterOptionDialog);
                        break;

                    case SearchEventOption:
                        context.Call(Util.Helper.MakeEventQueryDialog(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}