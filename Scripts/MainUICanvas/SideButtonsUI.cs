using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideButtonsUI : MonoBehaviour
{
    [Header("도시 행성 사이드 버튼")]
    [SerializeField] GameObject cityPlanetSideButtons;   // 도시 행성 사이드 버튼들

    [SerializeField] Button barButton;                   // 주점 버튼
    [SerializeField] Button shopButton;                  // 상점 버튼
    [SerializeField] Button dockyardButton;              // 조선소 버튼

    [SerializeField] TextMeshProUGUI barButtonText;      // 주점 버튼 텍스트
    [SerializeField] TextMeshProUGUI shopButtonText;     // 상점 버튼 텍스트
    [SerializeField] TextMeshProUGUI dockyardButtonText; // 조선소 버튼 텍스트

    [Header("전투 행성 사이드 버튼")]
    [SerializeField] GameObject battlePlanetSideButtons; // 전투 행성 사이드 버튼들

    [SerializeField] Button battleButton;                // 전투 버튼

    [SerializeField] TextMeshProUGUI battleButtonText;   // 주점 버튼 텍스트

    [Header("생활 행성 사이드 버튼")]
    [SerializeField] GameObject lifePlanetSideButtons;   // 생활 행성 사이드 버튼들

    [SerializeField] Button miningButton;                // 채굴 버튼
    [SerializeField] Button fishingButton;               // 낚시 버튼
    [SerializeField] Button huntingButton;               // 사냥 버튼 -> 스크립 버튼으로 이름 변경

    [SerializeField] TextMeshProUGUI miningButtonText;   // 채굴 버튼 텍스트
    [SerializeField] TextMeshProUGUI fishingButtonText;  // 낚시 버튼 텍스트
    [SerializeField] TextMeshProUGUI huntingButtonText;  // 사냥 버튼 텍스트

    //최적화플래그
    bool isClear = false;

    private void Awake()
    {
        // 버튼 리스너 세팅
        SetButtonListener();
    }

    void Start()
    {
        // 버튼 초기화 (처음 행성에 들어왔을 때)
        InitializeButton();
    }

    private void Update()
    {
        // 현재 UI 상태가 다 비활성화 상태인 경우
        if (GameManager.instance.CurrentRoom == ROOMSTATE.EXIT && !isClear)
        {
            isClear = true;

            // 버튼 상태 초기화
            InitializeButton();
        }
    }

    // 버튼 리스너 세팅
    void SetButtonListener()
    {
        // 도시 행성
        barButton.onClick.AddListener(ClickBarButton);
        shopButton.onClick.AddListener(ClickShopButton);
        dockyardButton.onClick.AddListener(ClickDockyardButton);

        // 전투 행성
        battleButton.onClick.AddListener(ClickBattleButton);

        // 생활 행성
        miningButton.onClick.AddListener(ClickMiningButton);
        fishingButton.onClick.AddListener(ClickFishingButton);
        huntingButton.onClick.AddListener(ClickHuntingButton);
    }

    // 버튼 상태 초기화
    public void InitializeButton()
    {
        // 위치 텍스트 초기화
        MainUIManager.instance.PositionUIComponent.SetPosition("");

        // 버튼 상호작용 여부 세팅
        SetInteractableButton();

        // 버튼 그룹 활성화 세팅
        SetActiveSideButtonGroup();
    }

    // 버튼 상호작용 여부 세팅
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

    // 버튼 그룹 활성화 세팅
    public void SetActiveSideButtonGroup()
    {
        // 현재 위치한 행성 종류에 맞춰 사이드 버튼 그룹 활성화 세팅
        switch (GameManager.instance.currentPlanet.planet_type)
        {
            case PLANET_TYPE.CITY:
                {
                    // 도시 행성인 경우
                    ActiveCityPlanetSideButtons();
                    break;
                }
            case PLANET_TYPE.BATTLE:
                {
                    // 전투 행성인 경우
                    ActiveBattlePlanetSideButtons();
                    break;
                }
            default:
                {
                    // 생활 행성인 경우
                    ActiveLifePlanetSideButtons();
                    break;
                }
        }
    }

    // 도시 행성 사이드 버튼들 활성화 (나머지 비활성화)
    public void ActiveCityPlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(true);
        battlePlanetSideButtons.SetActive(false);
        lifePlanetSideButtons.SetActive(false);
    }

    // 전투 행성 사이드 버튼들 활성화 (나머지 비활성화)
    public void ActiveBattlePlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(false);
        battlePlanetSideButtons.SetActive(true);
        lifePlanetSideButtons.SetActive(false);
    }

    // 생활 행성 사이드 버튼들 활성화 (나머지 비활성화)
    public void ActiveLifePlanetSideButtons()
    {
        cityPlanetSideButtons.SetActive(false);
        battlePlanetSideButtons.SetActive(false);
        lifePlanetSideButtons.SetActive(true);
    }

    // 주점 버튼 클릭
    void ClickBarButton()
    {
        //Debug.Log("주점 버튼 클릭");

        // 주점 버튼을 제외한 나머지 버튼 활성화
        barButton.interactable = false;
        shopButton.interactable = true;
        dockyardButton.interactable = true;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(barButtonText.text);

        // 주점 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.PUB;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenPub();
    }

    // 상점 버튼 클릭
    void ClickShopButton()
    {
        //Debug.Log("상점 버튼 클릭");

        // 상점 버튼을 제외한 나머지 버튼 활성화
        barButton.interactable = true;
        shopButton.interactable = false;
        dockyardButton.interactable = true;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(shopButtonText.text);

        // 상점 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.SHOP;
        if (SubUIManager_RSA.instance != null) SubUIManager_RSA.instance.OpenShopUI();
    }

    // 조선소 버튼 클릭
    void ClickDockyardButton()
    {
        //Debug.Log("조선소 버튼 클릭");

        // 조선소 버튼을 제외한 나머지 버튼 활성화
        barButton.interactable = true;
        shopButton.interactable = true;
        dockyardButton.interactable = false;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(dockyardButtonText.text);

        // 조선소 UI 열기
        GameManager.instance.CurrentRoom = ROOMSTATE.SHIPYARD;
        if (TestCanvasManager.instance != null) TestCanvasManager.instance.OpenSCY();
    }

    // 전투 버튼 클릭
    void ClickBattleButton()
    {
        //Debug.Log("전투 버튼 클릭");

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(battleButtonText.text);

        // 전투 씬 로드
        // (CurrentRoom 상태 확인 후 해당 씬 로드하는 방식)
        GameManager.instance.CurrentRoom = ROOMSTATE.BATTLE;
    }

    // 채굴 버튼 클릭
    void ClickMiningButton()
    {
        //Debug.Log("채굴 버튼 클릭");

        // 채굴 버튼을 제외한 나머지 버튼 활성화
        miningButton.interactable = false;
        fishingButton.interactable = true;
        huntingButton.interactable = true;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(miningButtonText.text);

        // 채굴 씬 로드
        // (CurrentRoom 상태 확인 후 해당 씬 로드하는 방식)
        GameManager.instance.CurrentRoom = ROOMSTATE.MINING;
    }

    // 낚시 버튼 클릭
    void ClickFishingButton()
    {
        //Debug.Log("낚시 버튼 클릭");

        // 낚시 버튼을 제외한 나머지 버튼 활성화
        miningButton.interactable = true;
        fishingButton.interactable = false;
        huntingButton.interactable = true;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(fishingButtonText.text);

        // 낚시 씬 로드
        // (CurrentRoom 상태 확인 후 해당 씬 로드하는 방식)
        GameManager.instance.CurrentRoom = ROOMSTATE.FISHING;
    }

    // 수렵 버튼 클릭
    void ClickHuntingButton()
    {
        //Debug.Log("수렵 버튼 클릭");

        // 수렵 버튼을 제외한 나머지 버튼 활성화
        miningButton.interactable = true;
        fishingButton.interactable = true;
        huntingButton.interactable = false;

        // 위치 텍스트 변경
        MainUIManager.instance.PositionUIComponent.SetPosition(huntingButtonText.text);

        // 수렵 씬 로드 -> 스크랩 씬으로 이름 변경됨
        // (CurrentRoom 상태 확인 후 해당 씬 로드하는 방식)
        GameManager.instance.CurrentRoom = ROOMSTATE.HUNTING;
    }

    // 버튼들의 상호작용 세팅
    public void SetButtonInteractable(bool interactable)
    {
        // 도시 행성
        barButton.interactable = interactable;
        shopButton.interactable = interactable;
        dockyardButton.interactable = interactable;

        // 전투 행성
        battleButton.interactable = interactable;

        // 생활 행성
        miningButton.interactable = interactable;
        fishingButton.interactable = interactable;
        huntingButton.interactable = interactable;
    }
}
