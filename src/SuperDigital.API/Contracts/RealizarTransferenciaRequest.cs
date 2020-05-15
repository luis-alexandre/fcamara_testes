namespace SuperDigital.API.Contracts
{
    public class RealizarTransferenciaRequest
    {
        public int ContaOrigemNumero { get; set; }

        public int ContaDestinoNumero { get; set; }

        public decimal Valor { get; set; }
    }
}
