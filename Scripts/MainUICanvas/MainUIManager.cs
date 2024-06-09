using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager instance;

    [SerializeField] GameObject informationUIObject;   // ĳ���� ���� UI ������Ʈ
    InformationUI informationUIComponent;              // ĳ���� ���� UI ������Ʈ
    
    [SerializeField] GameObject positionUIObject;      // ���� ��ġ UI ������Ʈ
    PositionUI positionUIComponent;                    // ���� ��ġ UI ������Ʈ

    [SerializeField] GameObject sideButtonsUIObject;   // ���� ��ư UI ������Ʈ
    SideButtonsUI sideButtonsUIComponent;              // ���� ��ư UI ������Ʈ
    
    [SerializeField] GameObject bottomButtonsUIObject; // �ϴ� ��ư UI ������Ʈ
    BottomButtonsUI bottomButtonsUIComponent;          // �ϴ� ��ư UI ������Ʈ

    [SerializeField] GameObject questSummaryUIObject;  // ����Ʈ ��� UI ������Ʈ
    QuestSummaryUI questSummaryUIComponent;            // ����Ʈ ��� UI ������Ʈ

    [SerializeField] GameObject moveButtonUIObject;    // �̵� UI ������Ʈ
    MoveButtonUI moveButtonUIComponent;                // �̵� UI ������Ʈ

    [SerializeField] GameObject systemMessageUIObject; // �ý��� �޼��� UI ������Ʈ
    SystemMessageUI systemMessageUIComponent;          // �ý��� �޼��� UI ������Ʈ

    #region ������Ƽ

    public InformationUI InformationUIComponent
    {
        get { return informationUIComponent; }
    }

    public PositionUI PositionUIComponent
    {
        get { return positionUIComponent; }
    }

    public SideButtonsUI SideButtonsUIComponent
    {
        get { return sideButtonsUIComponent; }
    }

    public BottomButtonsUI BottomButtonsUIComponent
    {
        get { return bottomButtonsUIComponent; }
    }

    public QuestSummaryUI QuestSummaryUIComponent
    {
        get { return questSummaryUIComponent; }
    }

    public MoveButtonUI MoveButtonUIComponent
    {
        get { return moveButtonUIComponent; }
    }

    public SystemMessageUI SystemMessageUIComponent
    {
        get { return systemMessageUIComponent; }
    }

    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;

        // ���� ������Ʈ �ҷ�����
        LoadComponents();
    }

    void Start()
    {
        // �ʱ�ȭ
        Initialize();

        // �ý��� �޼��� ����
        SetSystemMessage();
    }

    private void Update()
    {
        // ���̵� ��ư �׽�Ʈ��
        //OnlySideButtonTest();

        //if (Input.GetKeyDown(KeyCode.Escape)) GameManager.instance.CurrentRoom = ROOMSTATE.EXIT;

        if (GameManager.instance != null && GameManager.instance.CurrentRoom == ROOMSTATE.EXIT)
        {
            // ���� UI�� ��� ��Ȱ��ȭ ������ ��� (GameManager�� CurrentRoom�� Exit�� ���)
            // ���� UI�� ��� �ݱ�
            if (TestCanvasManager.instance != null) TestCanvasManager.instance.CloseUI();
            if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.CloseSubUI_RSA();

            // ĵ���� ���� ��� ��ư Ȱ��ȭ
            SetButtonsInteractable(true);
        }
        else
        {
            // ĵ���� ���� ��� ��ư ��Ȱ��ȭ
            SetButtonsInteractable(false);
        }
    }

    // ���̵� ��ư �׽�Ʈ��
    void OnlySideButtonTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            positionUIComponent.SetPosition("");
            sideButtonsUIComponent.SetInteractableButton();
            sideButtonsUIComponent.ActiveCityPlanetSideButtons();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            positionUIComponent.SetPosition("");
            sideButtonsUIComponent.SetInteractableButton();
            sideButtonsUIComponent.ActiveBattlePlanetSideButtons();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            positionUIComponent.SetPosition("");
            sideButtonsUIComponent.SetInteractableButton();
            sideButtonsUIComponent.ActiveLifePlanetSideButtons();
        }
    }

    // ���� ������Ʈ �ҷ�����
    void LoadComponents()
    {
        informationUIComponent = informationUIObject.GetComponent<InformationUI>();
        positionUIComponent = positionUIObject.GetComponent<PositionUI>();
        sideButtonsUIComponent = sideButtonsUIObject.GetComponent<SideButtonsUI>();
        bottomButtonsUIComponent = bottomButtonsUIObject.GetComponent<BottomButtonsUI>();
        questSummaryUIComponent = questSummaryUIObject.GetComponent<QuestSummaryUI>();
        moveButtonUIComponent = moveButtonUIObject.GetComponent<MoveButtonUI>();
        systemMessageUIComponent = systemMessageUIObject.GetComponent<SystemMessageUI>();
    }

    // �ʱ�ȭ
    void Initialize()
    {
        // ĵ���� ���� ��� UI Ȱ��ȭ
        ActiveAllUI(true);
    }

    // ĵ���� ���� ��� UI Ȱ��ȭ ����
    void ActiveAllUI(bool active)
    {
        informationUIObject.SetActive(active);
        positionUIObject.SetActive(active);
        sideButtonsUIObject.SetActive(active);
        bottomButtonsUIObject.SetActive(active);
        questSummaryUIObject.SetActive(active);
        moveButtonUIObject.SetActive(active);
        systemMessageUIObject.SetActive(active);
    }

    // ĵ���� ���� ��� ��ư Ȱ��ȭ ����
    void SetButtonsInteractable(bool interactable)
    {
        sideButtonsUIComponent.SetButtonInteractable(interactable);
        bottomButtonsUIComponent.SetButtonInteractable(interactable);
        questSummaryUIComponent.SetButtonInteractable(interactable);
        moveButtonUIComponent.SetButtonInteractable(interactable);
    }

    // �ý��� �޼��� ����
    void SetSystemMessage()
    {
        if(QuestManager.instance.NewQuestCount > 0)
        {
            // ���� ���� ����Ʈ�� �ִ� ���

            // �ý��� �޼��� ������Ʈ Ȱ��ȭ
            systemMessageUIObject.SetActive(true);

            // �ý��� �޼��� �ؽ�Ʈ ����
            string message = string.Format("���ο� ����Ʈ�� {0}�� �ֽ��ϴ�.", QuestManager.instance.NewQuestCount);
            systemMessageUIComponent.SetSystemMessage(message);
        }
        else
        {
            // ���� ���� ����Ʈ�� ���� ���
            
            // �ý��� �޼��� ������Ʈ ��Ȱ��ȭ
            systemMessageUIObject.SetActive(false);
        }
    }
}
