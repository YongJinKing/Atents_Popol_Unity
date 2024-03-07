using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    public void SlotBomb()
    {
        Destroy(gameObject);
    }
}
