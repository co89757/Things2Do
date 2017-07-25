using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FunExplorerBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace FunExplorerBot.Util
{
    public static class Helper
    {
        internal static IDialog<Event> MakeEventDialog()
        {
            return Chain.From(() => FormDialog.FromForm(Event.BuildForm, FormOptions.PromptInStart));
        }

        internal static IDialog<EventQuery> MakeEventQueryDialog()
        {
            return Chain.From(() => FormDialog.FromForm(EventQuery.BuildForm, FormOptions.PromptInStart));
        }
    }
}