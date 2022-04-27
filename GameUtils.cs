using System;
using System.Collections;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace GameUtils
{
    public static class Utils
    {
        //Extension Methods START
        public static float SqrMagnitude(this Vector2f input)
        {
            float x = input.X;
            float y = input.Y;

            float sum = x*x + y*y;

            return MathF.Sqrt(sum);
        }

        public static float ToDegrees(this float val)
        {
            float PI = (float)Math.PI;
            float degrees = val * (180 / PI);
            return degrees;
        }

        public static float ToRadians(this float val)
        {
            float PI = (float)Math.PI;
            float radians = val * (PI / 180);
            return radians;
        }

        public static Vector2f Normalize(this Vector2f source)
        {
            Vector2f normalizedVector;
            float x = source.X;
            float y = source.Y;

            float length = SqrMagnitude(source);

            if (length != 0)
            {
                normalizedVector.X = x/length;
                normalizedVector.Y = y/length;
            }
            else
            {
                normalizedVector = source;
            }

            return normalizedVector;
        }

        public static float Lerp(this float firstFloat, float secondFloat, float t, bool clamped = true)
        {
            if (clamped == true)
            {
                if (t > 1)
                {
                    t = 1;
                }
                else if (t < 0)
                {
                    t = 0;
                }
            }
            float lerpFloat = firstFloat + (secondFloat - firstFloat) * t;

            return t;
        }

        //Extension Methods END
        
        //Static Methods START
        public static Vector2f RotateVector(Vector2f v, float angle)
        {
            float x = v.X;
            float y = v.Y;
            float angleRadians = ToRadians(angle);

            v.X = x * MathF.Cos(angleRadians) - y * MathF.Sin(angleRadians);
            v.Y = x * MathF.Cos(angleRadians) + y * MathF.Sin(angleRadians);

            return v;
        }

        public static float AngleBetween(Vector2f v1, Vector2f v2)
        {
            float x = v1.X - v2.X;
            float y = v1.Y - v2.Y;

            float rotation = (MathF.Atan2(y, x)).ToDegrees() + 180;

            return rotation;
        }

        public static float Distance(Vector2f a, Vector2f b)
        {
            float k = (a.X - b.X) * (a.X - b.X);  
            float j = (a.Y - b.Y) * (a.Y - b.Y);
            float distance = k + j;

            return MathF.Sqrt(distance);
        }

        public static Vector2f Lerp(Vector2f firstVector, Vector2f secondVector, float t)
        {
            float x = Lerp(firstVector.X, firstVector.X, t);
            float y = Lerp(secondVector.Y, secondVector.Y, t);
            Vector2f lerpedVector = new Vector2f (x, y);

            return lerpedVector;
        }

        public static float Dot(Vector2f lhs, Vector2f rhs)
        {
            float x = lhs.X * rhs.X;  
            float y = lhs.Y * rhs.Y;
            float dotProduct = x + y;

            return MathF.Abs(dotProduct);
        }
        
        //Static Methods END

        //Personal Methods START

        public static int Index(int index, int arrayLength)
        {
            return index % arrayLength;
        }

        //Personal Methods END
    }
}