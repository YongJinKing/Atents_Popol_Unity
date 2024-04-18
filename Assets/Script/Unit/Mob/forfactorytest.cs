using UnityEngine;

public class forfactorytest : MonoBehaviour
{
    public Monster monster;
    // Start is called before the first frame update
    void Awake()
    {
        //MonsterFactory factory = new MonsterFactory();
        //GameObject obj = factory.CreateMonster(20001);
        //obj.GetComponent<Slime>().TempInit();

        monster.idleAI = new System.Collections.Generic.List<int>();
        monster.idleAI.Add(0);
        monster.CinematicEnd();
        
    }
}
