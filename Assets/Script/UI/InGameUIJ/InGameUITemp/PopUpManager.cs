using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    System.Action _OnClickConformButton, _OnClickCancelButton;
    private static PopUpManager _instance;
    public static PopUpManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject _popup;
    public Text _popMsg;

    public void Open(string text, 
        System.Action OnClickConformButton, System.Action OnClickCancelButton)
    {
        _popup.SetActive(true);
        _popMsg.text = text;
        _OnClickConformButton = OnClickConformButton;
        _OnClickCancelButton = OnClickCancelButton;
    }

    public void Close()
    {
        _popup.SetActive(false);
    }

    public void OnClickConformButton()
    {
        // 액션 콜백이 넘어왔을 때만(예외처리)
        if (_OnClickConformButton != null)
        {
            Debug.Log("확인 버튼 누름");
            _OnClickConformButton(); // 해당 델리게이트에 저장된 함수 실행
        }
        Close(); // 실행 후에는 창을 꺼준다.
    }

    // 취소 버튼을 눌렀을 때
    public void OnClickCancelButton()
    {
        if (_OnClickCancelButton != null)
        {
            Debug.Log("취소 버튼 누름");
            _OnClickCancelButton();
        }
        Close();
    }

    private void Awake()
    {
        _popup.SetActive(false);
        DontDestroyOnLoad(this);

        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
