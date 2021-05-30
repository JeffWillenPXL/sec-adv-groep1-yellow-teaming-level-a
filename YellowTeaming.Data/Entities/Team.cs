using System;
using System.Collections.Generic;
using System.Text;

namespace YellowTeaming.Data.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public IList<Member> Members { get; set; }
    }
}
