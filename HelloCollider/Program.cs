using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloCollider
{
    public class Program
    {
        public static string AppName => "HelloCollider";

        static void Main(string[] args)
        {
            Application.Add(new Window(AppName, (int)(800 * Engine.AppScale), (int)(600 * Engine.AppScale)));

            Application.RunLoop();
        }
    }
}
