using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebaRunnerLevelCreator
{
    [Serializable]
    public class Effects
    {
        public EffectTypes effectType;
        public float effectTime;

        public string name;

        public float fadInTime;
        public float fadeOutTime;

        public float imageScale;

        public float magnitude;
        public bool willFade;

    }
}
