using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloBlend
{
    class Program
    {
        public static string AppName => "HelloBlend";

        static void Main(string[] args)
        {
            Resource.Initalize();

            Application.Add(new Window((AppName,
                800 * (int)Engine.AppScale, 600 * (int)Engine.AppScale)));

            Application.RunLoop();
        }
    }
}
