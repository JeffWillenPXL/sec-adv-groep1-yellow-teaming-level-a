using System;
using System.Collections.Generic;
using System.Text;
using YellowTeaming.Data.Entities;
using System.Linq;

namespace YellowTeaming.Data
{
    public class PoemDummyRepository : IPoemRepository
    {
        private readonly IReadOnlyList<Poem> _poems;

        public PoemDummyRepository()
        {
            _poems = new List<Poem>
            {
                new Poem
                {
                    Id = 1,
                    Content = "Roses are red, violets are blue. I don't trust the client and neither should you."
                }
            };
        }
        public IReadOnlyList<Poem> GetAllPoems()
        {
            return _poems;
        }

        public Poem GetFirstPoem()
        {
            return _poems.FirstOrDefault();
        }

        public Poem GetPoemById(int id)
        {
            return _poems.FirstOrDefault(p => p.Id == id);
        }
    }
}
