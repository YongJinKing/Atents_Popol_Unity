using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitMovement : CharacterProperty
{
    Coroutine move = null;
    Coroutine rotate = null;
    Player player;
    protected virtual void Awake()
    {
        player = GetComponent<Player>();
    }


    void Update()
    {
        
    }

    public void MoveToPos(Vector3 target, float Speed)
    {
        if(move != null)
        {
            StopCoroutine(move);
            move = null;
        }
        
        move = StartCoroutine(MovingToPos(target, Speed));
    }

    IEnumerator MovingToPos(Vector3 target, float Speed)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        if(rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, Speed));

        myAnim.SetBool("run", true);
        
        while(!Mathf.Approximately(dist, 0.0f))
        {
            float delta = Speed * Time.deltaTime;
            if(delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            if(player.playerstate == Player.state.Fire || player.playerstate == Player.state.Dadge)
            {
                myAnim.SetBool("run", false);
                yield break;
            }
            yield return null;
        }

        myAnim.SetBool("run", false);
        player.playerstate = Player.state.Idle;
    }

    public IEnumerator Rotating(Vector3 dir,float Speed)
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
