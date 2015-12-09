using UnityEngine;
using System.Collections;

/// <summary>
/// @Author Marshall Mason
/// </summary>
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
    BEZIER,
    LOOKAT,
    LOOKCHAIN
};

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

public static class ScriptShooterNavEnums{

}
