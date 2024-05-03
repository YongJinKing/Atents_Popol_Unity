using UnityEngine;

public class KnockBackSkillEffect : BaseSkillEffect
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    [SerializeField] protected float _knockBackPower;
    [SerializeField] protected float _knockUpPower;
    #endregion

    //Public 변수영역
    #region public
    public float knockBackPower
    {
        get { return _knockBackPower; } 
        set { _knockBackPower = value;}
    }
    public float knockUpPower
    {
        get { return _knockUpPower; }
        set { _knockUpPower = value; }
    }
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
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillHit(Collider target, Vector3 pos)
    {
        Rigidbody rb = target.attachedRigidbody;
        if(rb == null) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        rb.AddForce(dir * knockBackPower, ForceMode.Impulse);
        rb.AddForce(Vector3.up * knockUpPower, ForceMode.Impulse);
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion

}
