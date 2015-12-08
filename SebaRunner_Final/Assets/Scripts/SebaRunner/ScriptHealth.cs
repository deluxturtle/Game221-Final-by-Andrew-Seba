using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptHealth : MonoBehaviour
{
    public float health = 100;
    Text healthText;
    // Use this for initialization
    void Start()
    {
        healthText = GameObject.Find("Text_Health").GetComponent<Text>();
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _ChangeHealth(-10);
        }
#endif
    }

    public void _ChangeHealth(float amount)
    {
        health += amount;
        if(health < 0)
        {
            health = 0;
        }
        healthText.text = health.ToString();
    }

}
