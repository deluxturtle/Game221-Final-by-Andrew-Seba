﻿using UnityEngine;
using System.Collections;

/*
 * @author Mike Dobson
 * */

[System.Serializable]
public class ScriptMovements
{

    [Tooltip("The amount of time that the player will take to complete this waypoint")]
    public float movementTime;

    [Tooltip("The type of movement that this waypoint will use")]
    public MovementTypes moveType;

    public string name;

    [Tooltip("The target for this movement")]
    public GameObject endWaypoint;

    [Tooltip("The curve for the bezier curve")]
    public GameObject curveWaypoint;

    [Tooltip("Determine if the window is folded out")]
    public bool showInEditor = true;

    // Added by Nathan
    public bool dataFoldout;

    //Look Variables
    public GameObject[] targets = new GameObject[] { };
    public float[] rotationSpeed = new float[] { };
    public float[] lockTimes = new float[] { };
    public int targetSize;
}
