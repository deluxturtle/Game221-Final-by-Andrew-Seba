﻿using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// @Author Marshall Mason
/// ScriptModSupport handles the I/O tasks of reading and outputting text files for modding.
/// </summary>
public class ScriptModSupport : MonoBehaviour
{

	public GameObject movementWaypoint;
	public GameObject facingWaypoint;
	public GameObject effectWaypoint;
	public ScriptEngine player;
    [Tooltip("Place your cate prefab that you want to see.")]
    public GameObject crate;
    Vector3 crateAdjust = new Vector3(0, -0.5f, 0);
    [Tooltip("Place your enemy prefab to spawn here.")]
    public GameObject enemy;

	string defaultModFileText = "Edit this file for modding timelines and delete this line";

	FileInfo modFile = null;
	StreamReader reader = null;

	void Awake()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptEngine>();
			if (player == null)
			{
				ScriptErrorLogging.logError("No Player Object found, please add a player to the scene and check the tag.");
				Application.Quit();
			}
		}
		if (movementWaypoint == null)
		{
			movementWaypoint = (GameObject)Resources.Load("movementWaypoint.prefab", typeof(GameObject));
			if (movementWaypoint == null)
			{
				ScriptErrorLogging.logError("No movementWaypoint prefab found, please place one in the Resources folder");
				Application.Quit();
			}
		}
		if (facingWaypoint == null)
		{
			facingWaypoint = (GameObject)Resources.Load("facingWaypoint.prefab", typeof(GameObject));
			if (facingWaypoint == null)
			{
				ScriptErrorLogging.logError("No facingWaypoint prefab found, please place one in the Resources folder");
				Application.Quit();
			}
		}
		if (effectWaypoint == null)
		{
			effectWaypoint = (GameObject)Resources.Load("effectWaypoint.prefab", typeof(GameObject));
			if (effectWaypoint == null)
			{
				ScriptErrorLogging.logError("No effectWaypoint prefab found, please place one in the Resources folder");
				Application.Quit();
			}
		}

		modFile = new FileInfo(Application.dataPath + "/waypoints.txt");
		if (!modFile.Exists)
		{

			File.WriteAllText(Application.dataPath + "/waypoints.txt", defaultModFileText);
		}
		else
		{

			reader = modFile.OpenText();
			if (reader.ReadLine() != defaultModFileText)
			{
				reader.Close();
				List<ScriptMovements> tempMovements = new List<ScriptMovements>();
				List<ScriptEffects> tempEffects = new List<ScriptEffects>();
				List<ScriptFacings> tempFacings = new List<ScriptFacings>();

				GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
				foreach (GameObject go in waypoints)
				{
					Destroy(go);
				}
				reader = modFile.OpenText();
				string inputLine = reader.ReadLine();

				while (inputLine != null)
				{
					ScriptMovements tempMove;
					string[] coords;
					Vector3 target;
					string[] keywords = inputLine.Split('_');

                    #region Object Parsing
                    if (keywords[0].ToUpper() == "O")
                    {
                        string[] words = keywords[1].Split(' ');
                        switch (words[0].ToUpper())
                        {
                            case "CRATE":
                                coords = words[1].Split(',');
                                target = new Vector3(System.Convert.ToSingle(coords[0]),
                                    System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));

                                Instantiate(crate, target + crateAdjust, Quaternion.identity);

                                break;
                            //case "PLAYER":
                            //    Node tempPlayer = new Node();
                            //    tempPlayer.isPlayer = true;
                            //    coords = words[1].Split(',');

                            //    tempPlayer.x = Convert.ToSingle(coords[0]);
                            //    tempPlayer.y = Convert.ToSingle(coords[1]);
                            //    tempPlayer.z = Convert.ToSingle(coords[2]);

                            //    AddObject(tempPlayer, false);
                            //    break;
                            case "ENEMY":
                                //tempEnemy.activationRange = (float)Convert.ToDecimal(words[1]);

                                coords = words[2].Split(',');

                                target = new Vector3(
                                    System.Convert.ToSingle(coords[0]),
                                    System.Convert.ToSingle(coords[1]),
                                    System.Convert.ToSingle(coords[2]));

                                GameObject tempEnemy = (GameObject)Instantiate(enemy, target, Quaternion.identity);
                                tempEnemy.GetComponent<ScriptEnemy>().activationRange = (float)System.Convert.ToDouble(words[1]);
                                break;
                            default:
                                break;
                        }
                    }


                    #endregion

                    #region Movement Parsing
                    if (keywords[0].ToUpper() == "M")
					{
						string[] words = keywords[1].Split(' ');
						switch ((MovementTypes)System.Enum.Parse(typeof(MovementTypes), words[0].ToUpper()))
						{
							case MovementTypes.STRAIGHT:
								tempMove = new ScriptMovements();
								tempMove.moveType = MovementTypes.STRAIGHT;
								tempMove.movementTime = (float)System.Convert.ToDouble(words[1]);
								coords = words[2].Split(',');
								target = new Vector3(System.Convert.ToSingle(coords[0]),
									System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));
								tempMove.endWaypoint = (GameObject)Instantiate(movementWaypoint, target, Quaternion.identity);
								tempMovements.Add(tempMove);
								break;
							case MovementTypes.WAIT:
								tempMove = new ScriptMovements();
								tempMove.moveType = MovementTypes.WAIT;
								tempMove.movementTime = (float)System.Convert.ToDouble(words[1]);
								tempMovements.Add(tempMove);
								break;
							case MovementTypes.BEZIER:
								tempMove = new ScriptMovements();
								tempMove.moveType = MovementTypes.BEZIER;
								tempMove.movementTime = (float)System.Convert.ToDouble(words[1]);
								coords = words[2].Split(',');
								target = new Vector3(System.Convert.ToSingle(coords[0]),
									System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));
								tempMove.endWaypoint = (GameObject)Instantiate(movementWaypoint, target, Quaternion.identity);
								coords = words[3].Split(',');
								target = new Vector3(System.Convert.ToSingle(coords[0]),
									System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));
								tempMove.curveWaypoint = (GameObject)Instantiate(movementWaypoint, target, Quaternion.identity);
								tempMovements.Add(tempMove);
								break;
						}
					}
                    #endregion
                    #region Effects Parsing
                    else if (keywords[0].ToUpper() == "E")
					{
						ScriptEffects tempEffect;

						
						string[] words = keywords[1].Split(' ');
						switch ((EffectTypes)System.Enum.Parse(typeof(EffectTypes), words[0].ToUpper()))
						{
							//@ mike @ reference Marshall
							case EffectTypes.FADE:
								//Fade waypoint spawning Code
								tempEffect = new ScriptEffects();
								tempEffect.effectType = EffectTypes.FADE;
								tempEffect.effectTime = System.Convert.ToSingle(words[1]);
								tempEffect.fadeInTime = System.Convert.ToSingle(words[2]);
								tempEffect.fadeOutTime = System.Convert.ToSingle(words[3]);
								tempEffects.Add(tempEffect);
								break;
							case EffectTypes.SHAKE:
								//Shake waypoint spawning Code
								tempEffect = new ScriptEffects();
								tempEffect.effectType = EffectTypes.SHAKE;
								tempEffect.effectTime = System.Convert.ToSingle(words[1]);
								tempEffect.magnitude = System.Convert.ToSingle(words[2]);
								tempEffects.Add(tempEffect);
								break;
							case EffectTypes.SPLATTER:
								//Splatter waypoint spawning Code
								tempEffect = new ScriptEffects();
								tempEffect.effectType = EffectTypes.SPLATTER;
								tempEffect.effectTime = System.Convert.ToSingle(words[1]);
								tempEffect.fadeInTime = System.Convert.ToSingle(words[2]);
								tempEffect.fadeOutTime = System.Convert.ToSingle(words[3]);
								tempEffect.imageScale = System.Convert.ToSingle(words[4]);
								break;
							case EffectTypes.WAIT:
								//Effect Wait waypoint spawning Code
								tempEffect = new ScriptEffects();
								tempEffect.effectType = EffectTypes.WAIT;
								tempEffect.effectTime = System.Convert.ToSingle(words[1]);
								tempEffects.Add(tempEffect);
								break;
								//end @ mike
						}
					}
                    #endregion
                    #region Facing Parcing
                    else if (keywords[0].ToUpper() == "F")
					{
						ScriptFacings tempFacing;

						string[] words = keywords[1].Split(' ');
						switch ((FacingTypes)System.Enum.Parse(typeof(FacingTypes), words[0].ToUpper()))
						{
								//@ mike @ reference Marshall
							case FacingTypes.LOOKAT:
							case FacingTypes.LOOKCHAIN:
								//Look At waypoint spawning Code
								tempFacing = new ScriptFacings();
							tempFacing.facingType = (FacingTypes)System.Enum.Parse(typeof(FacingTypes), words[0].ToUpper());
							List<float> tempRotationSpeed = new List<float>();
							List<float> tempLockTimes = new List<float>();
							List<GameObject> tempTargets = new List<GameObject>();
								for (int i = 1; i < words.Length; i++ )
								{
									if (i % 3 == 1)
									{
										
										tempRotationSpeed.Add (System.Convert.ToSingle(words[i]));
										
									}
									else if(i % 3 == 2)
									{
										tempLockTimes.Add (System.Convert.ToSingle(words[i]));
									}
									else
									{
										coords = words[i].Split(',');
										target = new Vector3(System.Convert.ToSingle(coords[0]),
											System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));
										tempTargets.Add ((GameObject)Instantiate(movementWaypoint, target, Quaternion.identity));
										
									}                                   
							}
							tempFacing.rotationSpeed = new float[tempRotationSpeed.Count];
							for (int i = 0; i < tempRotationSpeed.Count; i++)
							{
								tempFacing.rotationSpeed[i] = tempRotationSpeed[i];
							}

							tempFacing.lockTimes = new float[tempLockTimes.Count];
							for (int i = 0; i < tempLockTimes.Count; i++)
							{
								tempFacing.lockTimes[i] = tempLockTimes[i];
							}

							tempFacing.targets = new GameObject[tempTargets.Count];
							for (int i = 0; i < tempTargets.Count; i++)
							{
								tempFacing.targets[i] = tempTargets[i];
							}
								tempFacings.Add(tempFacing);
								break;
							//case FacingTypes.LOOKCHAIN:
								//Look Chain waypoint spawning Code
							    /*  tempFacing = new ScriptFacings();
								tempFacing.facingType = FacingTypes.LOOKAT;
							System.Collections.Generic.List<float> tempRotationSpeed = new System.Collections.Generic.List<float>(0);
							System.Collections.Generic.List<float> tempLockTimes = new System.Collections.Generic.List<float>(0);
							System.Collections.Generic.List<GameObject> tempTargets = new System.Collections.Generic.List<GameObject>(0);
								for (int i = 1; i < words.Length; i++ )
								{
									if (i % 3 == 1)
									{
										
										tempRotationSpeed.Add (System.Convert.ToSingle(words[i]));
										
									}
									else if(i % 3 == 2)
									{
										tempLockTimes.Add (System.Convert.ToSingle(words[i]));
									}
									else
									{
										coords = words[i].Split(',');
										target = new Vector3(System.Convert.ToSingle(coords[0]),
											System.Convert.ToSingle(coords[1]), System.Convert.ToSingle(coords[2]));
										tempTargets.Add ((GameObject)Instantiate(movementWaypoint, target, Quaternion.identity));
										
									}                                   
							}
							tempFacing.rotationSpeed = new float[tempRotationSpeed.Count];
							for (int i = 0; i < tempRotationSpeed.Count; i++)
							{
								tempFacing.rotationSpeed[i] = tempRotationSpeed[i];
							}

							tempFacing.lockTimes = new float[tempLockTimes.Count];
							for (int i = 0; i < tempLockTimes.Count; i++)
							{
								tempFacing.lockTimes[i] = tempLockTimes[i];
							}

							tempFacing.targets = new GameObject[tempTargets.Count];
							for (int i = 0; i < tempTargets.Count; i++)
							{
								tempFacing.targets[i] = tempTargets[i];
							}
								tempFacings.Add(tempFacing);
								break; */
							case FacingTypes.WAIT:
								//Facing Wait waypoint spawning Code
								tempFacing = new ScriptFacings();
								tempFacing.facingType = FacingTypes.WAIT;
								tempFacing.facingTime = System.Convert.ToSingle(words[1]);
								tempFacings.Add(tempFacing);
								break;
							case FacingTypes.FREELOOK:
								//Free look for the camera
								break;
								//end @ mike
						}
					}
                    #endregion
                    inputLine = reader.ReadLine();
				}
				player.movements = new List<ScriptMovements>();
				for (int i = 0; i < tempMovements.Count; i++)
				{
					player.movements.Add(tempMovements[i]);
				}
				player.effects = new List<ScriptEffects>();
				for (int i = 0; i < tempEffects.Count; i++)
				{
					player.effects.Add(tempEffects[i]);
				}
				player.facings = new List<ScriptFacings>();
				for (int i = 0; i < tempFacings.Count; i++)
				{
					player.facings.Add(tempFacings[i]);
					//Debug.Log ("add facing " + tempFacings[i].facingType);
				}
				//Debug.Log ("last facing " + player.facings[player.facings.Length - 1]);
				//Debug.Log ("last facing list type " + tempFacings[tempFacings.Count - 1].facingType);
			}
		}
	}
}
