using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Shapes;

using Presenter;

namespace HelloTexture
{
    public struct Vertex
    {
        public Vector3 position;
        public Vector2 tex;

        public Vertex(float x, float y, float z, float u, float v)
        {
            position = new Vector3(x, y, z);
            tex = new Vector2(u, v);
        }
    }

    public class Grid : Shape
    {
        private static VertexBuffer<Vertex> vertexBuffer;
        private static IndexBuffer<uint> indexBuffer;

        private static CameraBuffer cameraBuffer;

        private GridBuffer gridBuffer;


        static Grid()
        {
            Vertex[] vertices = new Vertex[4];

            vertices[0] = new Vertex(-0.5f, 0.5f, 0, 0, 0);
            vertices[1] = new Vertex(-0.5f, -0.5f, 0, 0, 1);
            vertices[2] = new Vertex(0.5f, -0.5f, 0, 1, 1);
            vertices[3] = new Vertex(0.5f, 0.5f, 0, 1, 0);

            uint[] indices = new uint[6]
            {
                0,2,1,
                0,3,2
            };

            vertexBuffer = new VertexBuffer<Vertex>(vertices);
            indexBuffer = new IndexBuffer<uint>(indices);
        }

        protected override void OnExport(object Unknown = null)
        {
            gridBuffer = GridBuffer.FromTransform(Transform);
            cameraBuffer = CameraBuffer.FromCamera(Micos.Camera);

            Resource.GridBuffer.Update(ref gridBuffer);
            Resource.CameraBuffer.Update(ref cameraBuffer);

            GraphicsPipeline.SetHeaps(new ResourceHeap[] { Resource.heap });

            GraphicsPipeline.InputSlot[0] = Resource.ConstantBufferTable;
            GraphicsPipeline.InputSlot[1] = Resource.TextureTable;


            GraphicsPipeline.InputAssemblerStage.VertexBuffer = vertexBuffer;
            GraphicsPipeline.InputAssemblerStage.IndexBuffer = indexBuffer;
            GraphicsPipeline.InputAssemblerStage.PrimitiveType = PrimitiveType.TriangleList;

            GraphicsPipeline.PutObjectIndexed(indexBuffer.Count);
        }

        public Grid(float width,float height)
        {
            Transform.Scale = new Vector3(width, height, 1);
        }

    }
}
