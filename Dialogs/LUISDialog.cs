using SimpleEchoBot.Modelo;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace SimpleEchoBot
{
    [LuisModel("16fab99f-253e-41c1-9077-f00b57392982", "104c33eac9374b10bcfb891faf7a0a10")]
    [Serializable]
    public class LUISDialog : LuisDialog<Formflow>
    {
        private Func<IForm<Formflow>> buildForm;
  
        public LUISDialog(Func<IForm<Formflow>> buildForm)
        {
            this.buildForm = buildForm;
        }

        //Programa para None Intents
        [LuisIntent("")]
        private async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento no estoy programado para este tipo de preguntas.");
            context.Wait(MessageReceived);
        }

        // Programa para Saludo Intents
        [LuisIntent("Saludo")]
        private async Task Saludo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hola! ¿En qué puedo ayudar?.");
            await Task.Delay(2000);
            context.Wait(MessageReceived);
        }

        // Programa para BuscaOperación Intents
        [LuisIntent("BuscaOperación")]
        private async Task BuscaOperación(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Entendido, ¿Podría darnos unos datos para atenderlo? Por favor.");
            var Formulario = Chain.From(() => FormDialog.FromForm(Formflow.BuildForm));
            context.Call(Formulario, CallBack);
        }


        private async Task CallBack(IDialogContext context, IAwaitable<Formflow> result)
        {
            context.Wait(MessageReceived);
        }
    }
}