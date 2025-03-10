using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DocumentService
    {
        private readonly MajorRepository _repo;

        public DocumentService(MajorRepository repo)
        {
            _repo = repo;
        }
    }
}
