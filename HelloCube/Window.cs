using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Objects;

using Builder;
using Presenter;

namespace HelloCube
{
    public class Window : GenericWindow
    {
        private Surface surface;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private InputLayout inputLayout;
        private ResourceLayout resourceLayout;
        private GraphicsPipelineState graphicsPipelineState;

        private FpsCounter fpsCounter;

        public Window((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true);

            vertexShader = new VertexShader(Properties.Resources.shader, "vs_main");
            pixelShader = new PixelShader(Properties.Resources.shader, "ps_main");

            inputLayout = new InputLayout(
                new InputLayout.Element[2]
                {
                    new InputLayout.Element("POSITION", ElementSize.eFloat3),
                    new InputLayout.Element("COLOR", ElementSize.eFlaot4)
                });

            resourceLayout = new ResourceLayout(
                new ResourceLayout.Element[2] {
                    new ResourceLayout.Element(ResourceType.ConstantBufferView, 0),
                    new ResourceLayout.Element(ResourceType.ConstantBufferView, 1)
                }, null);

            graphicsPipelineState = new GraphicsPipelineState(vertexShader, pixelShader,
                inputLayout, resourceLayout, new DepthStencilState());

            Micos.Camera = new Camera()
            {
                Project = Mico.Math.TMatrix.CreatePerspectiveFieldOfViewLH((float)Math.PI * 0.55f,
                    800f / 600f, 1.0f, 2000.0f)
            };

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;

            Micos.Add(new Cube(3, 3, 3));

            Micos.Add(fpsCounter = new FpsCounter());

            IsVisible = true;
        }

        public override void OnUpdate(object sender)
        {
            GraphicsPipeline.Open(graphicsPipelineState, surface);

            Micos.Exports();

            Title = Program.AppName + " FPS: " + fpsCounter.Fps.ToString();

            GraphicsPipeline.Close();

            Micos.Update();
        }

    }
}
