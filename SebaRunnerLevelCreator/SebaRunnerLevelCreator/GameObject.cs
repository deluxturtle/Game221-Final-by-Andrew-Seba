using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SebaRunnerLevelCreator
{


    /// <summary>
    /// @Author: Andrew Seba
    /// @Description: Base Class for all objects.
    /// Holds Position.
    /// </summary>
    public class GameObject
    {
        //World Position
        public float x = 0;
        public float y = 0;
        public float z = 0;

        public float width = 1;
        public float height = 1;

        public string name = "object";
        public Brush fillColor = Brushes.LightYellow;
        public Pen outlineColor = Pens.WhiteSmoke;

        public RectangleF bounds;

        public GameObject()
        {
            bounds = new RectangleF(new PointF(x, z), new SizeF(width, height));
        }

        public GameObject(float pX, float pY, float pZ)
        {
            x = pX;
            y = pY;
            z = pZ;
        }
        
        public GameObject(string pName, float pX, float pY, float pZ, float pWidth, float pHeight)
        {
            name = pName;
            x = pX;
            y = pY;
            z = pZ;
            width = pWidth;
            height = pHeight;

            bounds = new RectangleF(new PointF(x, z), new SizeF(width, height));

        }

        public void UpdatePosition(float pX, float pY, float pZ)
        {
            x = pX;
            y = pY;
            z = pZ;
            bounds = new RectangleF(new PointF(x, z), new SizeF(width, height));
        }

        public static GameObject operator *(GameObject go1, GameObject go2)
        {
            return new GameObject((go1.x * go2.x), (go1.y * go2.y), (go1.z * go2.z));
        }

        public static GameObject operator *(float pValue, GameObject go2)
        {
            return new GameObject((pValue * go2.x), (pValue * go2.y), (pValue * go2.z));
        }

        public static GameObject operator +(GameObject go1, GameObject go2)
        {
            return new GameObject((go1.x + go2.x), (go1.y + go2.y), (go1.z + go2.z));
        }
    }
}
