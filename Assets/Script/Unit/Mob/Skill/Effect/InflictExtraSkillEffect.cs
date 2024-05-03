using UnityEngine;

public class InflictExtraSkillEffect : BaseSkillEffect
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    [SerializeField] LayerMask groundMask = 1 << 14;
    [SerializeField] LayerMask targetMask = 1 << 10;
    #endregion

    //Public 변수영역
    #region public
    public GameObject extraEffectObject;
    public bool isOnGround = false;
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
    public override void OnSkillHit(Collider target)
    {
        Vector3 pos = target.transform.position;
        GameObject temp = null;

        if (isOnGround)
        {
            Ray ray = new Ray(pos, Vector3.down);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, groundMask))
            {
                pos = hit.point;
            }
        }

        if (extraEffectObject != null)
        {
            temp = Instantiate<GameObject>(extraEffectObject, pos, Quaternion.identity);
        }

        if (temp != null)
        {
            temp.GetComponent<BaseHitBox>().Initialize(myBattleSystem, targetMask);
            temp.SetActive(true);
        }
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}