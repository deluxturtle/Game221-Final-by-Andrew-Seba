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
    public GameObject gunFlarePos;

    public AttackType attackType = AttackType.MoveTowardsPlayer;

    bool canDamage = false;
    State state = State.IDLE;
    State previousState = State.IDLE;

    bool activated = false;

    GameObject player1;
    Camera player1Cam;
    GameObject player2;
    Camera player2Cam;
    bool visibleFromP1 = false;
    bool visibleFromP2 = false;
    float player1Distance;
    float player2Distance;

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
        player1Cam = player1.GetComponentInChildren<Camera>();
        player2 = GameObject.Find("Player2");
        player2Cam = player2.GetComponentInChildren<Camera>();
        StartCoroutine("CheckDistance");
    }

    void Update()
    {
        visibleFromP1 = GetComponent<Renderer>().IsVisibleFrom(player1Cam);
        visibleFromP2 = GetComponent<Renderer>().IsVisibleFrom(player2Cam);
        switch (state)
        {
            case State.IDLE:
                if (previousState != state)
                {
                    Debug.Log("Idle");
                    StopCoroutine("LookAtTarget");
                    CancelInvoke("Attack");
                    previousState = state;
                }
                break;
            case State.ATTACK:
                if (previousState != state)
                {
                    Debug.Log("Attack");
                    StartCoroutine("LookAtTarget");
                    previousState = state;
                }
                break;
        }
    }

    void Attack()
    {
        GameObject tempGunFlare = (GameObject)Instantiate(gunFlare);
        tempGunFlare.transform.parent = transform;
        tempGunFlare.transform.position = gunFlarePos.transform.position;
        
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
            yield return null;
        }
    }

    IEnumerator CheckDistance()
    {

            while (!activated)//Try to be activated
            {
                player1Distance = Vector3.Distance(transform.position, player1.transform.position);
                player2Distance = Vector3.Distance(transform.position, player2.transform.position);

                if (player1Distance < activationRange || player2Distance < activationRange)
                {
                    if (player1Distance < player2Distance)
                    {
                        currentTarget = player1;
                    }
                    else
                    {
                        currentTarget = player2;
                    }
                    activated = true;
                    state = State.ATTACK;
                }
                yield return null;
            }
            if (attackType == AttackType.MoveTowardsPlayer)
                StartCoroutine("MoveTowardsPlayer");
            else
                DuckAndCover();

        while (true)//Once Activated Always Activated
        {
            while (activated)//Are they looking at me? if not go back to idle
            {
                float player1Distance = Vector3.Distance(transform.position, player1.transform.position);
                float player2Distance = Vector3.Distance(transform.position, player2.transform.position);
                if (!visibleFromP1 && !visibleFromP2)
                {
                    activated = false;
                    state = State.IDLE;
                }
                yield return null;
            }


            //If visible from either player re activate!
            if (visibleFromP1 || visibleFromP2)
            {
                if (player1Distance < player2Distance)
                {
                    currentTarget = player1;
                }
                else
                {
                    currentTarget = player2;
                }
                activated = true;
                state = State.ATTACK;
                //Restart Attack or move
                if (attackType == AttackType.MoveTowardsPlayer)
                    StartCoroutine("MoveTowardsPlayer");
                else
                    DuckAndCover();
            }
            yield return null;
        }
    }

    //Move the enemy towards player
    IEnumerator MoveTowardsPlayer()
    {
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
        InvokeRepeating("Attack", 0, reloadSpeed);
    }
}
