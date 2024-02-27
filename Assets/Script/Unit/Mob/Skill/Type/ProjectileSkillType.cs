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
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float parabolaHeight = 2f;
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

        //발동 시점의 targetPos를 저장하므로 여러번 한다고 쳤을때 targetPos가 바뀌어서 나갈일은 없을 듯?
        //StartCoroutine(LinearMovingToPos(hitBox, targetPos));
        StartCoroutine(ParabolaMovingPos(hitBox, targetPos));

        //투사체 각각이 남은시간을 가지고 있어야 하므로 코루틴에다가 remainDuration을 지역변수로 재정의 했다.
        float remainDuration = hitDuration;
        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;

            Vector3 size = new Vector3(
            hitBox.transform.position.x * hitBox.transform.lossyScale.x,
            hitBox.transform.position.y * hitBox.transform.lossyScale.y,
            hitBox.transform.position.z * hitBox.transform.lossyScale.z
            );
            Collider[] tempcol = Physics.OverlapBox(hitBox.transform.position, size, Quaternion.identity, targetMask);


            for (int i = 0; i < tempcol.Length; i++)
            {
                Debug.Log(tempcol[i].gameObject.name);
            }
            yield return null;
        }

        //지속시간이 끝났다.
        //투사체가 제거됨
        Destroy(hitBox);
        hitBox = null;
        yield return null;
    }

    //투사체(Projectile)을 이동시키는 함수
    protected IEnumerator LinearMovingToPos(GameObject hitBox, Vector3 targetPos)
    {
        //어차피 그 방향으로 발사만 시킬것이기 때문에 dist 없이 간다.
        float delta = 0.0f;
        Vector3 dir;
        //투사체의 발사를 위해서 부모관계를 없앴다
        if (hitBox != null)
        {
            hitBox.transform.SetParent(null);
            dir = targetPos - hitBox.transform.position;
            dir.Normalize();

            //지속시간동안 이동
            //hitBox가 HitChecking에 의해서 사라지면 그대로 빠져나옴
            while (hitBox != null)
            {
                delta = Time.deltaTime * moveSpeed;
                // 이동한다.
                hitBox.transform.Translate(dir * delta, Space.World);

                yield return null;
            }
        }

        //어차피 투사체가 삭제되는것은 HitChecking이 담당하므로
        //여기서는 할 필요 없다.

        yield return null;
    }

    protected IEnumerator ParabolaMovingPos(GameObject hitBox, Vector3 targetPos)
    {
        Vector3 middlePos;
        float dist = 0.0f;
        float delta = 0.0f;

        // 타겟 위치의 바닥을 조준하기 위해 레이를 쐈다
        Ray ray = new Ray(targetPos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Abs(targetPos.y) + 2, LayerMask.GetMask("Ground")))
        {
            targetPos = hit.point;
        }
        //현 위치와 목표위치 중간지점의 좌표
        middlePos = (targetPos + transform.position) / 2.0f;
        dist = (middlePos - transform.position).magnitude;
        //distSecond = (targetPos - middlePos).magnitude;
        //중간지점의 좌표에서 y좌표를 올림
        middlePos.y += parabolaHeight;

        Vector3 dir;
        //투사체의 발사를 위해서 부모관계를 없앴다
        if (hitBox != null)
        {
            hitBox.transform.SetParent(null);
            dir = middlePos - hitBox.transform.position;
            dir.Normalize();
            //Vector3 dirXZ = new Vector3(dir.x, 0, dir.z);
            //Vector3 dirY = new Vector3(0, dir.y, 0);

            //지속시간동안 이동
            //hitBox가 HitChecking에 의해서 사라지면 그대로 빠져나옴
            while (hitBox != null && dist >= 0.01f)
            {
                delta = Time.deltaTime * moveSpeed;
                dist -= delta;
                // 이동한다.
                hitBox.transform.Translate(dir * delta, Space.World);

                yield return null;
            }
        }

        if (hitBox != null)
        {
            dir = targetPos - hitBox.transform.position;
            dir.Normalize();
            //Vector3 dirXZ = new Vector3(dir.x, 0, dir.z);
            //Vector3 dirY = new Vector3(0, dir.y, 0);

            //지속시간동안 이동
            //hitBox가 HitChecking에 의해서 사라지면 그대로 빠져나옴
            //마지막으로 떨어질때는 그냥 계속 가게 둔다.
            while (hitBox != null)
            {
                delta = Time.deltaTime * moveSpeed;
                // 이동한다.
                hitBox.transform.Translate(dir * delta, Space.World);

                yield return null;
            }
        }


        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillActivated(Vector3 targetPos)
    {
        base.OnSkillActivated(targetPos);
        for(int i = 0; i < maxIndex; i++)
        {
            StartCoroutine(HitChecking(areaOfEffect[i]));
            //발사 시키고 없앤다
            areaOfEffect[i] = null;
        }
        //투사체를 다시 리로드
        InitAreaOfEffect();
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    protected override void Awake()
    {
        areaOfEffect = new GameObject[maxIndex];
        base.Awake();
    }
    #endregion
}

