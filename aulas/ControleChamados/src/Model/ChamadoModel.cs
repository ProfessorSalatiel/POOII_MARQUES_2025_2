using System;

namespace Model
{
    public class ChamadoModel
    {
        //bigint = long
        public long Id { get; set; }
        //varchar = string
        public string Descricao { get; set; }
        //bit = bool
        public bool Status { get; set; }
        //getdate = datetime
        public DateTime DataCriacao { get; set; }
    }
}
