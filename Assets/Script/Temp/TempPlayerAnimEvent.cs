using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    public UnityEvent<int> End;

    public void OnEnd(int i)
    {
        End?.Invoke(i);
    }

}
