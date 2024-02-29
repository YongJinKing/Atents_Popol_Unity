using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class NavPlayer : CharacterProperty
{
    public NavMeshAgent myAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!myAgent.pathPending && myAgent.remainingDistance <= 0.25f)
        {
            myAnim.SetBool("run", false);
        }
    }

    public void MoveToPos(Vector3 pos, float Speed, UnityAction startAct, UnityAction endAct)
    {
        myAnim.SetBool("run", true);
        myAgent.SetDestination(pos);
        
    }
}
