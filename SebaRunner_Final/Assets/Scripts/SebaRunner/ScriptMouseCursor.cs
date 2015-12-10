using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Shows the cursor on screen.
/// </summary>
public class ScriptMouseCursor : MonoBehaviour {

    [Tooltip("Place your cursor graphic here.")]
    public Sprite cursorGaphic;
    [Tooltip("Place the canvas object here.")]
    public GameObject cursorObject;

    ScriptGameController gameController;
    ScriptGun curGun;

    private bool canFire = true;
    private float coolDownTime = 0;

    void Start()
    {

        try
        {
            curGun = GetComponent<ScriptPlayerSetup>().myGun;
            curGun.gunShot = GetComponent<ScriptPlayerSetup>().gunShotSound;
        }
        catch
        {
            Debug.Log("LoadSceneFrom Main Menu!!");
        }

        if (curGun == null)
        {
            Debug.Log("No gun on the player!");
        }
        Cursor.visible = false;
        if(cursorObject == null)
        {
            cursorObject = GameObject.Find("Cursor");
        }
        cursorObject.GetComponent<Image>().sprite = cursorGaphic;
        
        if((gameController = GameObject.Find("GameController").GetComponent<ScriptGameController>()) == null)
        {
            Debug.Log("No game controller found in the scene please add one" +
                " from the prefabs folder.");
        }
    }

	// Update is called once per frame
	void Update () {
        cursorObject.transform.position = Input.mousePosition;
        if(curGun != null)
        {
            if (!curGun.automatic && Input.GetButtonDown("Fire1") && canFire)
            {
                canFire = false;
                coolDownTime = curGun.fireRate;

                ShootGun();

                StartCoroutine("ResetTrigger");
            }
            if (curGun.automatic && Input.GetButton("Fire1") && canFire)
            {
                canFire = false;
                coolDownTime = curGun.fireRate;

                ShootGun();

                StartCoroutine("ResetTrigger");
            }
        }
    }

    IEnumerator ResetTrigger()
    {

        while (coolDownTime > 0)
        {
            coolDownTime -= Time.deltaTime;
            yield return null;
        }
        canFire = true;
    }

    void ShootGun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                Instantiate(gameController.sparks, hit.point, Quaternion.identity);
                hit.transform.GetComponent<ScriptEnemy>().Health = -curGun.damage;
            }
        }
        AudioSource.PlayClipAtPoint(curGun.gunShot, transform.position);
    }
}
