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
    protected override IEnumerator HitChecking()
    {
        areaOfEffect[0].SetActive(true);

        remainDuration = hitDuration;
        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;

            Vector3 size = new Vector3(
            areaOfEffect[0].transform.position.x * areaOfEffect[0].transform.lossyScale.x,
            areaOfEffect[0].transform.position.y * areaOfEffect[0].transform.lossyScale.y,
            areaOfEffect[0].transform.position.z * areaOfEffect[0].transform.lossyScale.z
            );
            Collider[] tempcol = Physics.OverlapBox(areaOfEffect[0].transform.position, size, Quaternion.identity, targetMask);


            for (int i = 0; i < tempcol.Length; i++)
            {
                Debug.Log(tempcol[i].gameObject.name);
            }
            yield return null;
        }

        areaOfEffect[0].SetActive(false);
        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillActivated(Vector3 targetPos)
    {
        base.OnSkillActivated(targetPos);
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}
