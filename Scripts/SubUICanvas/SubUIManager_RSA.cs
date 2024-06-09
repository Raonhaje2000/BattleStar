using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubUIManager_RSA : MonoBehaviour
{
    public static SubUIManager_RSA instance;

    [SerializeField] GameObject shopUI;     // 상점 UI 오브젝트
    ShopUI shopUIComponent;                 // 상점 UI 컴포넌트

    [SerializeField] GameObject questUI;    // 퀘스트 UI 오브젝트
    QuestUI questUIComponent;               // 퀘스트 UI 컴포넌트

    [SerializeField] GameObject paidShopUI; // 유료 상점 UI 오브젝트
    PaidShopUI paidShopUIComponent;         // 유료 상점 UI 컴포넌트

    private void Awake()
    {
        if (instance == null) instance = this;

        // 관련 컴포넌트 불러오기
        LoadComponents();

        // 테스트용
        //GameManager.instance.SetSpecialityMarginPercent();
    }

    void Start()
    { 
        CloseSubUI_RSA();
    }

    // 관련 컴포넌트 불러오기
    public void LoadComponents()
    {
        shopUIComponent = shopUI.GetComponent<ShopUI>();
        questUIComponent = questUI.GetComponent<QuestUI>();
        paidShopUIComponent = paidShopUI.GetComponent<PaidShopUI>();
    }

    // 서브 UI 창 모두 닫기
    public void CloseSubUI_RSA()
    {
        shopUI.SetActive(false);
        questUI.SetActive(false);
        paidShopUI.SetActive(false);
    }

    // 상점 UI 열기
    public void OpenShopUI()
    {
        // 해당 UI를 제외한 나머지 UI 비활성화
        shopUI.SetActive(true);
        questUI.SetActive(false);
        paidShopUI.SetActive(false);

        // 상점 UI 초기화 (첫화면 세팅)
        shopUIComponent.ClickPurchaseTabButton();
    }

    // 퀘스트 UI 열기
    public void OpenQuestUI(QuestData quest)
    {
        // 해당 UI를 제외한 나머지 UI 비활성화
        shopUI.SetActive(false);
        questUI.SetActive(true);
        paidShopUI.SetActive(false);

        // 퀘스트 UI 초기화 (첫화면 세팅)
        questUIComponent.CreateQuestLists();
        questUIComponent.SetQuestDetailContentsObject(quest);
    }

    // 유료 상점 열기
    public void OpenPaidShopUI()
    {
        // 해당 UI를 제외한 나머지 UI 비활성화
        shopUI.SetActive(false);
        questUI.SetActive(false);
        paidShopUI.SetActive(true);

        // 유료 상점 UI 초기화 (첫화면 세팅)
        paidShopUIComponent.ClickGemTabButton();
    }
}
