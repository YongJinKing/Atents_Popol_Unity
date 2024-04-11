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
        // �׼� �ݹ��� �Ѿ���� ����(����ó��)
        if (_OnClickConformButton != null)
        {
            Debug.Log("Ȯ�� ��ư ����");
            _OnClickConformButton(); // �ش� ��������Ʈ�� ����� �Լ� ����
        }
        Close(); // ���� �Ŀ��� â�� ���ش�.
    }

    // ��� ��ư�� ������ ��
    public void OnClickCancelButton()
    {
        if (_OnClickCancelButton != null)
        {
            Debug.Log("��� ��ư ����");
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
