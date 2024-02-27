using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//히트박스가 고정되어있는 근접공격등을 위한 클래스
public class MeleeSkillType : BaseSkillType
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    //공격이 지속되는 시간
    #endregion

    //protected 변수 영역
    #region protected
    #endregion

    //Public 변수영역
    #region public
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
    protected override IEnumerator HitChecking(GameObject hitBox)
    {
        hitBox.SetActive(true);

        remainDuration = hitDuration;

        //OverlapBox에서 쓸 box의 사이즈
        Vector3 size = new Vector3(
            hitBox.transform.position.x * hitBox.transform.lossyScale.x,
            hitBox.transform.position.y * hitBox.transform.lossyScale.y,
            hitBox.transform.position.z * hitBox.transform.lossyScale.z
            );

        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;
            Collider[] tempcol = Physics.OverlapBox(hitBox.transform.position, size, hitBox.transform.rotation, targetMask);


            for (int i = 0; i < tempcol.Length; i++)
            {
                Debug.Log(tempcol[i].gameObject.name);
            }
            yield return null;
        }

        hitBox.SetActive(false);
        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillActivated(Vector3 targetPos)
    {
        base.OnSkillActivated(targetPos);
        for (int i = 0; i < maxIndex; i++)
        {
            StartCoroutine(HitChecking(areaOfEffect[i]));
        }

    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}
