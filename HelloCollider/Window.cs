using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Math;
using Mico.Objects;

using Builder;
using Presenter;

namespace HelloCollider
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
            IsVisible = true;

            presenter = new Present(Handle, true);

            vertexShader = new VertexShader(Properties.Resources.shader, "vs_main");
            pixelShader = new PixelShader(Properties.Resources.shader, "ps_main");

            inputLayout = new InputLayout(
                new InputLayout.Element[] {
                    new InputLayout.Element("POSITION", ElementSize.eFloat3),
                    new InputLayout.Element("COLOR", ElementSize.eFlaot4)
                });

            resourceLayout = new ResourceLayout(
                new ResourceLayout.Element[]
                {
                    new ResourceLayout.Element(ResourceType.ConstantBufferView,0),
                    new ResourceLayout.Element(ResourceType.ConstantBufferView,1)
                });

            graphicsPipelineState = new GraphicsPipelineState(vertexShader, pixelShader, inputLayout,
                resourceLayout, new RasterizerState() { FillMode = FillMode.Wireframe}, new DepthStencilState(true), new BlendState());

            Micos.Camera = new Camera();
            Micos.Camera.Transform.Position = new Vector3(0, 0, -100);
            
            Micos.Camera.Project = TMatrix.CreatePerspectiveFieldOfViewLH((float)Math.PI * 0.55f,
                (float)Width / Height, 1.0f, 2000.0f);

            for (int i = 0; i < EventThing.ObjectCount; i++)
            {
                Cube cube = new Cube(10, 10, 10);
                cube.Transform.Position = new Vector3(EventThing.INT, EventThing.INT, EventThing.INT);
                cube.Forward = Vector3.Normalize(new Vector3(EventThing.INT, EventThing.INT, 0));
                cube.RotateSpeed = new Vector3(EventThing.FLOAT, EventThing.FLOAT, 0);
                Micos.Add(cube);
            }

            Micos.Add(fpsCounter = new FpsCounter());

            Resource.cameraBuffer.view = Micos.Camera;
            Resource.cameraBuffer.proj = Micos.Camera.Project;
            Resource.cameraBuffer.eyePos = new Vector4(Micos.Camera.Transform.Position, 1);

            CameraBuffer.View.Update(ref Resource.cameraBuffer);
        }

        public override void OnMouseClick(object sender, MouseClickEventArgs e)
        {
            if (e.IsDown is true && e.Which is MouseButton.LeftButton)
            {
                Cube cube = Micos.Pick(Camera.NdcX(e.X, Width),
                    Camera.NdcY(e.Y, Height)) as Cube;

                if (cube is null) return;
                Micos.Remove(cube);
            }
            base.OnMouseClick(sender, e);
        }

        public override void OnUpdate(object sender)
        {
             GraphicsPipeline.Open(graphicsPipelineState, presenter);

            Micos.Exports();
            
            SetTitle(Program.AppName + " FPS: " + fpsCounter.Fps);
            
            GraphicsPipeline.Close();
            
            Micos.Update();
            

            base.OnUpdate(sender);
        }
    }
}
