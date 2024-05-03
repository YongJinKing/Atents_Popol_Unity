
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int TextValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text =
        UnitCalculate.GetInstance().Calculate(TextValue);
    }
}
