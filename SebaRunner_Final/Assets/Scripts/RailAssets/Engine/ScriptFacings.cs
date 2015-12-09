using UnityEngine;
using System.Collections;

/*
*   @author Mike Dobson
* */

[System.Serializable]
public class ScriptFacings
{
    public FacingTypes facingType;

    public string name;

    //Loot at target variables
    public GameObject[] targets = new GameObject[] { };
	public float[] rotationSpeed = new float[] { };
	public float[] lockTimes = new float[] { };

    public float facingTime;

    // int for number of elements in the look chain @author: Nathan
    public int targetSize;
}
