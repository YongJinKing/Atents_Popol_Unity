using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//투사체를 날리는 형태의 스킬에대한 클래스
public class ProjectileSkillType : AttackSkillType
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    [SerializeField] protected LayerMask unpenetrableMask;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float parabolaHeight = 2f;
    //최대 사거리(xz 평면에서만)
    [SerializeField] protected float maxDist = 5f;
    #endregion

    //Public 변수영역
    #region public
    //투사체가 부숴졌을때 생성될 이펙트
    public GameObject destroyEffectPrefeb;
    //투사체가 관통이 되냐 안되냐를 판정
    public bool penetrable = false;
    #endregion

    //이벤트 함수들 영역
    #region Event
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    private void DestroyProjectile(GameObject hitBox)
    {
        //시도때도 없이 호출되는 함수라서 예외처리를 확실히 해줘야한다.
        if(hitBox != null)
        {
            if(destroyEffectPrefeb != null)
            {
                Instantiate(destroyEffectPrefeb, hitBox.transform.position, Quaternion.identity);
            }

            Destroy(hitBox);
            hitBox = null;
        }
    }
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
        //이미 충돌판정이 끝난 오브젝트들
        HashSet<Collider> calculatedObject = new HashSet<Collider>();

        //발동 시점의 targetPos를 저장하므로 여러번 한다고 쳤을때 targetPos가 바뀌어서 나갈일은 없을 듯?
        //람다식으로 DestroyProjectile 함수를 쓰도록 했다.
        //람다식으로 되있는 부분은 거리가 다되면 오브젝트 제거하도록 한부분이다.
        StartCoroutine(LinearMovingToPos(hitBox, targetPos, () => DestroyProjectile(hitBox)));
        //StartCoroutine(ParabolaMovingPos(hitBox, targetPos));

        //투사체 각각이 남은시간을 가지고 있어야 하므로 코루틴에다가 remainDuration을 지역변수로 재정의 했다.
        float remainDuration = hitDuration;

        //Collider tempcollider = hitBox.GetComponent<BoxCollider>();

        //OverlapBox에서 쓸 box의 사이즈
        Vector3 size = new Vector3(
            hitBox.transform.position.x * hitBox.transform.lossyScale.x,
            hitBox.transform.position.y * hitBox.transform.lossyScale.y,
            hitBox.transform.position.z * hitBox.transform.lossyScale.z
            );

        while (remainDuration >= 0.0f && hitBox != null)
        {
            remainDuration -= Time.deltaTime;
            Collider[] tempcol = Physics.OverlapBox(hitBox.transform.position, size, hitBox.transform.rotation, targetMask | unpenetrableMask);

            if (isdebuging_Gizmo)
            {
                //DebugGizmos(hitBox.transform.position, size);
            }

            for (int i = 0; i < tempcol.Length; i++)
            {
                //Projectile destroy because of ununpenetrableMask
                if ((1 << tempcol[i].gameObject.layer & unpenetrableMask) != 0)
                {
                    DestroyProjectile(hitBox);
                    break;
                }
                //지금껏 충돌 해보지 못한 오브젝트와 충돌했을시
                //스킬이 맞았다고 이벤트 발생
                if (!calculatedObject.Contains(tempcol[i]))
                {
                    //Debug.Log For check
                    Debug.Log(tempcol[i].gameObject.name);
                    //맞췄을때 이펙트를 넣어줌
                    HitEffectPlay(hitBox.transform.position, tempcol[i].gameObject.transform.position);

                    //맞췄으니 효과 적용용으로 히트 이벤트 발생
                    onSkillHitEvent?.Invoke(tempcol[i].gameObject);

                    if (penetrable)
                    {
                        calculatedObject.Add(tempcol[i]);
                    }
                    else
                    {
                        DestroyProjectile(hitBox);
                        //i를 바꿔버림으로써 루프에서 나감
                        i = tempcol.Length;
                    }

                }
            }
            yield return null;
        }

        //지속시간이 끝났다.
        //투사체가 제거됨
        //이 부분은 지속시간이 끝나서 제거되는 부분이다.
        DestroyProjectile(hitBox);
        yield return null;
    }

    //투사체(Projectile)을 이동시키는 함수
    protected IEnumerator LinearMovingToPos(GameObject hitBox, Vector3 targetPos, UnityAction distEndAct)
    {
        //어차피 그 방향으로 발사만 시킬것이기 때문에 dist 없이 간다.
        float delta = 0.0f;
        float dist = maxDist;
        Vector3 dir;
        //투사체의 발사를 위해서 부모관계를 없앴다
        if (hitBox != null)
        {
            hitBox.transform.SetParent(null);
            dir = targetPos - hitBox.transform.position;
            dir.Normalize();
            //xz 평면에서 가는 거리
            float xzDist = (new Vector3(dir.x, 0, dir.z)).magnitude;

            //지속시간동안 이동
            //hitBox가 HitChecking에 의해서 사라지면 그대로 빠져나옴
            while (hitBox != null && dist >= 0.0f)
            {
                delta = Time.deltaTime * moveSpeed;
                //xz 평면안에서 간 거리만큼만 뺀다.
                dist -= xzDist * delta;
                // 이동한다.
                hitBox.transform.Translate(dir * delta, Space.World);

                yield return null;
            }
            //반복을 나왔으므로 최대 사거리 까지 간것이다.
            distEndAct?.Invoke();
        }

        //어차피 투사체가 삭제되는것은 HitChecking이 담당하므로
        //여기서는 할 필요 없다.

        yield return null;
    }

    //투사체를 포물선으로 이동시키는 함수
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
        base.OnSkillActivated(targetPos + Vector3.up * 1.0f);
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
    protected override void Start()
    {
        areaOfEffect = new GameObject[maxIndex];
        base.Start();
    }
    #endregion
}

