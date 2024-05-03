#if UNITY_EDITOR
using UnityEngine;

public class forfactorytest : MonoBehaviour
{
    public Monster monster;
    public int index;
    public bool isFactoryTest;
    // Start is called before the first frame update
    void Awake()
    {
        if (isFactoryTest)
        {
            MonsterFactory factory = new MonsterFactory();
            GameObject obj = factory.CreateMonster(index);
            monster = obj.GetComponent<Monster>();
        }
        else
        {
            monster.idleAI = new System.Collections.Generic.List<int>();
            monster.idleAI.Add(0);
        }
        
        monster.CinematicEnd();
    }
}
#endif