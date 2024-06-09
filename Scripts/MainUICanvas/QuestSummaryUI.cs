using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSummaryUI : MonoBehaviour
{
    [SerializeField] Button questSummaryHeadButton;        // 퀘스트 요약 헤더 버튼
    [SerializeField] TextMeshProUGUI questSummaryHeadText; // 퀘스트 요약 헤더 텍스트

    GameObject[] questSummaryList;                         // 퀘스트 요약 버튼 오브젝트 리스트
    QuestSummaryButton[] questSummaryComponents;           // 퀘스트 요약 버튼 컴포넌트 리스트
    GameObject questSummaryButtonPrefab;                   // 퀘스트 요약 버튼 프리팹

    [SerializeField] Transform questSummaryListTransform;  // 퀘스트 요약 버튼 생성 위치

    bool isQuestSummaryActive;                             // 퀘스트 요약 버튼 활성화 여부

    private void Awake()
    {
        // 관련 리소스 불러오기
        LoadResources();
    }

    void Start()
    {
        // 초기화
        Initialize();
    }

    void Update()
    {
        // 퀘스트 요약 버튼 세팅
        SetQuestSummary();
    }

    // 관련 리소스 불러오기
    void LoadResources()
    {
        questSummaryButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/MainUICanvas_QuestSummaryButton");
    }

    // 초기화
    void Initialize()
    {
        // 버튼 이벤트 등록
        questSummaryHeadButton.onClick.AddListener(ClickHeadButton);

        // 퀘스트 요약 리스트 관련
        questSummaryList = new GameObject[QuestManager.instance.QuestCheckMaxCount];
        questSummaryComponents = new QuestSummaryButton[QuestManager.instance.QuestCheckMaxCount];

        // 퀘스트 요약 버튼들 생성
        CreateQuestSummaryButtons();

        isQuestSummaryActive = true;

        // 모든 퀘스트 요약 버튼 활성화 및 헤더 텍스트 세팅
        SetActiveAllQuestSummaryButtons(true);
        isQuestSummaryActive = true;

        questSummaryHeadText.text = "퀘스트 목록 접기";
    }

    // 퀘스트 요약 버튼들 생성
    void CreateQuestSummaryButtons()
    {
        if (questSummaryButtonPrefab != null && questSummaryListTransform != null)
        {
            for (int i = 0; i < questSummaryList.Length; i++)
            {
                questSummaryList[i] = Instantiate(questSummaryButtonPrefab, questSummaryListTransform);
                questSummaryList[i].SetActive(false);

                questSummaryComponents[i] = questSummaryList[i].GetComponent<QuestSummaryButton>();
            }
        }
    }

    // 퀘스트 요약 리스트 전체 활성화 여부 설정
    void SetActiveAllQuestSummaryButtons(bool active)
    {
        for(int i = 0; i < questSummaryList.Length; i++)
        {
            if (questSummaryList[i] != null) questSummaryList[i].SetActive(active);
        }
    }

    // 헤더 버튼 클릭 처리
    void ClickHeadButton()
    {
        //Debug.Log("퀘스트 버튼 클릭");

        if (isQuestSummaryActive)
        {
            // 퀘스트 요약 버튼들이 활성화 상태인 경우
            // 모든 퀘스트 요약 버튼 비활성화 및 헤더 텍스트 세팅
            SetActiveAllQuestSummaryButtons(false);
            isQuestSummaryActive = false;

            questSummaryHeadText.text = "퀘스트 목록 펼치기";
        }
        else
        {
            // 퀘스트 요약 버튼들이 비활성화 상태인 경우
            // 모든 퀘스트 요약 버튼 활성화 및 헤더 텍스트 세팅
            SetActiveAllQuestSummaryButtons(true);
            isQuestSummaryActive = true;

            questSummaryHeadText.text = "퀘스트 목록 접기";
        }
    }

    // 퀘스트 요약 버튼 세팅
    void SetQuestSummary()
    {
        if (isQuestSummaryActive)
        {
            // 퀘스트 요약 버튼들이 활성화 상태인 경우

            // 체크 표시된 퀘스트 목록 받아오기
            List<QuestData> checkQuests = QuestManager.instance.FindCheckQuests();

            // 퀘스트 요약 버튼 세팅
            for (int i = 0; i < questSummaryList.Length; i++)
            {
                if (i < checkQuests.Count)
                {
                    // 체크 표시된 개수 보다 적은 경우
                    // 퀘스트 요약 버튼 활성화 및 퀘스트에 맞춰 퀘스트 요약 버튼 세팅
                    questSummaryList[i].SetActive(true);
                    questSummaryComponents[i].SetQuestSummary(checkQuests[i]);
                }
                else
                {
                    // 체크 표시된 개수보다 많은 경우
                    // 퀘스트 요약 버튼 비활성화 및 null에 맞춰 퀘스트 요약 버튼 세팅
                    questSummaryList[i].SetActive(false);
                    questSummaryComponents[i].SetQuestSummary(null);
                }
            }
        }
    }

    // 버튼들의 상호작용 세팅
    public void SetButtonInteractable(bool interactable)
    {
        questSummaryHeadButton.interactable = interactable;

        for (int i = 0; i < QuestManager.instance.QuestCheckMaxCount; i++)
        {
            questSummaryComponents[i].SetButtonInteractable(interactable);
        }
    }
}
