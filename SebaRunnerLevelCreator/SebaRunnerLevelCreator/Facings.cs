using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebaRunnerLevelCreator
{
    [Serializable]
    public class Facings
    {
        public FacingTypes facingType;

        public string name;

        public GameObject[] targets;
        public float[] rotationSpeed;
        public float[] lockTimes;

        public float facingTime;
    }
}
