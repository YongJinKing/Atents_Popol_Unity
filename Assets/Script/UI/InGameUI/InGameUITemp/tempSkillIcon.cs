using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSkillIcon : MonoBehaviour
{
    public GameObject Target;
    public GameObject tempTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (Target == true)
            {
                Target.SetActive(false);
            }
            else if (Target == false)
            {
                Target.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (tempTarget == true)
            {
                tempTarget.SetActive(false);
            }
            else if (tempTarget == false)
            {
                tempTarget.SetActive(true);
            }
        }
    }
}
