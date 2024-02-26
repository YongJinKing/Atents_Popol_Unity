using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    //�ϴ� ���� ���İ�Ƽ�� ¥����
    public Transform meleeAttackStartPos;
    public GameObject[] skill = new GameObject[2];
    public Transform tempTarget;

    private void UseSkill1()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[0].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }
    private void UseSkill2()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[1].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
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
        if (Input.GetButtonDown("Fire1"))
        {
            UseSkill2();
        }
    }
}
