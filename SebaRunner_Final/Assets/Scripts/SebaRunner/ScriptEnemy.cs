using UnityEngine;
using System.Collections;

public class ScriptEnemy : MonoBehaviour {

    public float health = 1;
    public float attackDamage = 1;

    /// <summary>
    /// Setting only adds to health;
    /// </summary>
    public float Health
    {
        set
        {
            health += value;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get
        {
            return health;
        }
    }

    public float AttackDamage
    {
        get
        {
            return attackDamage;
        }

        set
        {
            attackDamage = value;
        }
    }
}
