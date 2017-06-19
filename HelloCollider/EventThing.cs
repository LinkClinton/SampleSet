using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCollider
{
    public static class EventThing
    {
        public static int ObjectCount => 50;
        public static int XLimit => 100;
        public static int YLimit => 100;
        public static int ZLimit => 100;

        static Random random = new Random();

        public static int INT => random.Next(1, 70);
        public static float FLOAT => (float)random.NextDouble();
    }
}
