#if UNITY_EDITOR
using UnityEngine;

public class TempGizmos : MonoBehaviour
{
    private Collider _Col;

    public Collider myCol
    {
        get
        {
            if(_Col == null)
            {
                _Col = GetComponent<Collider>();
            }
            return _Col;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1f,0,0,0.5f);
        Gizmos.DrawSphere(myCol.bounds.center, myCol.bounds.extents.x);
    }
}
#endif