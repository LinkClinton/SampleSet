using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Shapes;
using Presenter;

namespace HelloBlend
{
    public class Cube : Shape
    {
        private static VertexBuffer<Vertex> vertexBuffer;
        private static IndexBuffer<uint> indexBuffer;
        private static float rotate_speed = 2.0f;

        private CubeBuffer cubeBuffer;
        private ConstantBuffer<CubeBuffer> cubeBufferUploader;

        static Cube()
        {
            uint[] index = new uint[36] {
                0,1,2,0,2,3,4,6,5,4,7,6,
                4,5,1,4,1,0,3,2,6,3,6,7,
                1,5,6,1,6,2,4,0,3,4,3,7
            };

            Vertex[] vertex = new Vertex[8];

            float halfwidth = 0.5f;
            float halfheight = 0.5f;
            float halfdepth = 0.5f;

            vertex[0] = new Vertex(-halfwidth, -halfheight, -halfdepth);
            vertex[1] = new Vertex(-halfwidth, halfheight, -halfdepth);
            vertex[2] = new Vertex(halfwidth, halfheight, -halfdepth);
            vertex[3] = new Vertex(halfwidth, -halfheight, -halfdepth);
            vertex[4] = new Vertex(-halfwidth, -halfheight, halfdepth);
            vertex[5] = new Vertex(-halfwidth, halfheight, halfdepth);
            vertex[6] = new Vertex(halfwidth, halfheight, halfdepth);
            vertex[7] = new Vertex(halfwidth, -halfheight, halfdepth);

            for (int i = 0; i < vertex.Length; i++)
            {
                vertex[i].r = 1;
                vertex[i].g = 0;
                vertex[i].b = 0;
                vertex[i].a = 1f;
            }

            vertexBuffer = new VertexBuffer<Vertex>(vertex);
            indexBuffer = new IndexBuffer<uint>(index);
        }

        protected override void OnUpdate(object Unknown = null)
        {
            Transform.Rotate *= Quaternion.CreateFromYawPitchRoll(rotate_speed * Mico.Objects.Time.DeltaSeconds, 0, 0);
        }

        protected override void OnExport(object Unknown = null)
        {
            cubeBuffer.world = Transform;
            cubeBufferUploader.Update(ref cubeBuffer);
            

            GraphicsPipeline.InputSlot[0] = Resource.CameraBuffer;
            GraphicsPipeline.InputSlot[1] = cubeBufferUploader;

            GraphicsPipeline.InputAssemblerStage.VertexBuffer = vertexBuffer;
            GraphicsPipeline.InputAssemblerStage.IndexBuffer = indexBuffer;
            GraphicsPipeline.InputAssemblerStage.PrimitiveType = PrimitiveType.TriangleList;

            GraphicsPipeline.PutObjectIndexed(indexBuffer.Count);
        }

        public Cube(int width, int height, int depth)
        {
            Transform.Scale = new Vector3(width, height, depth);

            cubeBuffer = new CubeBuffer();
            cubeBufferUploader = new ConstantBuffer<CubeBuffer>(cubeBuffer);
        }

    }
}
