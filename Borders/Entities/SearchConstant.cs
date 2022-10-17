using Borders.Enums;
using System;

namespace Borders.Entities
{
    public class SearchConstant
    {
        public Guid PkSearchConstant { get; set; }
        public Guid FkOrgan { get; set; }
        public string Constant { get; set; }
        public SearchConstants Type { get; set; }
    }
}
