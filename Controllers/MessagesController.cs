using System.Threading.Tasks;
using System.Web.Http;
using SimpleEchoBot.Modelo;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using SimpleEchoBot;
using System.Net;
using System;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {

        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // Reviso si hay actividad en los mensajes
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                    // llamar al Dialogo MakeRootDialog
                    await Conversation.SendAsync(activity, MakeLuisDialog);
                
                  // grabo la actividad.
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity replyMessage = activity.CreateReply(sb.ToString());
                    await connector.Conversations.ReplyToActivityAsync(replyMessage);
              
                }
                else
                {
                    // This was not a Message activity
                    HandleSystemMessage(activity);
                }
                // Send response
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }

            private static IDialog<Formflow> MakeLuisDialog()
        {
            return Chain.From(() => new LUISDialog(Formflow.BuildForm));
        }



        private void HandleSystemMessage(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}