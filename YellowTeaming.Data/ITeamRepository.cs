using System;
using System.Collections.Generic;
using System.Text;
using YellowTeaming.Data.Entities;

namespace YellowTeaming.Data
{
    public interface ITeamRepository
    {
        Member GetMemberById(int id);
        IReadOnlyList<Member> GetAllMembers();
    }
}
