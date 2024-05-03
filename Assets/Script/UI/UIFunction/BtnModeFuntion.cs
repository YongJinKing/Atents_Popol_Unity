
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;


public class BtnModeFuntion : MonoBehaviour
{
    public Sprite[] BtnImage;
    public GameObject BtnParents;
    public UnityEvent<int> ModeAction;
    int PrevIndex = -1;
    Button[] BtnFunctionList;
    Image[] BtnImageList;
    private void Start() 
    {
        BtnFunctionList = BtnParents.GetComponentsInChildren<Button>();
        BtnImageList = BtnParents.GetComponentsInChildren<Image>();
        for(int i = 0; i < BtnFunctionList.Length; i++)
        {
            int index = i;
            BtnFunctionList[i].onClick.AddListener(() => ChooseBtn(index));
        }
    }
    
    void OnDisable() 
    {
        CleanBtn(-1);
    }
    public void ChooseBtn(int index)
    {
        if(PrevIndex == index)
        {
            CleanBtn(-1);
            ModeAction?.Invoke(0);
            return;
        }
        else
        {
            CleanBtn(index);
            
            transform.GetChild(index).GetComponent<Image>().sprite = BtnImage[1];
            transform.GetChild(index).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
            ModeAction?.Invoke(index + 1);
        }
    }
    public void CleanBtn(int PrevIdxSetting)
    {
        
        for(int i = 0; i < BtnParents.transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = BtnImage[0];
            transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
        }
        PrevIndex = PrevIdxSetting;
    }
    

}
