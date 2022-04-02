using Makale.DataAccessLayer;
using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.BusinessLayer
{
    public class NoteYonet
    {
        Repository<Note> repo_not = new Repository<Note>();
        public List<Note> NotListesi()
        {
            return repo_not.List();
        }
    }
}
