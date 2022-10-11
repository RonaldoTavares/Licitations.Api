using Borders.Enums;
using System;

namespace Borders.Entities
{
    public class Licitation
    {
        public Guid PkLicitation { get; set; }
        public string Notice { get; set; }
        public string Object { get; set; }
        public string OrganName { get; set; }
        public string OrganDocument { get; set; }
        public decimal Value { get; set; }
        public DateTime OpeningDate { get; set; }
        public LicitationStatus Status { get; set; }
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
