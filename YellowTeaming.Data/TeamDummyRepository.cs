using System;
using System.Collections.Generic;
using System.Text;
using YellowTeaming.Data.Entities;
using System.Linq;

namespace YellowTeaming.Data
{
    public class TeamDummyRepository : ITeamRepository
    {
        private readonly IReadOnlyList<Member> _members;

        public TeamDummyRepository()
        {
            _members = new List<Member> {
                new Member
                { Id = 1,
                    FirstName = "Jeff",
                    LastName = "Willen"
                },
                new Member
                {
                    Id = 2,
                    FirstName = "Nadine",
                    LastName = "Vaesen"
                },
                new Member
                {
                    Id = 3,
                    FirstName = "Sigrid",
                    LastName = "Meesters"
                },
                new Member
                {
                    Id = 4,
                    FirstName = "Birgit",
                    LastName = "Panis"
                },
            };
        }

        public IReadOnlyList<Member> GetAllMembers()
        {
            return _members;
        }

        public Member GetMemberById(int id)
        {
            return _members.FirstOrDefault(m => m.Id == id);
        }
    }
}
