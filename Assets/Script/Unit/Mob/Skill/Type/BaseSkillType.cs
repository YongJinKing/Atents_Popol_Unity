using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//스킬의 히트박스와 피격박스를 위한 클래스
public abstract class BaseSkillType : MonoBehaviour
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
    //OnSkillActivated로 받아온 타겟의 위치를 저장함
    protected Vector3 targetPos;
    //인스턴타이즈화된 areaOfEffectPrefeb을 저장하는곳
    [SerializeField] protected GameObject areaOfEffect;
    #endregion

    //Public 변수영역
    #region public
    //스킬이 시작될 위치
    public Transform attackStartPos;
    //스킬 공격범위라던가를 설정할 오브젝트 프리펩
    public GameObject areaOfEffectPrefeb;
    #endregion
    //이벤트 함수들 영역
    #region Event
    //스킬
    public UnityEvent onSkillHitEvent;
    #endregion
    #endregion

    #region Method
    //private 함수들 영역
    #region PrivateMethod
    #endregion


    //protected 함수들 영역
    #region ProtectedMethod
    protected void InitAreaOfEffect()
    {
        if (areaOfEffect != null) return;

        areaOfEffect = Instantiate(areaOfEffectPrefeb);
        areaOfEffect.transform.SetParent(attackStartPos.transform, false);
        areaOfEffect.transform.position = attackStartPos.position;
        areaOfEffect.SetActive(false);
    }
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion
    #endregion



    //코루틴 영역
    #region Coroutine
    //히트박스에 들어갔는지 아닌지 체크하는 코루틴
    protected virtual IEnumerator HitChecking()
    {
        areaOfEffect.SetActive(true);

        remainDuration = hitDuration;
        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;

            Vector3 size = new Vector3(
            areaOfEffect.transform.position.x * areaOfEffect.transform.lossyScale.x,
            areaOfEffect.transform.position.y * areaOfEffect.transform.lossyScale.y,
            areaOfEffect.transform.position.z * areaOfEffect.transform.lossyScale.z
            );
            Collider[] tempcol = Physics.OverlapBox(areaOfEffect.transform.position, size, Quaternion.identity, targetMask);


            for (int i = 0; i < tempcol.Length; i++)
            {
                Debug.Log(tempcol[i].gameObject.name);
            }
            yield return null;
        }

    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    //skill 클래스의 이벤트가 발생
    public virtual void OnSkillActivated(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        StartCoroutine(HitChecking());
    }
    #endregion





    //유니티 함수들 영역
    #region MonoBehaviour
    protected virtual void Awake()
    {
        InitAreaOfEffect();
    }
    #endregion
}

