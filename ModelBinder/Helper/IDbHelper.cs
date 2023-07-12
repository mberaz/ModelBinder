using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBinder.Helper
{
    public interface IDbHelper
    {
       Task< Dictionary<string, bool>> GetOptions();
    }
}
