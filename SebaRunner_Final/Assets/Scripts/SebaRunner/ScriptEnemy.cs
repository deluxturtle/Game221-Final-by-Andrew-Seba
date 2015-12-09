using UnityEngine;
using System.Collections;

enum AttackType
{
    MoveTowardsPlayer,
    DuckAndCover
}

public class ScriptEnemy : MonoBehaviour {

    [Tooltip("Range at wich the enemy will start trying to attack the player.")]
    public float activationRange = 5;
    public float health = 1;
    public float attackDamage = 1;
    public float moveSpeed = 0.4f;

    AttackType attackType = AttackType.MoveTowardsPlayer;

    GameObject player;

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

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine("WaitForActivation");
    }

    IEnumerator WaitForActivation()
    {
        bool activated = false;
        while (!activated)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < activationRange)
            {
                activated = true;
            }
            yield return null;
        }
        if (attackType == AttackType.MoveTowardsPlayer)
            StartCoroutine("MoveTowardsPlayer");
        else
            StartCoroutine("DuckAndCover");
    }

    IEnumerator MoveTowardsPlayer()
    {
        while (true)
        {
            transform.Translate((player.transform.position - transform.position) * moveSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed);
            yield return null;
        }
    }

    IEnumerator DuckAndCover()
    {
        //TODO
        StopCoroutine("DuckAndCover");
        yield return null;
    }
}
