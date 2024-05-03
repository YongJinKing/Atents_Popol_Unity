using UnityEngine;

public class RootMotion : MonoBehaviour
{
    Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        transform.parent.position += myAnim.deltaPosition;
        transform.parent.rotation *= myAnim.deltaRotation;
    }
}
