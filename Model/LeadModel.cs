using Microsoft.Bot.Builder.FormFlow;
using System;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Microsoft.Bot.Sample.SimpleEchoBot.Models
{

    public enum Service { Emergencia = 1, AgendarHora = 2, Consulta = 3 };
    public enum OpcionOperaciones
    {
        CirugiaRefractivaLasik = 1, CirugiaRefractivaPrk = 2, FacoemulsificacionLenteIntraocular = 3, ImplanteDeIcl = 4, ImplanteDeAnillosCorneales = 5,
        CirugiaCombinada = 6, Vitrectomia = 7, ExtirpacionDeChalazion = 8, ExtirpacionDePterigion = 9, FotocoagulacionRetinal = 10, IyeccionIntravitrea = 11, Blefaroplastia = 12
    }
    public enum OpcionOjoOperado { Derecho = 1, Izquierdo = 2, Ambos = 3 }
    public enum OpcionSintomas { SensacionDeArenilla = 1, Ardor = 2, Dolor = 3, PerdidaDeVision = 4 }
    public enum NivelDolores { uno = 1, dos = 2, tres = 3, cuatro = 4, cinco = 5, seis = 6, siete = 7, ocho = 8, nueve = 9, diez = 10 }
    public enum NivelVision { Luz = 1, PercibeMovimiento = 2, DintingueFormas = 3, VeAlgoBorroso = 4 }
    [Serializable]
    public class LeadModel
    {
        public Service ServicioRequerido;
        public string NombreDePaciente;
        public string MedicoOperante;
        [Pattern(@"(<Undefined control sequence>\d)?\s*\d{3}(-|\s*)\d{4}")]
        public string Telefono;
        public OpcionOperaciones OperaciónRealizada;
        public OpcionOjoOperado OjoOperado;
        public List<OpcionSintomas> ElijaSintomas;
        public NivelDolores NivelDolor;
        public NivelVision Visión;
        public string Description;
        public static IForm<LeadModel> BuildForm()
        {
            return new FormBuilder<LeadModel>()
            .Message("Bienvenido al bot de asistencia !")
            .OnCompletion(CreateLeadInCRM)
            .Build();
        }
        private static async Task CreateLeadInCRM(IDialogContext context, LeadModel state)
        {
            await context.PostAsync("Gracias, será contactado en breves minutos.");
            Entity lead = new Entity("lead");
            lead.Attributes["subject"] = "Interested in product " + state.ServicioRequerido.ToString();
            lead.Attributes["lastname"] = state.NombreDePaciente;
            lead.Attributes["description"] = state.Description;
            GetOrganizationService().Create(lead);
        }

        public static OrganizationServiceProxy GetOrganizationService()
        {
            IServiceManagement<IOrganizationService> orgServiceManagement =
            ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri("https://nishantcrm365.crm.dynamics.com/XRMServices/2011/Organization.svc"));
            AuthenticationCredentials authCredentials = new AuthenticationCredentials();
            authCredentials.ClientCredentials.UserName.UserName = "nishant@nishantcrm365.onmicrosoft.com";
            authCredentials.ClientCredentials.UserName.Password = "asadff";
        AuthenticationCredentials tokenCredentials = orgServiceManagement.Authenticate(authCredentials);
            return new OrganizationServiceProxy(orgServiceManagement, tokenCredentials.SecurityTokenResponse);
        }
    }
}