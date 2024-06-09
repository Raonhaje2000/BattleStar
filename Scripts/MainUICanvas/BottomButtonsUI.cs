using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomButtonsUI : MonoBehaviour
{
    [SerializeField] Button informationButton; // �ɷ�ġ ��ư
    [SerializeField] Button inventoryButton;   // �κ��丮 ��ư
    [SerializeField] Button lodgingButton;     // ���� ��ư
    [SerializeField] Button questButton;       // ����Ʈ ��ư
    [SerializeField] Button paidShopButton;    // ���� ���� ��ư

    [SerializeField]
    TextMeshProUGUI[] shortcutKeyTexts;        // ����Ű �ؽ�Ʈ��
    KeyCode[] shortcutKeyCodes;                // ����Ű Ű�ڵ��

    void Start()
    {
        // �ʱ�ȭ
        Initialize();
    }

    void Update()
    {
        // ����Ű�� ���� ��� �ش� ��ư�� ���� UI ����
        if(Input.GetKeyDown(shortcutKeyCodes[0])) ClickInformationButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[1])) ClickInventoryButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[2])) ClickLodgingButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[3])) ClickQuestButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[4])) ClickPaidShopButton();
    }

    // �ʱ�ȭ
    void Initialize()
    {
        // ��ư �̺�Ʈ ���
        if (informationButton != null) informationButton.onClick.AddListener(ClickInformationButton);
        if (inventoryButton != null) inventoryButton.onClick.AddListener(ClickInventoryButton);
        if (lodgingButton != null) lodgingButton.onClick.AddListener(ClickLodgingButton);
        if (questButton != null) questButton.onClick.AddListener(ClickQuestButton);
        if (paidShopButton != null) paidShopButton.onClick.AddListener(ClickPaidShopButton);

        // ����Ű ����
        shortcutKeyCodes = new KeyCode[] { KeyCode.U, KeyCode.I, KeyCode.L, KeyCode.J, KeyCode.P };

        // ����Ű �ؽ�Ʈ ����
        SetShortcutKeyTexts();
    }

    // ����Ű �ؽ�Ʈ ����
    void SetShortcutKeyTexts()
    {
        for (int i = 0; i < shortcutKeyTexts.Length; i++)
        {
            // '(����Ű)' �������� �ؽ�Ʈ ����
            shortcutKeyTexts[i].text = string.Format("({0})", shortcutKeyCodes[i]);
        }
    }

    // �ɷ�ġ ��ư Ŭ�� ó��
    void ClickInformationButton()
    {
        //Debug.Log("�ɷ�ġ ��ư Ŭ��");

        // �ɷ�ġ UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.INFORMATION;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenInfo();
    }

    // �κ��丮 ��ư Ŭ�� ó��
    void ClickInventoryButton()
    {
        //Debug.Log("�κ��丮 ��ư Ŭ��");

        // �κ��丮 UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.INVENTORY;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenInven();
    }

    // ���� ��ư Ŭ�� ó��
    void ClickLodgingButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ���� UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.LODGING;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenQuarter();
    }

    // ����Ʈ ��ư Ŭ�� ó��
    void ClickQuestButton()
    {
        //Debug.Log("����Ʈ ��ư Ŭ��");

        // ����Ʈ UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.QUEST;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenQuestUI(null);
    }

    // ���� ���� ��ư Ŭ�� ó��
    void ClickPaidShopButton()
    {
        //Debug.Log("���� ���� ��ư Ŭ��");

        // ���� ���� UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.PAIDSHOP;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenPaidShopUI();
    }

    // ��ư���� ��ȣ�ۿ� ����
    public void SetButtonInteractable(bool interactable)
    {
        informationButton.interactable = interactable;
        inventoryButton.interactable = interactable;
        lodgingButton.interactable = interactable;
        questButton.interactable = interactable;
        paidShopButton.interactable = interactable;
    }
}
