using UnityEngine;

public class forfactorytest : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MonsterFactory factory = new MonsterFactory();
        GameObject obj = factory.CreateMonster(30000);
        //obj.GetComponent<Slime>().TempInit();
    }
}
