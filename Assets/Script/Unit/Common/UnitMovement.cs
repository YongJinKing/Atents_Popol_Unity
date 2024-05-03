using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnitMovement : CharacterProperty
{
    Coroutine move = null;
    Coroutine rotate = null;
    Coroutine follow = null;
    Coroutine sideMove = null;
    public Rigidbody rigid;

    float sideDir = 1.0f;
    float tempSpeed = 0;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    //Player
    public void MoveToPos(Vector3 dir, float Speed, UnityAction<float> blendAct)
    {
        if (move != null)
        {
            StopCoroutine(move);
            move = null;
        }

        move = StartCoroutine(MovingToPos(dir, Speed, blendAct));
    }

    //Player Stop
    public void StopMove(UnityAction<float> endAct)
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
        if(rotate != null)
        {
            StopCoroutine(rotate);
        }
        if(sideMove != null)
        {
            StopCoroutine(sideMove);
            sideMove = null;
        }

        tempSpeed = 0f;
        endAct?.Invoke(0.0f);
    }



    //Moster
    public void MoveToPos(Vector3 target, float Speed, UnityAction startAct, UnityAction endAct)
    {
        if (move != null)
        {
            StopCoroutine(move);
            move = null;
        }

        move = StartCoroutine(MovingToPos(target, Speed, startAct, endAct));
    }

    public void FollowTarget(Transform target, Info<float, float> info, UnityAction startAct, UnityAction endAct)
    {
        if (follow != null)
        {
            StopCoroutine(follow);
            follow = null;
        }

        follow = StartCoroutine(FollowingTarget(target, info, startAct, endAct));
    }

    public void SideMove(Transform target, Info<float, float> info, UnityAction startAct, UnityAction endAct)
    {
        if (sideMove != null)
        {
            StopCoroutine(sideMove);
            sideMove = null;
        }
        sideMove = StartCoroutine(SideMoving(target, info, startAct, endAct));
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
        if(sideMove != null)
        {
            StopCoroutine(sideMove);
            sideMove = null;
        }
        
        endAct?.Invoke();
    }

    
    //Player Roll
    public void Dadge(Vector3 dir, float dadge)
    {
        dir.Normalize();
        
        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, 10.0f));
        rigid.AddForce(dir * dadge);
    }

    //Player Move
    IEnumerator MovingToPos(Vector3 dir, float speed, UnityAction<float> blendAct) 
    {
        float dist = dir.magnitude;
        float delta;
        dir.Normalize();

        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir, speed));
        
        
        while (!Mathf.Approximately(dist, 0.0f))
        {
            if(dist <= 0.2f)
            {
                tempSpeed = Mathf.Lerp(tempSpeed, 0, Time.deltaTime * 9.0f);
            }
            else
            {
                tempSpeed = Mathf.Lerp(tempSpeed, 1, Time.deltaTime * 9.0f);
            }
            
            blendAct?.Invoke(tempSpeed);

            delta = tempSpeed * Time.deltaTime * speed;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.position += dir * delta;
            yield return null;
        }
        while (tempSpeed >= 0.01f)
        {
            tempSpeed = Mathf.Lerp(tempSpeed, 0, Time.deltaTime * 9.0f);
            blendAct?.Invoke(tempSpeed);
            yield return null;
        }
        tempSpeed = 0;
    }

    IEnumerator MovingToPos(Vector3 target, float speed, UnityAction startAct, UnityAction endAct)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit, dist, 1 << LayerMask.NameToLayer("Obstacle")))
        {
            dist = (hit.point - transform.position).magnitude - 1.0f;
        }

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

    public IEnumerator FollowingTarget(Transform target, Info<float, float> info, UnityAction startAct, UnityAction endAct)
    {
        //info arg0 : Speed, info arg1 : Offset
        float Offset = info.arg1;

        while (target != null)
        {
            //�ִϸ��̼�
            startAct?.Invoke();

            Vector3 dir = target.position - transform.position;
            //0.5�� ������
            float dist = dir.magnitude - Offset;
            if (dist < 0.0f) dist = 0.0f;
            float delta;


            dir.Normalize();
            delta = info.arg0 * Time.deltaTime;
            if (delta > dist) delta = dist;
            transform.Translate(dir * delta, Space.World);
            if (Mathf.Approximately(dist, 0.0f))
            {
                //�ִϸ��̼�
                endAct?.Invoke();
            }
            dir.y = 0;
            dir.Normalize();
            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = info.arg0 * 90.0f * Time.deltaTime;
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
            transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            yield return null;
        }
    }

    IEnumerator SideMoving(Transform target, Info<float, float> info, UnityAction startAct, UnityAction endAct)
    {
        //info arg0 : Speed, info arg1 : radius
        //Debug.Log("SideMoving");
        while (target != null)
        {
            //Animation
            startAct?.Invoke();
            //define
            float delta;
            float radius = info.arg1;
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            Vector3 revDir = (-dir.normalized * radius) + dir;
            revDir.y = 0;

            //Rotate
            dir.Normalize();
            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = info.arg0 * 90.0f * Time.deltaTime;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * rotDir * delta, Space.World);


            //SideMove
            dir = Vector3.Cross(dir, Vector3.down);
            dir.y = 0;
            dir.Normalize();
            dir = (dir * sideDir) + revDir;
            /*
            if (Physics.Raycast(transform.position, dir, transform.localScale.x, 1 << LayerMask.NameToLayer("Obstacle")))
            {
                sideDir = -sideDir;
            }
            */
            Debug.DrawRay(transform.position, dir);

            //Debug.Log($"dir : {dir} revDir : {revDir}");

            dir.Normalize();
            delta = info.arg0 * Time.deltaTime;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        endAct?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            sideDir = -sideDir;
        }
    }

}
