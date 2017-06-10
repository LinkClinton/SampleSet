using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloCube
{
    class Program
    {

        public static string AppName = "HelloCube";

        static void Main(string[] args)
        {
            Application.Add(new Window((AppName, (int)Manager.AppScale * 800,
                (int)Manager.AppScale * 600)));

            Application.RunLoop();
        }
    }
}
