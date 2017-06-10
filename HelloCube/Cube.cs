using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Shapes;
using Presenter;

namespace HelloCube
{
    public class Cube : Shape
    {
        private struct Vertex
        {
            public Vector3 position;
            public Vector4 color;

            public Vertex(float x,float y,float z)
            {
                position = new Vector3(x, y, z);
                color = Vector4.Zero;
            }
        }

        private struct CubeBuffer
        {
            public Matrix4x4 world;
        }

        private struct CameraBuffer
        {
            public Matrix4x4 view;
            public Matrix4x4 proj;
        }

        private static VertexBuffer<Vertex> vertexBuffer;
        private static IndexBuffer<uint> indexBuffer;
        private static float rotate_speed = 2.0f;

        private static CameraBuffer cameraBuffer;
        private static ConstantBuffer<CameraBuffer> cameraBufferUploader;

        private static Random random = new Random();
        private static float NextFloat => (float)random.NextDouble();

        private CubeBuffer cubeBuffer;
        private ConstantBuffer<CubeBuffer> cubeBufferUploader;

        static Cube()
        {
            cameraBuffer = new CameraBuffer();
            cameraBufferUploader = new ConstantBuffer<CameraBuffer>(cameraBuffer);

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
                vertex[i].color = new Vector4(NextFloat, NextFloat, NextFloat, NextFloat);
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

            cameraBuffer.view = Micos.Camera;
            cameraBuffer.proj = Micos.Camera.Project;
            cameraBufferUploader.Update(ref cameraBuffer);

            ResourceLayout.InputSlot[0] = cameraBufferUploader;
            ResourceLayout.InputSlot[1] = cubeBufferUploader;

            Manager.VertexBuffer = vertexBuffer;
            Manager.IndexBuffer = indexBuffer;

            Manager.DrawObjectIndexed(indexBuffer.Count);
        }

        public Cube(int width,int height,int depth)
        {
            Transform.Scale = new Vector3(width, height, depth);

            cubeBuffer = new CubeBuffer();
            cubeBufferUploader = new ConstantBuffer<CubeBuffer>(cubeBuffer);
        }

    }
}
