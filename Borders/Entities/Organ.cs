using System;

namespace Borders.Entities
{
    public class Organ
    {
        public Guid PkOrgan { get; set; }   
        public string OrganName { get; set; }
        public string OrganDocument { get; set; }
        public bool Active { get; set; }
        public int LastLicitation { get; set; }
    }
}
