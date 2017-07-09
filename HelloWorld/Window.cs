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
        private Present presenter;

        private FpsCounter fpsCounter;

        public Window(string Title, int Width, int Height) : base(Title, Width, Height)
        {
            presenter = new Present(Handle, true);

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
