using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictBuff : MonoBehaviour
{
    SkillManager skillManager;
    public E_Buff myBuff;
    public void GetBuff()
    {
        Status st = skillManager.Player.GetComponent<Status>();
        st.Add(myBuff);
    }

    private void Start()
    {
        skillManager = GetComponent<SkillManager>();
        GetBuff();
    }
}
