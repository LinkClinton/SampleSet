using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Add(new HelloWindow(("HelloWorld", (int)(Manager.AppScale * 800),
                (int)(Manager.AppScale * 600))));

            Application.RunLoop();

        }
    }
}
