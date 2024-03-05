using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : CharacterMovement
{
    public NavMeshPath myPath;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void MoveToPos(Vector3 pos)
    {
        //myPath에 경로가 저장된다.
        if(NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, myPath))
        {
            switch (myPath.status)
            {
                //못감
                case NavMeshPathStatus.PathInvalid:
                    break;
                //가다가 막힘
                case NavMeshPathStatus.PathPartial:
                    break;
                //갈수 있음
                case NavMeshPathStatus.PathComplete:
                    break;
            }
            StopAllCoroutines();
            StartCoroutine(MovingByPath(myPath.corners));
        }
    }

    IEnumerator MovingByPath(Vector3[] path)
    {
        for(int i = 0; i < path.Length - 1;)
        {
            //path[0]에는 현재 위치가 저장되어 있다.
            yield return StartCoroutine(base.MovingToPos(path[i + 1], () => i++, () =>
            {
                myAnim.SetFloat("f_MoveSpeed", 1.0f);
            }));

        }

        while (myAnim.GetFloat("f_MoveSpeed") > 0.01f)
        {
            myAnim.SetFloat("f_MoveSpeed", Mathf.Lerp(myAnim.GetFloat("f_MoveSpeed"), 0.0f, Time.deltaTime * 3.0f));
            yield return null;
        }
        myAnim.SetFloat("f_MoveSpeed", 0.0f);

        yield return null;
    }
}
