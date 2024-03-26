using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrachCode : MonoBehaviour
{
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        //particle = GetComponent<ParticleSystem>();
        //particle.Play();
        //particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var emission = particle.emission;
            emission.rateOverTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var emission = particle.emission;
            emission.rateOverTime = 30f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var emission = particle.emission;
            emission.rateOverTime = 60f;
        }
    }
}
