using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloTexture
{
    class Program
    {

        public static string AppName => "HelloTexture";

        static void Main(string[] args)
        {
            Resource.Initalize();

            Application.Add(new Window((AppName,
                (int)(Engine.AppScale * 800), (int)(Engine.AppScale * 600))));

            Application.RunLoop();

        }
    }
}
