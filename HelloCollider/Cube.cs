using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico;
using Mico.Math;
using Mico.Shapes;
using Mico.Objects;

using Presenter;

namespace HelloCollider
{
    public class Cube : Shape
    {
        private static Buffer vertexBuffer;
        private static Buffer indexBuffer;

        private PassObject passObject = new PassObject();
        private ConstantBuffer<PassObject> passObjectView = new ConstantBuffer<PassObject>();


        private Vector3 rotate_speed = new Vector3(1, 1, 0);
        private Vector3 translation_direct = TVector3.Forward;
        private float speed = 20;

        protected override void OnUpdate(object Unknown = null)
        {
            if (Transform.Position.X >= EventThing.XLimit || Transform.Position.X <= -EventThing.XLimit)
                translation_direct.X = -translation_direct.X;
            if (Transform.Position.Y >= EventThing.YLimit || Transform.Position.Y <= -EventThing.YLimit)
                translation_direct.Y = -translation_direct.Y;
            if (Transform.Position.Z >= EventThing.ZLimit || Transform.Position.Z <= -EventThing.ZLimit)
                translation_direct.Z = -translation_direct.Z;

            //Update
            Transform.Rotate *= Quaternion.CreateFromYawPitchRoll(rotate_speed.Y * Time.DeltaSeconds,
                rotate_speed.X * Time.DeltaSeconds, rotate_speed.Z * Time.DeltaSeconds);

            Transform.Position += Vector3.Normalize(translation_direct) * speed * Time.DeltaSeconds;

            Collider.Center = Transform.Position;
            (Collider as BoxCollider).Rotate = Transform.Rotate;

            base.OnUpdate(Unknown);
        }

        protected override void OnCollide(Shape target)
        {
            translation_direct = Vector3.Normalize(Transform.Position - target.Transform.Position);
            passObject.color = new TVector4(EventThing.FLOAT, EventThing.FLOAT, EventThing.FLOAT, 1);
            base.OnCollide(target);
        }

        protected override void OnExport(object Unknown = null)
        {
            passObject.world = Transform;

            passObjectView.Update(ref passObject);

            GraphicsPipeline.InputSlot[0] = CameraBuffer.View;
            GraphicsPipeline.InputSlot[1] = passObjectView;

            GraphicsPipeline.InputAssemblerStage.VertexBuffer = vertexBuffer;
            GraphicsPipeline.InputAssemblerStage.IndexBuffer = indexBuffer;
            GraphicsPipeline.InputAssemblerStage.PrimitiveType = PrimitiveType.TriangleList;

            GraphicsPipeline.PutObjectIndexed(indexBuffer.Count);

            base.OnExport(Unknown);
        }

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

            vertexBuffer = new VertexBuffer<Vertex>(vertex);
            indexBuffer = new IndexBuffer<uint>(index);
        }

        public Cube(float width, float height, float depth)
        {
            Transform.Scale = new Vector3(width, height, depth);

            Collider = new BoxCollider(new Vector3(0, 0, 0), new Vector3(width / 2.0f, height / 2.0f, depth / 2.0f))
            {
                IsPicked = true
            };

            passObject.world = Matrix4x4.Identity;
            passObject.color = new Vector4(EventThing.FLOAT, EventThing.FLOAT, EventThing.FLOAT, EventThing.FLOAT);
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector3 RotateSpeed
        {
            get => rotate_speed;
            set => rotate_speed = value;
        }

        public Vector3 Forward
        {
            get => translation_direct;
            set => translation_direct = value;
        }

        public TVector4 Color
        {
            get => passObject.color;
            set => passObject.color = value;
        }

    }
}
