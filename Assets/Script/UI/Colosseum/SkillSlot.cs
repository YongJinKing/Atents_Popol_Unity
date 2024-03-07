using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    public GameObject ColosseumEventManager;
    public void SlotBomb()
    {
        
        Destroy(gameObject);
    }
}
