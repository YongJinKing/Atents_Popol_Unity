using UnityEngine;

public class PlayerTraking : MonoBehaviour
{
    public LayerMask targetLayer;
    public Vector3 offSet;
    [SerializeField]Transform target;

    public bool isRot;
    public float RotSpeed;
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, targetLayer);
        target = colliders[0].gameObject.transform.root;
        transform.SetParent(target);
        transform.position = target.position + offSet;
    }
    void Update()
    {
        if (isRot)
        {
            transform.GetChild(0).gameObject.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
        }
    }
}
