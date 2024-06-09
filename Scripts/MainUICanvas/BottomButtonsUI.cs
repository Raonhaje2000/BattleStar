using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomButtonsUI : MonoBehaviour
{
    [SerializeField] Button informationButton; // 능력치 버튼
    [SerializeField] Button inventoryButton;   // 인벤토리 버튼
    [SerializeField] Button lodgingButton;     // 숙소 버튼
    [SerializeField] Button questButton;       // 퀘스트 버튼
    [SerializeField] Button paidShopButton;    // 유료 상점 버튼

    [SerializeField]
    TextMeshProUGUI[] shortcutKeyTexts;        // 단축키 텍스트들
    KeyCode[] shortcutKeyCodes;                // 단축키 키코드들

    void Start()
    {
        // 초기화
        Initialize();
    }

    void Update()
    {
        // 단축키를 누른 경우 해당 버튼에 맞춰 UI 열기
        if(Input.GetKeyDown(shortcutKeyCodes[0])) ClickInformationButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[1])) ClickInventoryButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[2])) ClickLodgingButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[3])) ClickQuestButton();
        else if(Input.GetKeyDown(shortcutKeyCodes[4])) ClickPaidShopButton();
    }

    // 초기화
    void Initialize()
    {
        // 버튼 이벤트 등록
        if (informationButton != null) informationButton.onClick.AddListener(ClickInformationButton);
        if (inventoryButton != null) inventoryButton.onClick.AddListener(ClickInventoryButton);
        if (lodgingButton != null) lodgingButton.onClick.AddListener(ClickLodgingButton);
        if (questButton != null) questButton.onClick.AddListener(ClickQuestButton);
        if (paidShopButton != null) paidShopButton.onClick.AddListener(ClickPaidShopButton);

        // 단축키 설정
        shortcutKeyCodes = new KeyCode[] { KeyCode.U, KeyCode.I, KeyCode.L, KeyCode.J, KeyCode.P };

        // 단축키 텍스트 세팅
        SetShortcutKeyTexts();
    }

    // 단축키 텍스트 세팅
    void SetShortcutKeyTexts()
    {
        for (int i = 0; i < shortcutKeyTexts.Length; i++)
        {
            // '(단축키)' 형식으로 텍스트 세팅
            shortcutKeyTexts[i].text = string.Format("({0})", shortcutKeyCodes[i]);
        }
    }

    // 능력치 버튼 클릭 처리
    void ClickInformationButton()
    {
        //Debug.Log("능력치 버튼 클릭");

        // 능력치 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.INFORMATION;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenInfo();
    }

    // 인벤토리 버튼 클릭 처리
    void ClickInventoryButton()
    {
        //Debug.Log("인벤토리 버튼 클릭");

        // 인벤토리 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.INVENTORY;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenInven();
    }

    // 숙소 버튼 클릭 처리
    void ClickLodgingButton()
    {
        //Debug.Log("숙소 버튼 클릭");

        // 숙소 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.LODGING;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenQuarter();
    }

    // 퀘스트 버튼 클릭 처리
    void ClickQuestButton()
    {
        //Debug.Log("퀘스트 버튼 클릭");

        // 퀘스트 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.QUEST;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenQuestUI(null);
    }

    // 유료 상점 버튼 클릭 처리
    void ClickPaidShopButton()
    {
        //Debug.Log("유료 상점 버튼 클릭");

        // 유료 상점 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.PAIDSHOP;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenPaidShopUI();
    }

    // 버튼들의 상호작용 세팅
    public void SetButtonInteractable(bool interactable)
    {
        informationButton.interactable = interactable;
        inventoryButton.interactable = interactable;
        lodgingButton.interactable = interactable;
        questButton.interactable = interactable;
        paidShopButton.interactable = interactable;
    }
}
