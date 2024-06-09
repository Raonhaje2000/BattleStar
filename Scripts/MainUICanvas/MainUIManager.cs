using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager instance;

    [SerializeField] GameObject informationUIObject;   // 캐릭터 정보 UI 오브젝트
    InformationUI informationUIComponent;              // 캐릭터 정보 UI 컴포넌트
    
    [SerializeField] GameObject positionUIObject;      // 현재 위치 UI 오브젝트
    PositionUI positionUIComponent;                    // 현재 위치 UI 컴포넌트

    [SerializeField] GameObject sideButtonsUIObject;   // 측면 버튼 UI 오브젝트
    SideButtonsUI sideButtonsUIComponent;              // 측면 버튼 UI 컴포넌트
    
    [SerializeField] GameObject bottomButtonsUIObject; // 하단 버튼 UI 오브젝트
    BottomButtonsUI bottomButtonsUIComponent;          // 하단 버튼 UI 컴포넌트

    [SerializeField] GameObject questSummaryUIObject;  // 퀘스트 요약 UI 오브젝트
    QuestSummaryUI questSummaryUIComponent;            // 퀘스트 요약 UI 컴포넌트

    [SerializeField] GameObject moveButtonUIObject;    // 이동 UI 오브젝트
    MoveButtonUI moveButtonUIComponent;                // 이동 UI 컴포넌트

    [SerializeField] GameObject systemMessageUIObject; // 시스템 메세지 UI 오브젝트
    SystemMessageUI systemMessageUIComponent;          // 시스템 메세지 UI 컴포넌트

    #region 프로퍼티

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

        // 관련 컴포넌트 불러오기
        LoadComponents();
    }

    void Start()
    {
        // 초기화
        Initialize();

        // 시스템 메세지 설정
        SetSystemMessage();
    }

    private void Update()
    {
        // 사이드 버튼 테스트용
        //OnlySideButtonTest();

        //if (Input.GetKeyDown(KeyCode.Escape)) GameManager.instance.CurrentRoom = ROOMSTATE.EXIT;

        if (GameManager.instance != null && GameManager.instance.CurrentRoom == ROOMSTATE.EXIT)
        {
            // 현재 UI가 모두 비활성화 상태인 경우 (GameManager의 CurrentRoom이 Exit인 경우)
            // 관련 UI들 모두 닫기
            if (TestCanvasManager.instance != null) TestCanvasManager.instance.CloseUI();
            if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.CloseSubUI_RSA();

            // 캔버스 내의 모든 버튼 활성화
            SetButtonsInteractable(true);
        }
        else
        {
            // 캔버스 내의 모든 버튼 비활성화
            SetButtonsInteractable(false);
        }
    }

    // 사이드 버튼 테스트용
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

    // 관련 컴포넌트 불러오기
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

    // 초기화
    void Initialize()
    {
        // 캔버스 내의 모든 UI 활성화
        ActiveAllUI(true);
    }

    // 캔버스 내의 모든 UI 활성화 세팅
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

    // 캔버스 내의 모든 버튼 활성화 세팅
    void SetButtonsInteractable(bool interactable)
    {
        sideButtonsUIComponent.SetButtonInteractable(interactable);
        bottomButtonsUIComponent.SetButtonInteractable(interactable);
        questSummaryUIComponent.SetButtonInteractable(interactable);
        moveButtonUIComponent.SetButtonInteractable(interactable);
    }

    // 시스템 메세지 세팅
    void SetSystemMessage()
    {
        if(QuestManager.instance.NewQuestCount > 0)
        {
            // 새로 받은 퀘스트가 있는 경우

            // 시스템 메세지 오브젝트 활성화
            systemMessageUIObject.SetActive(true);

            // 시스템 메세지 텍스트 세팅
            string message = string.Format("새로운 퀘스트가 {0}개 있습니다.", QuestManager.instance.NewQuestCount);
            systemMessageUIComponent.SetSystemMessage(message);
        }
        else
        {
            // 새로 받은 퀘스트가 없는 경우
            
            // 시스템 메세지 오브젝트 비활성화
            systemMessageUIObject.SetActive(false);
        }
    }
}
