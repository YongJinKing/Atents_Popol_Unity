using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillType : BaseSkillType
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private

    #endregion

    //protected 변수 영역
    #region protected
    //타겟이 플레이어면 플레어 레이어로 하고 몬스터면 몬스터 레이어
    [SerializeField] protected LayerMask targetMask;
    //히트박스가 지속되는 시간
    [SerializeField] protected float hitDuration;
    //남은 지속시간을 계산하기 위한 변수 serial은 그냥 값이 줄어드는지 확인하기 위한것으로 수정하려고 만든것이 아니다.
    [SerializeField] protected float remainDuration;
    //인스턴타이즈화된 areaOfEffectPrefeb을 저장하는곳
    [SerializeField] protected GameObject[] areaOfEffect;
    //areaOfEffect를 최대 몇개까지 만들것이냐를 결정하기 위함
    //예를들어 투사체 클래스의 경우에는 2개 이상으로 만들면 여러개 발사 할수 있도록
    [SerializeField] protected int maxIndex;
    #endregion

    //Public 변수영역
    #region public
    //스킬 공격범위라던가를 설정할 오브젝트 프리펩
    public GameObject areaOfEffectPrefeb;
    //공격이 맞았을때 생성될 이펙트
    public GameObject hitEffectPrefeb;
    //스킬이 시작될 위치
    public Transform[] attackStartPos;
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
    protected virtual void InitAreaOfEffect()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            if (areaOfEffect[i] == null)
            {
                areaOfEffect[i] = Instantiate(areaOfEffectPrefeb);
                areaOfEffect[i].transform.SetParent(attackStartPos[i].transform, false);
                areaOfEffect[i].transform.position = attackStartPos[i].position;
                areaOfEffect[i].SetActive(false);
            }
        }
    }

    //맞췄을때 이펙트가 나오도록 하는 함수
    protected virtual void HitEffectPlay(Vector3 hitBoxPos, Vector3 targetPos)
    {
        if (hitEffectPrefeb != null)
        {
            Ray ray = new Ray(hitBoxPos, targetPos - hitBoxPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 10f, targetMask))
            {
                Instantiate(hitEffectPrefeb, hit.point, Quaternion.identity);
            }
        }
    }
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    //히트박스에 들어갔는지 아닌지 체크하는 코루틴
    protected abstract IEnumerator HitChecking(GameObject hitBox);
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    protected override void Start()
    {
        InitAreaOfEffect();
    }
    #endregion
}
