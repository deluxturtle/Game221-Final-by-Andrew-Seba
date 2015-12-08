using UnityEngine;
using System.Collections;

public enum GunType
{
    PISTOL,
    SMG,
    RIFLE,
    SNIPER,
    SPECIAL
}

/// <summary>
/// @Author: Andrew Seba
/// @Description: Holds the properties of the gun.
/// </summary>
public class ScriptGun : MonoBehaviour{

    public string gunName = "default";
    [Tooltip("The type of gun (controls ammotype)")]
    public GunType type = GunType.PISTOL;
    [Tooltip("The amount of damage being fired.")]
    public float damage = 1;
    [Tooltip("The amount of bullets in a full clip")]
    public int clipSize = 17;
    [Tooltip("The rate at the gun can fire in seconds.")]
    public float fireRate = 0.5f;
    [Tooltip("If you can hold down the firebutton or not.")]
    public bool automatic = false;
    public AudioClip gunShot;

    public ScriptGun(){}

    public ScriptGun(string pName, GunType pType, float pDamage, int pClipSize,
        float pFireRate, bool pAutomatic)
    {
        gunName = pName;
        type = pType;
        damage = pDamage;
        clipSize = pClipSize;
        fireRate = pFireRate;
        automatic = pAutomatic;
    }

    public override string ToString()
    {
        return string.Format(
            "{0}\n" +
            "{1}\n"+
            "{2}\n" +
            "{3}\n" +
            "{4}\n" +
            "{5}",
            gunName,
            type,
            damage,
            clipSize,
            fireRate,
            automatic
            );
    }
}
