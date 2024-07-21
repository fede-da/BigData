using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RagApp.DAL.PostgresModels
{
    [Table("dipendenti")]
    public class Dipendente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("posizione")]
        public string Posizione { get; set; } = string.Empty;

        [Column("dipartimento")]
        public string Dipartimento { get; set; } = string.Empty;

        [Column("eta")]
        public int Eta { get; set; }

        [Column("data_assunzione")]
        public DateTime DataAssunzione { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;
    }
}
