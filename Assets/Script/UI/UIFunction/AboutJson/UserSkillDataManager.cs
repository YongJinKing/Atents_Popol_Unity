using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class UserSkillDataManager : MonoBehaviour
{
    private static UserSkillDataManager instance;
    public Dictionary<int, PlayerSkill_StringTable> dicStringDatas;

    
    public static UserSkillDataManager GetInstance()
    {
        if(UserSkillDataManager.instance == null)
            UserSkillDataManager.instance = new UserSkillDataManager();
        return UserSkillDataManager.instance;
    }
    public void UserSkillLoadDatas()
    {
        var Mestiarii_PlayerSkill_StringTable = Resources.Load<TextAsset>("UI/UserSkill/Json/Mestiarii_PlayerSkill_StringTable").text;
        var arrItemDatas = JsonConvert.DeserializeObject<PlayerSkill_StringTable[]>(Mestiarii_PlayerSkill_StringTable);
        this.dicStringDatas = arrItemDatas.ToDictionary(x => x.index);
    }
}
