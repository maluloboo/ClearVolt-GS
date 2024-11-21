namespace ClearVolt.Models
{
    public class RespostaModel<Resposta>
    {
        public Resposta? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
