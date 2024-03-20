using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HitCheckSkillType : BaseSkillType
{
    #region Properties / Field
    #region Private
    #endregion

    #region protected
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected float hitDuration = 10;
    protected float remainDuration;
    [SerializeField] protected GameObject[] areaOfEffect;
    [SerializeField] protected int maxIndex;
    #endregion

    #region public
    public GameObject areaOfEffectPrefeb;
    public GameObject hitEffectPrefeb;
    public Transform[] attackStartPos;
    #endregion

    #region Event
    public UnityEvent<Collider> onSkillHitEvent;
    #endregion
    #endregion


    #region Method
    #region PrivateMethod
    #endregion

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

    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    protected abstract IEnumerator HitChecking(GameObject hitBox);
    #endregion


    #region EventHandler
    public abstract void OnSkillHitCheckStartEventHandler();
    public virtual void OnSkillHitCheckEndEventHandler()
    {
    }
    #endregion


    #region MonoBehaviour
    protected override void Start()
    {
        InitAreaOfEffect();
    }
    #endregion
}
