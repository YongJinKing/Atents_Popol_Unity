using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : Player
{

    public Transform meleeAttackStartPos;
    public GameObject[] skill = new GameObject[1];
    public Transform tempTarget;

    private void UseSkill1()
    {
        myAnim.SetTrigger("Attack");
        //skill[0].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }
    /*
    private void UseSkill2()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[1].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }
    */
    public void onUseSkill1()
    {
        skill[0].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }


    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            UseSkill1();
        }
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            UseSkill2();
        }
        */
    }
}
