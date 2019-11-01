using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public partial class Repository
    {
        private MainDbContext myContext = null;

        public Repository(MainDbContext newContext)
        {
            myContext = newContext;
        }



    }
}
