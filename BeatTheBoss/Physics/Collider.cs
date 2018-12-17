using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Physics
{
    class Collider
    {
        public static void CheckForCollision(Collider object1, Collider object2)
        {
            if(object1 is BoxCollider)
            {
                if (object2 is BoxCollider)
                    CheckBoxBoxCollider((BoxCollider)object1, (BoxCollider)object2);
                else if (object2 is PollygonCollider)
                    CheckBoxPollyCollider((BoxCollider)object1, (PollygonCollider)object2);
            }
            else if (object1 is PollygonCollider)
            {
                if (object2 is PollygonCollider)
                    CheckPollyPollyCollider((PollygonCollider)object1, (PollygonCollider)object2);
                else if (object2 is BoxCollider)
                    CheckPollyBoxCollider((PollygonCollider)object1, (BoxCollider)object2);
            }
        }

        public static void CheckBoxBoxCollider(BoxCollider object1, BoxCollider object2)
        {
            if (object1.area.Intersects(object2.area))
                object1.ApplyCollision(object2);
        }

        public static void CheckPollyPollyCollider(PollygonCollider object1, PollygonCollider object2)
        {
            if (IsPollygonsIntersecting(object1, object2))
                object1.ApplyCollision(object2);
        }

        public static void CheckBoxPollyCollider(BoxCollider object1, PollygonCollider object2)
        {
            Vector2[] points = new Vector2[] { new Vector2(object1.area.X, object1.area.Y), new Vector2(object1.area.X + object1.area.Width, object1.area.Y), new Vector2(object1.area.X + object1.area.Width, object1.area.Y + object1.area.Height), new Vector2(object1.area.X, object1.area.Y + object1.area.Height) };
            PollygonCollider object1Polly = new Models.PollyColliderObject(points, new Vector2(object1.area.X, object1.area.Y), null, Vector2.One);

            if (IsPollygonsIntersecting(object1Polly, object2))
                object1.ApplyCollision(object2);
        }

        public static void CheckPollyBoxCollider(PollygonCollider object1, BoxCollider object2)
        {
            Vector2[] points = new Vector2[] { new Vector2(object2.area.X, object2.area.Y), new Vector2(object2.area.X + object2.area.Width, object2.area.Y), new Vector2(object2.area.X + object2.area.Width, object2.area.Y + object2.area.Height), new Vector2(object2.area.X, object2.area.Y + object2.area.Height) };
            PollygonCollider object2Polly = new Models.PollyColliderObject(points, new Vector2(0,0), null, Vector2.One);

            if (IsPollygonsIntersecting(object1, object2Polly))
                object1.ApplyCollision(object2);
        }

        private static bool IsPollygonsIntersecting(PollygonCollider object1, PollygonCollider object2)
        {
            foreach (PollygonCollider collider in new PollygonCollider[] { object1, object2 })
            {
                for (int i1 = 0; i1 < collider.Points.Length; i1++)
                {
                    int i2 = (i1 + 1) % collider.Points.Length;
                    Vector2 p1 = collider.GetActualPointPosition(i1);
                    Vector2 p2 = collider.GetActualPointPosition(i2);

                    Vector2 normal = new Vector2(p2.Y - p1.Y, p1.X - p2.X);

                    double minA = double.MaxValue, maxA = double.MinValue;
                    for (int index = 0; index < object1.Points.Length; index++)
                    {
                        var projected = normal.X * object1.GetActualPointPosition(index).X + normal.Y * object1.GetActualPointPosition(index).Y;
                        if (projected < minA)
                            minA = projected;
                        if (projected > maxA)
                            maxA = projected;
                    }

                    double minB = double.MaxValue, maxB = double.MinValue;
                    for (int index = 0; index < object2.Points.Length; index++)
                    {
                        var projected = normal.X * object2.GetActualPointPosition(index).X + normal.Y * object2.GetActualPointPosition(index).Y;
                        if (projected < minB)
                            minB = projected;
                        if (projected > maxB)
                            maxB = projected;
                    }

                    if (maxA < minB || maxB < minA)
                        return false;
                }
            }
            return true;
        }
    }
}
