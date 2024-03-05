using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class UnitMovement : CharacterProperty
{
    Coroutine move = null;
    Coroutine rotate = null;
    Coroutine follow = null;
    public Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void MoveToPos(Vector3 target, float Speed, UnityAction startAct, UnityAction endAct)
    {
        if (move != null)
        {
            StopCoroutine(move);
            move = null;
        }

        move = StartCoroutine(MovingToPos(target, Speed, startAct, endAct));
    }

    public void FollowTarget(Transform target, float Speed, UnityAction startAct, UnityAction endAct)
    {
        if (follow != null)
        {
            StopCoroutine(follow);
            follow = null;
        }

        follow = StartCoroutine(FollowingTarget(target, Speed, startAct, endAct));
    }

    public void Rotate(Vector3 dir, float speed)
    {
        if (rotate != null)
        {
            StopCoroutine(rotate);
            rotate = null;
        }
        rotate = StartCoroutine(Rotating(dir, speed));
    }
    
    public void StopMove(UnityAction endAct)
    {
        if (move != null)
        {
            StopCoroutine(move);
            move = null;
        }
        if(follow != null)
        {
            StopCoroutine(follow);
            follow = null;
        }
        
        endAct?.Invoke();
    }
    
    public void Dadge(Vector3 target, float dadge)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, 10.0f));
        rigid.AddForce(dir * dadge, ForceMode.Impulse);
    }

    IEnumerator MovingToPos(Vector3 target, float speed, UnityAction startAct, UnityAction endAct)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, speed));

        startAct?.Invoke();

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = speed * Time.deltaTime;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }

        endAct?.Invoke();
    }

    public IEnumerator FollowingTarget(Transform target, float speed, UnityAction startAct, UnityAction endAct)
    {
        while (target != null)
        {
            //�ִϸ��̼�
            startAct?.Invoke();

            Vector3 dir = target.position - transform.position;
            //0.5�� ������
            float dist = dir.magnitude - 0.5f;
            if (dist < 0.0f) dist = 0.0f;
            float delta;


            dir.Normalize();
            delta = speed * Time.deltaTime;
            if (delta > dist) delta = dist;
            transform.Translate(dir * delta, Space.World);
            if (Mathf.Approximately(dist, 0.0f))
            {
                //�ִϸ��̼�
                endAct?.Invoke();
            }

            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = speed * 90.0f * Time.deltaTime;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * rotDir * delta);

            yield return null;
        }
        endAct?.Invoke();
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
