using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SebaRunnerLevelCreator
{
    class Crate : GameObject
    {

        public Crate()
        {
            name = "crate";
        }

        public Crate(string name, float x, float y, float z, float width, float length):base(name, x,y,z,width,length)
        {
            
        }

    }
}
