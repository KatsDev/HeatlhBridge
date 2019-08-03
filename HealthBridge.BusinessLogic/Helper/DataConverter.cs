using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.Helper
{
    public class DataConverter
    {
        public static float ToSingle(decimal value)
        {
            return (float)value;
        }
    }
}
