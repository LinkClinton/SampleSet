using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Builder;
using Presenter;

namespace HelloWorld
{
    public class HelloWindow : GenericWindow
    {
        private Surface surface;

        public HelloWindow((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true)
            {
                BackGround = (1, 0, 0, 1)
            };

            IsVisible = true;
        }

        public override void OnUpdate(object sender)
        {
            Manager.Surface = surface;

            Manager.ClearObject();

            Manager.FlushObject();
        }
    }
}
