using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Skill : MonoBehaviour
{
    #region Properties / Field
    #region Private
    private bool isForceEnd = false;
    #endregion

    #region protected
    [SerializeField] protected float coolDownTime;
    protected float remainCoolDownTime = -1f;
    #endregion

    #region public
    public float detectRadius;
    public LayerMask targetMask;
    public uiUnitSkillStatus uiSkillStatus;
    public float preDelay = 0;
    public float postDelay = 0;
    public bool isLoopAnim = false;
    #endregion


    #region Event
    public UnityEvent<Transform> onSkillActivatedEvent;
    public UnityEvent onSkillHitCheckStartEvent;
    public UnityEvent onSkillHitCheckEndEvent;
    private UnityAction middleAct;
    private UnityAction endAct;

    //public UnityEvent onSkillAvailableEvent;
    public UnityAction onDetectTargetAct;

    public UnityEvent<UnityAction<Transform, UnityAction, UnityAction, UnityAction>,UnityAction ,UnityAction, UnityAction> onAddSkillEvent;
    public UnityEvent<UnityAction, bool, LayerMask> onAddSkillEvent2;
    #endregion
    #endregion

    #region Method
    //private 
    #region PrivateMethod
    #endregion

    //protected 
    #region ProtectedMethod
    #endregion

    //public 
    #region PublicMethod
    #endregion
    #endregion

    #region Coroutine
    protected IEnumerator CoolDownChecking()
    {
        remainCoolDownTime = coolDownTime;
        while (remainCoolDownTime > 0)
        {
            remainCoolDownTime -= Time.deltaTime;
            yield return null;
        }


        //onSkillAvailableEvent?.Invoke();
        yield return null;
    }

    protected IEnumerator DetectingRange()
    {
        yield return new WaitForEndOfFrame();
        bool isDetecting = false;

        while (!isDetecting)
        {
            Collider[] tempcol = Physics.OverlapSphere(transform.position, detectRadius, targetMask);

            for (int i = 0; i < tempcol.Length; i++)
            {
                if(tempcol[i] != null)
                {
                    onDetectTargetAct?.Invoke();
                    onDetectTargetAct = null;
                    isDetecting = true;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator ProcessDelay(float delayTime, UnityAction act)
    {
        if(delayTime > 0.0f)
            yield return new WaitForSeconds(delayTime);
        act?.Invoke();
    }
    #endregion

    #region EventHandler
    public void OnRequestSkillInfo()
    {
        onAddSkillEvent?.Invoke(OnSkillStart, OnSkillHitCheckStart,  OnSkillHitCheckEnd, OnSkillAnimEnd);
        onAddSkillEvent2?.Invoke(OnSkillForceEnd, isLoopAnim, targetMask);
    }

    public void OnCommandDetectSkillTarget(UnityAction detectAct)
    {
        onDetectTargetAct = detectAct;
        StartCoroutine(DetectingRange());
    }

    public void OnSkillStart(Transform target, UnityAction startAct, UnityAction middleAct, UnityAction endAct)
    {
        if (remainCoolDownTime <= 0)
        {
            //preDelayTime Process
            StartCoroutine(ProcessDelay(preDelay,
                () => 
                {
                    startAct?.Invoke();
                    onSkillActivatedEvent?.Invoke(target);
                }
                ));
            this.middleAct = middleAct;
            this.endAct = endAct;
        }
    }

    public void OnSkillHitCheckStart()
    {
        onSkillHitCheckStartEvent?.Invoke();
    }

    public void OnSkillHitCheckEnd()
    {
        onSkillHitCheckEndEvent?.Invoke();
    }

    //use for afterdelay
    public void OnSkillAnimEnd()
    {
        if (!isForceEnd)
        {
            middleAct?.Invoke();
            onSkillHitCheckEndEvent?.Invoke();
            StartCoroutine(ProcessDelay(postDelay,
                () =>
                {
                    OnSkillEnd();
                }));
        }
    }

    public void OnSkillEnd()
    {
        endAct?.Invoke();
        StartCoroutine(CoolDownChecking());
    }

    public void OnSkillForceEnd()
    {
        StopAllCoroutines();
        isForceEnd = true;
        onSkillHitCheckEndEvent?.Invoke();
    }
    #endregion


    #region MonoBehaviour
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new UnityEngine.Color(0, 1f, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, detectRadius);
    }
#endif
    #endregion
}