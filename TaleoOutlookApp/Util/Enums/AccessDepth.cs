using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Enums
{
    public enum AccessDepth
    {
        None,
        Only_AuthToken,
        Only_LogInToken,
        Only_Cookies,
        Upto_Cookies
    }
}
