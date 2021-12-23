using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.InjectOrnek
{
    public class MyDependency : IMyDependency
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
