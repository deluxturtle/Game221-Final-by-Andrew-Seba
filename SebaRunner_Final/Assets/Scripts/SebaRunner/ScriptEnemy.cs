using UnityEngine;
using System.Collections;

public enum AttackType
{
    MoveTowardsPlayer,
    DuckAndCover
}

enum State
{
    IDLE,
    ATTACK,
}

public class ScriptEnemy : MonoBehaviour {

    [Tooltip("Range at wich the enemy will start trying to attack the player.")]
    public float activationRange = 5;
    public float health = 1;
    public float attackDamage = 1;
    public float reloadSpeed = 3.0f;
    public float moveSpeed = 0.4f;

    [Tooltip("Gun Flare Effect Object")]
    public GameObject gunFlare;

    public AttackType attackType = AttackType.MoveTowardsPlayer;

    bool canDamage = false;
    State state = State.IDLE;
    State previousState = State.IDLE;

    bool activated = false;

    GameObject player1;
    GameObject player2;

    GameObject currentTarget;

    GameObject curentGunFlare;
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
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player2");
        StartCoroutine("WaitForActivation");
    }

    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                break;
            case State.ATTACK:
                if (previousState != state)
                {
                    StartCoroutine("LookAtTarget");
                    state = previousState;
                }
                break;
        }
    }

    void Attack()
    {
        GameObject tempGunFlare = (GameObject)Instantiate(gunFlare);
        tempGunFlare.transform.parent = transform;
        tempGunFlare.transform.position = new Vector3(transform.position.x -0.095f, transform.position.y + 0.2f, transform.position.z- 1.274f);
        curentGunFlare = tempGunFlare;
        Invoke("KillFlare", 0.1f);

        player1.GetComponent<ScriptHealth>()._ChangeHealth(attackDamage);
    }

    void KillFlare()
    {
        Destroy(curentGunFlare);
    }

    IEnumerator LookAtTarget()
    {
        while (true)
        {
            transform.LookAt(currentTarget.transform, Vector3.up);
        }
    }

    IEnumerator WaitForActivation()
    { 
        while (!activated) 
        {
            if(Vector3.Distance(transform.position, player1.transform.position) < activationRange)
            {
                currentTarget = player1;
                activated = true;
                state = State.ATTACK;
            }
            else if(Vector3.Distance(transform.position, player2.transform.position) < activationRange)
            {
                currentTarget = player2;
                activated = true;
                state = State.ATTACK;
            }
            yield return null;
        }
        if (attackType == AttackType.MoveTowardsPlayer)
            StartCoroutine("MoveTowardsPlayer");
        else
            DuckAndCover();
    }

    //Move the enemy towards player
    IEnumerator MoveTowardsPlayer()
    {
        StopCoroutine("WaitForActivation");
        while (true)
        {
            transform.Translate((currentTarget.transform.position - transform.position) * moveSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed);
            yield return null;
        }
    }

    void DuckAndCover()
    {
        //TODO
        StopCoroutine("WaitForActivation");
        InvokeRepeating("Attack", 0, reloadSpeed);
    }
}
