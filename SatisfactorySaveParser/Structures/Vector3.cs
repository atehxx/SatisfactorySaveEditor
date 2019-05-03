using System;

namespace SatisfactorySaveParser.Structures
{
    [Serializable]
    public class Vector3
    {
        public Vector3() { }

        public Vector3(Vector3 other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Z: {Z}";
        }

        public Vector3 Set(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            return this;
        }

        public Vector3 AddToX(float diff)
        {
            X += diff;
            return this;
        }
        public Vector3 AddToY(float diff)
        {
            Y += diff;
            return this;
        }
        public Vector3 AddToZ(float diff)
        {
            Z += diff;
            return this;
        }

        public bool IsCloseTo(Vector3 other, float maxDistance, bool checkZ = true)
        {
            return (X >= other.X - maxDistance)
                && (X <= other.X + maxDistance)
                && (Y >= other.Y - maxDistance)
                && (Y <= other.Y + maxDistance)
                && (checkZ ? Z >= other.Z - maxDistance : true)
                && (checkZ ? Z <= other.Z + maxDistance : true);
        }
    }
}
