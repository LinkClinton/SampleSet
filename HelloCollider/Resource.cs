using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Presenter;

namespace HelloCollider
{
    public struct CameraBuffer
    {
        public Matrix4x4 view;
        public Matrix4x4 proj;
        public Vector4 eyePos;

        public static ConstantBuffer<CameraBuffer> View = new ConstantBuffer<CameraBuffer>();
    }

    public struct Vertex
    {
        public float x, y, z;
        public float r, g, b, a;

        public Vertex(float _x = 0, float _y = 0, float _z = 0)
        {
            x = _x;
            y = _y;
            z = _z;

            r = 0;
            g = 0;
            b = 0;
            a = 1;
        }
    }

    public struct PassObject
    {
        public Matrix4x4 world;
        public Vector4 color;
    }

    public static class Resource
    {
        public static CameraBuffer cameraBuffer;

        static Resource()
        {
            cameraBuffer = new CameraBuffer();
        }
    }
}
