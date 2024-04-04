using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPartManager : MonoBehaviour, IGetDType
{
    public Part[] parts;
    public Transform[] attackStartPos;


    public DefenceType GetDType(Collider col)
    {
        DefenceType type = 0;

        foreach(Part part in parts)
        {
            if(part.col == col)
            {
                type = part.type;
                return type;
            }
        }

        return type;
    }

    public void DisActiveCol()
    {
        StartCoroutine(DisAcivating());
    }

    private IEnumerator DisAcivating()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (Part part in parts)
        {
            part.col.enabled = false;
        }
    }
}
