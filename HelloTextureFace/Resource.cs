﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Presenter;

namespace HelloTextureFace
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

    public struct GridBuffer
    {
        public Matrix4x4 world;

        public static GridBuffer FromTransform(Mico.Shapes.Transform transform)
        {
            return new GridBuffer()
            {
                world = transform
            };
        }
    }


    public static class Resource
    {
        public static ResourceHeap heap1;
        
        public static ConstantBuffer<CameraBuffer> CameraBuffer;
        public static ConstantBuffer<GridBuffer> GridBuffer;

        public static Texture2D Texture;
        
        public static ResourceTable ConstantBufferTable;
        public static ResourceTable TextureTable;
        public static ResourceTable TextureFaceTable;

        public static void Initalize()
        {
            heap1 = new ResourceHeap(4);
            
            CameraBuffer = new ConstantBuffer<CameraBuffer>(1);
            GridBuffer = new ConstantBuffer<GridBuffer>(1);

            Texture = Texture2D.FromFile(@"..\..\Resources\Dream.png");

            heap1.AddResource(CameraBuffer);
            heap1.AddResource(GridBuffer);
            heap1.AddResource(Texture);

            ConstantBufferTable = new ResourceTable(heap1, 0);
            TextureTable = new ResourceTable(heap1, 2);
        }
    }
}
