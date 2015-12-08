using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebaRunnerLevelCreator
{
    //From Marshall Mason
    public enum WaypointType
    {
        Movement,
        Facing,
        Effect
    }

    //@Mike
    public enum MovementTypes
    {
        WAIT,
        STRAIGHT,
        BEZIER
    }

    public enum FacingTypes
    {
        LOOKAT,
        LOOKCHAIN,
        WAIT,
        FREELOOK,
        LOOKANDRETURN
    }

    public enum EffectTypes
    {
        SHAKE,
        SPLATTER,
        FADE,
        WAIT
    }

    public class Node : GameObject
    {

        public bool isPlayer = false;

        public Node()
        {
            name = "Node";
        }

        public override string ToString()
        {
            return name;
        }
    }
}
