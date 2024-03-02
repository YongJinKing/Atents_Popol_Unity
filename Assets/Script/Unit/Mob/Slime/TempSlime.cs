using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSlime : MonoBehaviour
{
    public Skill[] skills;
    public Transform tempTarget;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            skills[0].GetComponent<Skill>().OnSkillStart(tempTarget.position);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            skills[1].GetComponent<Skill>().OnSkillStart(tempTarget.position);
        }
    }
}
