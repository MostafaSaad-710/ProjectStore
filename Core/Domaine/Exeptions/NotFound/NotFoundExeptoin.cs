using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Exeptions.NotFound
{
    public class NotFoundExeptoin(string message) : Exception(message)
    {

    }
}
