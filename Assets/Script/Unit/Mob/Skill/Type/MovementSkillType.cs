using System.Collections;
using UnityEngine;
using UnityEngine.Events;


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
    //For use UnitMovement Class
    public UnityEvent<Vector3, float, UnityAction, UnityAction> moveToPosEvent;
    //When End Move
    public UnityEvent onMoveEndEvent;
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
        dir.Normalize();

        moveToPosEvent?.Invoke(selfObject.transform.position + dir * maxDist, moveSpeed, null, () => onMoveEndEvent?.Invoke());

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
