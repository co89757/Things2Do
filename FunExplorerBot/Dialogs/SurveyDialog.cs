using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace FunExplorerBot.Dialogs
{
    [Serializable]
    public class SurveyDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }
}