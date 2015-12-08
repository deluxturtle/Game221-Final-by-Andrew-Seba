using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebaRunnerLevelCreator
{
    class Enemy : GameObject
    {
        public float activationRange = 5.0f;
        public int health = 10;

        public Enemy()
        {
            name = "Enemy";
        }
    }
}
