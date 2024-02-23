using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Mob
{
    //일단 완전 스파게티로 짜보자
    public Transform meleeAttackStartPos;
    public GameObject[] skill = new GameObject[2];

    private void UseSkill1()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[0].GetComponent<Skill>().OnUse();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            UseSkill1();
        }
    }
}
