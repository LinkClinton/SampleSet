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

namespace HelloTexture
{
    public class Window : GenericWindow
    {
        private Present presenter;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private InputLayout inputLayout;
        private ResourceLayout resourceLayout;
        private GraphicsPipelineState graphicsPipelineState;

        private FpsCounter fpsCounter;

        public Window(string Title, int Width, int Height) : base(Title, Width, Height)
        {
            presenter = new Present(Handle, true);

            vertexShader = new VertexShader(Properties.Resources.shader, "vs_main");
            pixelShader = new PixelShader(Properties.Resources.shader, "ps_main");

            inputLayout = new InputLayout(
                new InputLayout.Element[2]
                {
                    new InputLayout.Element("POSITION", ElementSize.eFloat3),
                    new InputLayout.Element("TEXCOORD", ElementSize.eFloat2)
                });

            resourceLayout = new ResourceLayout(
                new ResourceLayout.Element[] {
                    new ResourceLayout.Element(ResourceType.ConstantBufferTable,0,2),
                    new ResourceLayout.Element(ResourceType.ShaderResourceTable,0,1)
                },
                new StaticSampler[] {
                    new StaticSampler(TextureAddressMode.Clamp,TextureFilter.MinMagMipLinear)
                });

            graphicsPipelineState = new GraphicsPipelineState(vertexShader, pixelShader,
                inputLayout, resourceLayout, new RasterizerState(), new DepthStencilState(), new BlendState());

            Micos.Camera = new Camera()
            {
                Project = Mico.Math.TMatrix.CreatePerspectiveFieldOfViewLH((float)Math.PI * 0.55f,
                    800f / 600f, 1.0f, 2000.0f)
            };

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;

            Micos.Add(fpsCounter = new FpsCounter());

            Micos.Add(new Grid(20, 20));

            IsVisible = true;
        }

        public override void OnDestroyed(object sender)
        {
        }

        public override void OnKeyEvent(object sender, KeyEventArgs e)
        {
        }

        public override void OnMouseClick(object sender, MouseClickEventArgs e)
        {
        }

        public override void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
        }

        public override void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        public override void OnSizeChange(object sender, SizeChangeEventArgs e)
        {
        }

        public override void OnUpdate(object sender)
        {
            GraphicsPipeline.Open(graphicsPipelineState, presenter);

            Micos.Exports();

            Title = Program.AppName + " FPS: " + fpsCounter.Fps.ToString();

            GraphicsPipeline.Close();
            
            Micos.Update();

            GraphicsPipeline.WaitFlush();
        }

    }
}
