using System.Collections;
using UnityEngine;


public class MovementSkillType : SelfSkillType
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    #endregion

    //Public 변수영역
    #region public
    //이동가능한 최대 거리
    public float maxDist = 5.0f;
    public float moveSpeed = 5.0f;
    #endregion

    //이벤트 함수들 영역
    #region Event
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    #endregion

    //protected 함수들 영역
    #region ProtectedMethod
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    protected IEnumerator MovingPos()
    {
        Vector3 dir = targetPos - selfObject.transform.position;
        //XZ 평면에서의 움직임만을 본다.
        dir = new Vector3(dir.x, 0, dir.z);
        //타겟과의 거리가 maxDist 보다 멀면 Clamp 최소치는 0
        float dist = Mathf.Clamp(dir.magnitude, 0, maxDist);
        dir.Normalize();

        float delta = 0.0f;
        while(dist >= 0.0f)
        {
            delta = Time.deltaTime * moveSpeed;
            if(delta > dist) delta = dist;
            dist -= delta;

            selfObject.transform.Translate(dir * delta);

            yield return null;
        }

        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillActivated(Vector3 targetPos)
    {
        base.OnSkillActivated(targetPos);
        StartCoroutine(MovingPos());
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}
