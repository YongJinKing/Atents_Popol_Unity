using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class forfactorytest : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MonsterFactory factory = transform.AddComponent<MonsterFactory>();
        factory.CreateMonster(30000);
    }
}
