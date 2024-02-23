using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    //타겟이 플레이어면 플레어 레이어로 하고 몬스터면 몬스터 레이어
    [SerializeField] private LayerMask targetMask;
    #endregion

    //protected 변수 영역
    #region protected

    #endregion

    //Public 변수영역
    #region public
    //스킬 아래에 콜라이더를 가진 오브젝트를 넣어서 이 오브젝트에 트리거 되면 스킬 시전 시작
    //플레이어의 경우 이 오브젝트는 사용하지 않는다
    public Transform detectRange;
    public Transform attackStartPos;
    //실제로 타격판정이 들어갈 오브젝트(투사체라던가)
    public GameObject areaOfEffect;
    public LayerMask tempLayerMask;

    #endregion
    //이벤트 함수들 영역
    #region Event
    #endregion
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion

    //private 함수들 영역
    #region PrivateMethod
    #endregion

    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    //이 스킬이 사용되었을때
    public void OnUse()
    {
        Vector3 size = new Vector3(
        areaOfEffect.transform.position.x * areaOfEffect.transform.lossyScale.x,
        areaOfEffect.transform.position.y * areaOfEffect.transform.lossyScale.y,
        areaOfEffect.transform.position.z * areaOfEffect.transform.lossyScale.z
        );
        Collider[] tempcol = Physics.OverlapBox(areaOfEffect.transform.position, size, Quaternion.identity, tempLayerMask);
        for(int i = 0; i < tempcol.Length; i++)
        {
            //Debug.Log(Collider.Game)
        }
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        areaOfEffect.transform.position = attackStartPos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}