﻿using System;
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
            context.Done<object>(null);
        }
    }
}