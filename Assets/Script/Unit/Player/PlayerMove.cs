using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public LayerMask clickMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                
            }
        }


    }

    IEnumerable MovingToPos(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while(!Mathf.Approximately(dist, 0.0f))
        {
            float delta = 2.0f * Time.deltaTime;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
    }
}
