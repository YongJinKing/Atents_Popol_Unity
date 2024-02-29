using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitMovement : MonoBehaviour
{
    Coroutine move = null;
    Coroutine rotate = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPos(Vector3 target, float Speed, UnityAction startAct, UnityAction endAct)
    {
        if(move != null)
        {
            StopCoroutine(move);
            move = null;
        }
        
        move = StartCoroutine(MovingToPos(target, Speed, startAct, endAct));
    }

    IEnumerator MovingToPos(Vector3 target, float Speed, UnityAction startAct, UnityAction endAct)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        if(rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, Speed));

        //myAnim.SetBool("run", true);
        startAct?.Invoke();

        while(!Mathf.Approximately(dist, 0.0f))
        {
            float delta = Speed * Time.deltaTime;
            if(delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        //myAnim.SetBool("run", false);
        endAct?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir,float Speed)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;

        if(Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while(!Mathf.Approximately(angle, 0.0f))
        {
            float delta = 360.0f * Speed * Time.deltaTime;

            if(delta > angle)
            {
                delta = angle;
            }

            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;

        }                       
    }
}
