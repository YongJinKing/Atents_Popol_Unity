using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestIconImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var data = Resources.Load("Player/SkillEffect/Slash");
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
        data.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
        transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite=
        data.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
