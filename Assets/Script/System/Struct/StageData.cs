public struct StageData
{
    public WaveData[] waveDatas;
}

public struct WaveData
{
    public int index;
    public int Stage_Index;
    public int[] Wave_MonsterArr;
    public int[] Wave_Monster_CountArr;
    public int Wave_Reward_Gold;
    public int Wave_Reward_Exp;
    public int[] Wave_Reward_ItemArr;
}
