using Borders.Enums;
using System;

namespace Borders.Entities
{
    public class LicitationLink
    {
        public Guid PkLicitationLink { get; set; }
        public string Link { get; set; }
        public LicitationLinkStatus Status { get; set; }
    }
}
