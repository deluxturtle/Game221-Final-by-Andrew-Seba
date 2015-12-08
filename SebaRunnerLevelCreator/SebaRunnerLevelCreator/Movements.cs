using System;
using System.Drawing;

namespace SebaRunnerLevelCreator
{
    [Serializable]
    public class Movements
    {
        //the amount of time that the player will take to complete this waypoint
        public decimal movementTime;

        public MovementTypes moveType;

        public string name;

        public GameObject endWaypoint;

        public GameObject curveWaypoint;

        public RectangleF bounds;
    }
}
