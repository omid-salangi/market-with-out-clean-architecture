using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Models;

namespace market.Data.Repositories
{
    public  interface IGroupRepositories 
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ShowGroupViewModel> GetGrouptForShow();

    }
}
