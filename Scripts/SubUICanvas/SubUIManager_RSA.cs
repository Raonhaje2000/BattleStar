using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubUIManager_RSA : MonoBehaviour
{
    public static SubUIManager_RSA instance;

    [SerializeField] GameObject shopUI;     // ���� UI ������Ʈ
    ShopUI shopUIComponent;                 // ���� UI ������Ʈ

    [SerializeField] GameObject questUI;    // ����Ʈ UI ������Ʈ
    QuestUI questUIComponent;               // ����Ʈ UI ������Ʈ

    [SerializeField] GameObject paidShopUI; // ���� ���� UI ������Ʈ
    PaidShopUI paidShopUIComponent;         // ���� ���� UI ������Ʈ

    private void Awake()
    {
        if (instance == null) instance = this;

        // ���� ������Ʈ �ҷ�����
        LoadComponents();

        // �׽�Ʈ��
        //GameManager.instance.SetSpecialityMarginPercent();
    }

    void Start()
    { 
        CloseSubUI_RSA();
    }

    // ���� ������Ʈ �ҷ�����
    public void LoadComponents()
    {
        shopUIComponent = shopUI.GetComponent<ShopUI>();
        questUIComponent = questUI.GetComponent<QuestUI>();
        paidShopUIComponent = paidShopUI.GetComponent<PaidShopUI>();
    }

    // ���� UI â ��� �ݱ�
    public void CloseSubUI_RSA()
    {
        shopUI.SetActive(false);
        questUI.SetActive(false);
        paidShopUI.SetActive(false);
    }

    // ���� UI ����
    public void OpenShopUI()
    {
        // �ش� UI�� ������ ������ UI ��Ȱ��ȭ
        shopUI.SetActive(true);
        questUI.SetActive(false);
        paidShopUI.SetActive(false);

        // ���� UI �ʱ�ȭ (ùȭ�� ����)
        shopUIComponent.ClickPurchaseTabButton();
    }

    // ����Ʈ UI ����
    public void OpenQuestUI(QuestData quest)
    {
        // �ش� UI�� ������ ������ UI ��Ȱ��ȭ
        shopUI.SetActive(false);
        questUI.SetActive(true);
        paidShopUI.SetActive(false);

        // ����Ʈ UI �ʱ�ȭ (ùȭ�� ����)
        questUIComponent.CreateQuestLists();
        questUIComponent.SetQuestDetailContentsObject(quest);
    }

    // ���� ���� ����
    public void OpenPaidShopUI()
    {
        // �ش� UI�� ������ ������ UI ��Ȱ��ȭ
        shopUI.SetActive(false);
        questUI.SetActive(false);
        paidShopUI.SetActive(true);

        // ���� ���� UI �ʱ�ȭ (ùȭ�� ����)
        paidShopUIComponent.ClickGemTabButton();
    }
}
