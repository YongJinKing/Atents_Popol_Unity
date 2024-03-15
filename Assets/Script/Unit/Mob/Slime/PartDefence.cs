using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDefence : MonoBehaviour, IGetDType
{
    public Part[] parts;

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
}
