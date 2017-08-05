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

namespace HelloBlend
{
    public class Window : GenericWindow
    {
        public static Present prenster;
        public static VertexShader vertexShader;
        public static PixelShader pixelShader;
        public static InputLayout inputLayout;
        public static ResourceLayout resourceLayout;

        public static GraphicsPipelineState graphicsPipelineState;

        public static BlendState blendState;

        private FpsCounter fpsCounter;

        public void InitalizeBlendState()
        {
            blendState = new BlendState(true)
            {
                SourceBlend = BlendOption.SourceAlpha,
                DestinationBlend = BlendOption.InverseSourceAlpha,
                BlendOperation = BlendOperation.Add,
            };
        }

        public Window(string Title, int Width, int Height) : base(Title,Width,Height)
        {
            prenster = new Present(Handle, true);
            
            vertexShader = new VertexShader(Properties.Resources.shader, "vs_main");
            pixelShader = new PixelShader(Properties.Resources.shader, "ps_main");

            inputLayout = new InputLayout(
                new InputLayout.Element[]
                {
                    new InputLayout.Element("POSITION", ElementSize.eFloat3)
                });

            resourceLayout = new ResourceLayout(
                new ResourceLayout.Element[2] {
                    new ResourceLayout.Element(ResourceType.ConstantBufferView, 0),
                    new ResourceLayout.Element(ResourceType.ConstantBufferView, 1)
                }, null);

            InitalizeBlendState();

            graphicsPipelineState = new GraphicsPipelineState(vertexShader, pixelShader,
                inputLayout, resourceLayout, null, new DepthStencilState(), blendState);

            Micos.Camera = new Camera()
            {
                Project = Mico.Math.TMatrix.CreatePerspectiveFieldOfViewLH((float)Math.PI * 0.55f,
                    800f / 600f, 1.0f, 2000.0f)
            };

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;
            
            Micos.Add(new Cube(new Vector3(0, 0, 3),
                new Vector3(3, 3, 3), new Vector4(1, 0, 0, 1), true));

            Micos.Add(new Cube(new Vector3(0, 0, 0),
                new Vector3(5, 5, 5), new Vector4(0, 1, 1, 0.5f), false));

            Micos.Add(fpsCounter = new FpsCounter());

            CameraBuffer cameraBuffer = CameraBuffer.FromCamera(Micos.Camera);

            Resource.CameraBuffer.Update(ref cameraBuffer);

            IsVisible = true;
        }
    
        private void TestKeyInput()
        {
            string result = "Key is down: ";

            for (int i = 0; i < 255; i++)
            {
                if (Application.IsKeyDown((KeyCode)i))
                    result += " " + ((KeyCode)i).ToString();
            }

            Console.WriteLine(result);
        }

        public override void OnUpdate(object sender)
        {
            GraphicsPipeline.Open(graphicsPipelineState, prenster);

            Micos.Exports();

            Title = Program.AppName + " FPS: " + fpsCounter.Fps.ToString();

            GraphicsPipeline.Close();

            Micos.Update();

            GraphicsPipeline.WaitFlush();
        }
    }
}
