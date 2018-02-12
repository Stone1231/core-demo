using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreDemo.Entity
{
    public class StudClub
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ClubId { get; set; }
        public ClubM Club { get; set; }
    }
}
