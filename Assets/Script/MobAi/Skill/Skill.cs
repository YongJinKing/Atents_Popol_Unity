using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //타겟이 플레이어면 플레어 레이어로 하고 몬스터면 몬스터 레이어
    [SerializeField]private LayerMask targetMask;
    //스킬 아래에 콜라이더를 가진 오브젝트를 넣어서 이 오브젝트에 트리거 되면 스킬 시전 시작
    //플레이어의 경우 이 오브젝트는 사용하지 않는다
    public Transform detectRange;
    //실제로 타격판정이 들어갈 오브젝트(투사체라던가)
    public Transform areaOfEffect;

}