using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
{
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = particle.emission;
        emission.rateOverTime = 10f;
    }
}
