using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeIntToString : MonoBehaviour
{
    public static string IntToStringUIDesc(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "한손 검";
        if(index == 1)
            Rtstring = "양손 검";
        if(index == 2)
            Rtstring = "한손 둔기";
        if(index == 3)
            Rtstring = "양손 둔기";
        if(index == 4)
            Rtstring = "창";
        if(index == 5)
            Rtstring = "단검";
        if(index == 6)
            Rtstring = "투창용 창";
        if(index == 10)
            Rtstring = "가죽";
        if(index == 11)
            Rtstring = "경갑";
        if(index == 12)
            Rtstring = "판금";
        return Rtstring;
    }
    public static string IntToStringRiggingType(int index)
    {
        string Rtstring = "";
        //0 : 무기 1 : 방어구
        if(index == 0)
            Rtstring = "공격력 : ";
        if(index == 1)
            Rtstring = "체력 : ";
       
        return Rtstring;
    }
    public static string IntToStringUISkillType(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "참격";
        if(index == 1)
            Rtstring = "참격";
        if(index == 2)
            Rtstring = "타격";
        if(index == 3)
            Rtstring = "타격";
        if(index == 4)
            Rtstring = "관통";
        if(index == 5)
            Rtstring = "참격";
        if(index == 6)
            Rtstring = "관통";
        if(index == 10)
            Rtstring = "가죽";
        if(index == 11)
            Rtstring = "경갑";
        if(index == 12)
            Rtstring = "판금";
        return Rtstring;
    }
    public static string IntToStringSkillFileName(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "OneHandSwordSkill";
        if(index == 1)
            Rtstring = "TwoHandSwordSkill";
        if(index == 2)
            Rtstring = "한손 둔기";
        if(index == 3)
            Rtstring = "양손 둔기";
        if(index == 4)
            Rtstring = "창";
        if(index == 5)
            Rtstring = "단검";
        if(index == 6)
            Rtstring = "투창용 창";
        if(index == 10)
            Rtstring = "가죽";
        if(index == 11)
            Rtstring = "경갑";
        if(index == 12)
            Rtstring = "판금";
        return Rtstring;
    }
    public static string IntToStringConditionFileName(int index)
    {
        string Rtstring = "";
        if(index == 1)
            Rtstring = "Buff";
        if(index == 2)
            Rtstring = "DeBuff";
        return Rtstring;
    }
}
