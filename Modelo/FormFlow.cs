using Microsoft.Bot.Builder.FormFlow;
using System;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;

namespace SimpleEchoBot.Modelo
{

    public enum OpcionOperaciones
    {
        CirugiaRefractivaLasik = 1, CirugiaRefractivaPrk = 2, FacoemulsificacionLenteIntraocular = 3, ImplanteDeIcl = 4, ImplanteDeAnillosCorneales = 5,
        CirugiaCombinada = 6, Vitrectomia = 7, ExtirpacionDeChalazion = 8, ExtirpacionDePterigion = 9, FotocoagulacionRetinal = 10, InyeccionIntravitrea = 11, Blefaroplastia = 12
    }
    public enum OpcionOjoOperado { Derecho = 1, Izquierdo = 2, Ambos = 3 }
    public enum SensacionDeArenilla { Si = 1, No = 2}
    public enum SensacionArdor { Si = 1, No = 2 }
    public enum NivelDolores { uno = 1, dos = 2, tres = 3, cuatro = 4, cinco = 5, seis = 6, siete = 7, ocho = 8, nueve = 9, diez = 10 }
    public enum NivelVision { DistingueLuz = 1, DistingueMovimiento = 2, DintingueFormas = 3, VeAlgoBorroso = 4 }
    [Serializable]
    public class Formflow
    {
        [Prompt("Por favor, ingrese el nombre completo del paciente: {||}")]
        public string NombreDePaciente;
        [Prompt("Por favor, ingrese run del paciente: {||}")]
        public string RunDePaciente;
        [Prompt("Por favor, ingrese el nombre del cirujano: {||}")]
        public string MedicoOperante;
        [Prompt("Por favor, ingrese un número de contacto: {||}")]
        [Pattern(@"(<Undefined control sequence>\d)?\s*\d{3}(-|\s*)\d{4}")]
        public string Telefono;
        [Prompt("Por favor, seleccione la operación que le realizaron: {||}")]
        public OpcionOperaciones OperaciónRealizada;
        [Prompt("Por favor, seleccione el o los ojos que le operaron: {||}")]
        public OpcionOjoOperado OjoOperado;
        [Prompt("Por favor, ingrese motivo de la consulta: {||}")]
        public string Consulta;
        [Prompt("Por favor, indique si tiene sensación de arenilla en el ojo: {||}")]
        public SensacionDeArenilla Arenilla;
        [Prompt("Por favor, indique si tiene sensación de ardor en el ojo: {||}")]
        public SensacionArdor Ardor;
        [Prompt("Por favor, indique de 1 a 10 el nivel de dolor que siente, dónde 1 es nada y 10 es mucho: {||}")]
        public NivelDolores NivelDolor;
        [Prompt("Por favor, indique en base a su visión cuál de las opciones es más acertada: {||}")]
        public NivelVision Visión;
        [Prompt("Por favor, ingrese algún detalle adicional: {||}")]
        public string Descripcion;
        public static IForm<Formflow> BuildForm()
        {
            return new FormBuilder<Formflow>()
            .Message("Bienvenido al bot de asistencia !")
            .OnCompletion(async (context, Formflow) =>
            {
                context.PrivateConversationData.SetValue<bool>(
                       "ProfileComplete", true);
                context.PrivateConversationData.SetValue<string>(
                        "NombreDePaciente", Formflow.NombreDePaciente);
                context.PrivateConversationData.SetValue<string>(
                        "Run", Formflow.RunDePaciente);
                context.PrivateConversationData.SetValue<string>(
                        "MedicoOperante", Formflow.MedicoOperante);
                context.PrivateConversationData.SetValue<string>(
                        "Telefono", Formflow.Telefono);
                context.PrivateConversationData.SetValue<Enum>(
                        "OperaciónRealizada", Formflow.OperaciónRealizada);
                context.PrivateConversationData.SetValue<Enum>(
                        "OjoOperado", Formflow.OjoOperado);
                context.PrivateConversationData.SetValue<string>(
                         "Consulta", Formflow.Consulta);
                context.PrivateConversationData.SetValue<Enum>(
                        "Arenilla", Formflow.Arenilla);
                context.PrivateConversationData.SetValue<Enum>(
                        "Ardor", Formflow.Ardor);
                context.PrivateConversationData.SetValue<Enum>(
                        "NivelDolor", Formflow.NivelDolor);
                context.PrivateConversationData.SetValue<Enum>(
                        "Visión", Formflow.Visión);
                context.PrivateConversationData.SetValue<string>(
                        "Descripcion", Formflow.Descripcion);
                // Me indica si los datos son correctos
                await context.PostAsync("Datos guardados, sera comunicado en breves minutos.");
            })
            .Build();

        }


    }

}