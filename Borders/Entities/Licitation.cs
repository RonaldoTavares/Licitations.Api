using Borders.Enums;
using System;

namespace Borders.Entities
{
    public class Licitation
    {
        public Guid Id { get; set; }
        public string Edital { get; set; }
        public string Objeto { get; set; }
        public string OrgaoName { get; set; }
        public string OrgaoDocument { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAbertura { get; set; }
        public LicitationStatus Status { get; set; }
        public string Link { get; set; }
    }
}
