using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Exeptions.BadRequest
{
    public class BadRequestExeption(string message) : Exception(message)
    {

    }
}
