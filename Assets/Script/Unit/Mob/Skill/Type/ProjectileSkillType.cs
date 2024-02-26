using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//투사체를 날리는 형태의 스킬에대한 클래스
public class ProjectileSkillType : BaseSkillType
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

        //지속시간이 끝났다.
        //투사체가 제거됨
        Destroy(areaOfEffect[0]);
        areaOfEffect[0] = null;
        //투사체가 리로드됨
        InitAreaOfEffect();
        yield return null;
    }

    //투사체(Projectile)을 이동시키는 함수
    protected IEnumerator MovingToPos()
    {
        //어차피 그 방향으로 발사만 시킬것이기 때문에 dist 없이 간다.
        float delta = 0.0f;
        Vector3 dir;
        //투사체의 발사를 위해서 부모관계를 없앴다
        if (areaOfEffect != null)
        {
            areaOfEffect[0].transform.parent = null;
            dir = targetPos - areaOfEffect[0].transform.position;
            dir.Normalize();

            //지속시간동안 이동
            //areaOfEffect가 HitChecking에 의해서 사라지면 그대로 빠져나옴
            while (remainDuration >= 0.0f && areaOfEffect != null)
            {
                delta = Time.deltaTime;
                // 이동한다.
                areaOfEffect[0].transform.Translate(dir * delta);

                yield return null;
            }
        }

        //어차피 투사체가 삭제되는것은 HitChecking이 담당하므로
        //여기서는 할 필요 없다.

        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillActivated(Vector3 targetPos)
    {
        base.OnSkillActivated(targetPos);
        StartCoroutine(MovingToPos());
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}

