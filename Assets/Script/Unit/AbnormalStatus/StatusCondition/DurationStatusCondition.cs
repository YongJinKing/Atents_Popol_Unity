using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationStatusCondition : StatusCondition
{
    protected int MaxCount = 9;
    protected int overlapCount = 1;
    protected float duration = 5.0f;

    private void Start()
    {
        //load duration data
        duration = ConditionDataManager.GetInstance().dicConditionDatas[(int)myStatusAbType + 2000].Condition_DurationTime;
        StartCoroutine(DurationChecking());
    }

    private IEnumerator DurationChecking()
    {
        float time = 0.0f;
        while(overlapCount > 0)
        {
            while(time < duration)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0.0f;
            --overlapCount;
        }
        base.Remove();
    }

    public override void Overlap()
    {
        if (++overlapCount > MaxCount)
            overlapCount = MaxCount;
    }
}
