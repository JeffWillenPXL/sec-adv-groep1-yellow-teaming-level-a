using System;
using System.Collections.Generic;
using System.Text;
using YellowTeaming.Data.Entities;

namespace YellowTeaming.Data
{
    public interface IPoemRepository
    {
        Poem GetPoemById(int id);
        IReadOnlyList<Poem> GetAllPoems();
        Poem GetFirstPoem();

    }
}
