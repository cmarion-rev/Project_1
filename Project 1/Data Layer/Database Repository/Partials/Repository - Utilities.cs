using Data_Layer.Data_Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository
    {
        public async Task<List<State>> GetStates()
        {
            List<State> results = await myContext.States.ToListAsync();

            return results;
        }

        public async Task<State> GetState(int ID)
        {
            State result = null;
            
            try
            {
                result = await myContext.States.Where(s => s.ID == ID).FirstOrDefaultAsync();
            }
            catch (Exception WTF)
            {
                // Log error.
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {
            
            }

            return result;
        }
    }
}