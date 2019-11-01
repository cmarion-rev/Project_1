using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using Data_Layer.View_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    interface IRepository: IRepository_Customer, IRepository_Account
    {
      
    }
}
