#if UNITY_EDITOR
using UnityEngine;

public class forfactorytest : MonoBehaviour
{
    public Monster monster;
    // Start is called before the first frame update
    void Awake()
    {
        //MonsterFactory factory = new MonsterFactory();
        //GameObject obj = factory.CreateMonster(30002);
        //monster = obj.GetComponent<Monster>();

        monster.idleAI = new System.Collections.Generic.List<int>();
        monster.idleAI.Add(0);
        
        monster.CinematicEnd();
    }
}
#endif