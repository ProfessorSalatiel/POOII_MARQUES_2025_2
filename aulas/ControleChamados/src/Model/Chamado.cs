using System;

namespace Model
{
    public class Chamado
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}