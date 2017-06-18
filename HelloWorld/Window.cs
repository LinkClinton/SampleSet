using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico;
using Mico.Objects;

using Builder;
using Presenter;

namespace HelloWorld
{
    public class Window : GenericWindow
    {
        private Surface surface;

        private FpsCounter fpsCounter;

        public Window((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true)
            {
                BackGround = (1, 0, 0, 1)
            };

            IsVisible = true;

            Micos.Add(fpsCounter = new FpsCounter());
        }

        public override void OnUpdate(object sender)
        {
            Title = Program.AppName + " FPS: " + fpsCounter.Fps.ToString();

            Micos.Exports();

            Micos.Update();
        }
    }
}
