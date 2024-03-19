using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(obj1, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(obj2, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(obj3, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(obj4, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(obj5, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Instantiate(obj6, transform.position, transform.rotation);
        }
    }
}
