﻿using UnityEngine;
using System.Collections;

public class ScriptParticleAutoDestroy : MonoBehaviour
{

    ParticleSystem pSystem;

    // Use this for initialization
    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pSystem != null)
        {
            if (!pSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}

