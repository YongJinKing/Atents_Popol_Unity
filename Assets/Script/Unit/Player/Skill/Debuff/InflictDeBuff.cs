using UnityEngine;

public class InflictDeBuff : MonoBehaviour
{
    public E_StatusAbnormality myab;
    public void GetCol(Collider target)
    {
        Debug.Log("Ÿ��:" + target.name); 
        Status st = target.GetComponentInParent<Status>();
        if(st != null)
        {
            st.Add(myab);
        }
        else
        {
            Debug.Log("Can`t find Status");
        }
    }
}
