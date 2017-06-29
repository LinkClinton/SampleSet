using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Presenter;

namespace HelloBlend
{
    public struct CameraBuffer
    {
        public Matrix4x4 view;
        public Matrix4x4 proj;

        public static CameraBuffer FromCamera(Mico.Objects.Camera camera)
        {
            return new CameraBuffer()
            {
                view = camera,
                proj = camera.Project
            };
        }
    }

    public struct CubeBuffer
    {
        public Matrix4x4 world;

        public static CubeBuffer FromTransform(Mico.Shapes.Transform transform)
        {
            return new CubeBuffer()
            {
                world = transform
            };
        }
    }

    public struct Vertex
    {
        public float x, y, z;
        public float r, g, b, a;

        public Vertex(float xpos, float ypos, float zpos)
        {
            x = xpos; y = ypos; z = zpos;

            r = 0; g = 0; b = 0; a = 0;
        }
    }

    public static class Resource
    {
        public static ConstantBuffer<CameraBuffer> CameraBuffer;

        public static void Initalize()
        {
            CameraBuffer = new ConstantBuffer<CameraBuffer>(1);
        }
    }
}
