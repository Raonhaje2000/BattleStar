using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideButtonsUI : MonoBehaviour
{
    [Header("���� �༺ ���̵� ��ư")]
    [SerializeField] GameObject cityPlanetSideButtons;   // ���� �༺ ���̵� ��ư��

    [SerializeField] Button barButton;                   // ���� ��ư
    [SerializeField] Button shopButton;                  // ���� ��ư
    [SerializeField] Button dockyardButton;              // ������ ��ư

    [SerializeField] TextMeshProUGUI barButtonText;      // ���� ��ư �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI shopButtonText;     // ���� ��ư �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI dockyardButtonText; // ������ ��ư �ؽ�Ʈ

    [Header("���� �༺ ���̵� ��ư")]
    [SerializeField] GameObject battlePlanetSideButtons; // ���� �༺ ���̵� ��ư��

    [SerializeField] Button battleButton;                // ���� ��ư

    [SerializeField] TextMeshProUGUI battleButtonText;   // ���� ��ư �ؽ�Ʈ

    [Header("��Ȱ �༺ ���̵� ��ư")]
    [SerializeField] GameObject lifePlanetSideButtons;   // ��Ȱ �༺ ���̵� ��ư��

    [SerializeField] Button miningButton;                // ä�� ��ư
    [SerializeField] Button fishingButton;               // ���� ��ư
    [SerializeField] Button huntingButton;               // ��� ��ư -> ��ũ�� ��ư���� �̸� ����

    [SerializeField] TextMeshProUGUI miningButtonText;   // ä�� ��ư �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI fishingButtonText;  // ���� ��ư �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI huntingButtonText;  // ��� ��ư �ؽ�Ʈ

    //����ȭ�÷���
    bool isClear = false;

    private void Awake()
    {
        // ��ư ������ ����
        SetButtonListener();
    }

    void Start()
    {
        // ��ư �ʱ�ȭ (ó�� �༺�� ������ ��)
        InitializeButton();
    }

    private void Update()
    {
        // ���� UI ���°� �� ��Ȱ��ȭ ������ ���
        if (GameManager.instance.CurrentRoom == ROOMSTATE.EXIT && !isClear)
        {
            isClear = true;

            // ��ư ���� �ʱ�ȭ
            InitializeButton();
        }
    }

    // ��ư ������ ����
    void SetButtonListener()
    {
        // ���� �༺
        barButton.onClick.AddListener(ClickBarButton);
        shopButton.onClick.AddListener(ClickShopButton);
        dockyardButton.onClick.AddListener(ClickDockyardButton);

        // ���� �༺
        battleButton.onClick.AddListener(ClickBattleButton);

        // ��Ȱ �༺
        miningButton.onClick.AddListener(ClickMiningButton);
        fishingButton.onClick.AddListener(ClickFishingButton);
        huntingButton.onClick.AddListener(ClickHuntingButton);
    }

    // ��ư ���� �ʱ�ȭ
    public void InitializeButton()
    {
        // ��ġ �ؽ�Ʈ �ʱ�ȭ
        MainUIManager.instance.PositionUIComponent.SetPosition("");

        // ��ư ��ȣ�ۿ� ���� ����
        SetInteractableButton();

        // ��ư �׷� Ȱ��ȭ ����
        SetActiveSideButtonGroup();
    }

    // ��ư ��ȣ�ۿ� ���� ����
    public void SetInteractableButton()
    {
        barButton.interactable = true;
        shopButton.interactable = true;
        dockyardButton.interactable = true;

        battleButton.interactable = true;

        miningButton.interactable = true;
        fishingButton.interactable = true;
        huntingButton.interactable = true;
    }

    // ��ư �׷� Ȱ��ȭ ����
    public void SetActiveSideButtonGroup()
    {
        // ���� ��ġ�� �༺ ������ ���� ���̵� ��ư �׷� Ȱ��ȭ ����
        switch (GameManager.instance.currentPlanet.planet_type)
        {
            case PLANET_TYPE.CITY:
                {
                    // ���� �༺�� ���
                    ActiveCityPlanetSideButtons();
                    break;
                }
            case PLANET_TYPE.BATTLE:
                {
                    // ���� �༺�� ���
                    ActiveBattlePlanetSideButtons();
                    break;
                }
            default:
                {
                    // ��Ȱ �༺�� ���
                    ActiveLifePlanetSideButtons();
                    break;
                }
        }
    }

    // ���� �༺ ���̵� ��ư�� Ȱ��ȭ (������ ��Ȱ��ȭ)
    public void ActiveCityPlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(true);
        battlePlanetSideButtons.SetActive(false);
        lifePlanetSideButtons.SetActive(false);
    }

    // ���� �༺ ���̵� ��ư�� Ȱ��ȭ (������ ��Ȱ��ȭ)
    public void ActiveBattlePlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(false);
        battlePlanetSideButtons.SetActive(true);
        lifePlanetSideButtons.SetActive(false);
    }

    // ��Ȱ �༺ ���̵� ��ư�� Ȱ��ȭ (������ ��Ȱ��ȭ)
    public void ActiveLifePlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(false);
        battlePlanetSideButtons.SetActive(false);
        lifePlanetSideButtons.SetActive(true);
    }

    // ���� ��ư Ŭ��
    void ClickBarButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ���� ��ư�� ������ ������ ��ư Ȱ��ȭ
        barButton.interactable = false;
        shopButton.interactable = true;
        dockyardButton.interactable = true;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(barButtonText.text);

        // ���� UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.PUB;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenPub();
    }

    // ���� ��ư Ŭ��
    void ClickShopButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ���� ��ư�� ������ ������ ��ư Ȱ��ȭ
        barButton.interactable = true;
        shopButton.interactable = false;
        dockyardButton.interactable = true;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(shopButtonText.text);

        // ���� UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.SHOP;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenShopUI();
    }

    // ������ ��ư Ŭ��
    void ClickDockyardButton()
    {
        //Debug.Log("������ ��ư Ŭ��");

        // ������ ��ư�� ������ ������ ��ư Ȱ��ȭ
        barButton.interactable = true;
        shopButton.interactable = true;
        dockyardButton.interactable = false;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(dockyardButtonText.text);

        // ������ UI ����
        GameManager.instance.CurrentRoom = ROOMSTATE.SHIPYARD;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenSCY();
    }

    // ���� ��ư Ŭ��
    void ClickBattleButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(battleButtonText.text);

        // ���� �� �ε�
        // (CurrentRoom ���� Ȯ�� �� �ش� �� �ε��ϴ� ���)
        GameManager.instance.CurrentRoom = ROOMSTATE.BATTLE;
    }

    // ä�� ��ư Ŭ��
    void ClickMiningButton()
    {
        //Debug.Log("ä�� ��ư Ŭ��");

        // ä�� ��ư�� ������ ������ ��ư Ȱ��ȭ
        miningButton.interactable = false;
        fishingButton.interactable = true;
        huntingButton.interactable = true;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(miningButtonText.text);

        // ä�� �� �ε�
        // (CurrentRoom ���� Ȯ�� �� �ش� �� �ε��ϴ� ���)
        GameManager.instance.CurrentRoom = ROOMSTATE.MINING;
    }

    // ���� ��ư Ŭ��
    void ClickFishingButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ���� ��ư�� ������ ������ ��ư Ȱ��ȭ
        miningButton.interactable = true;
        fishingButton.interactable = false;
        huntingButton.interactable = true;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(fishingButtonText.text);

        // ���� �� �ε�
        // (CurrentRoom ���� Ȯ�� �� �ش� �� �ε��ϴ� ���)
        GameManager.instance.CurrentRoom = ROOMSTATE.FISHING;
    }

    // ���� ��ư Ŭ��
    void ClickHuntingButton()
    {
        //Debug.Log("���� ��ư Ŭ��");

        // ���� ��ư�� ������ ������ ��ư Ȱ��ȭ
        miningButton.interactable = true;
        fishingButton.interactable = true;
        huntingButton.interactable = false;

        // ��ġ �ؽ�Ʈ ����
        MainUIManager.instance.PositionUIComponent.SetPosition(huntingButtonText.text);

        // ���� �� �ε� -> ��ũ�� ������ �̸� �����
        // (CurrentRoom ���� Ȯ�� �� �ش� �� �ε��ϴ� ���)
        GameManager.instance.CurrentRoom = ROOMSTATE.HUNTING;
    }

    // ��ư���� ��ȣ�ۿ� ����
    public void SetButtonInteractable(bool interactable)
    {
        // ���� �༺
        barButton.interactable = interactable;
        shopButton.interactable = interactable;
        dockyardButton.interactable = interactable;

        // ���� �༺
        battleButton.interactable = interactable;

        // ��Ȱ �༺
        miningButton.interactable = interactable;
        fishingButton.interactable = interactable;
        huntingButton.interactable = interactable;
    }
}
