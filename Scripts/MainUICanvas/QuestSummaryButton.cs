using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSummaryButton : MonoBehaviour
{
    const int STRING_LENGTH = 18;                        // 표기 글자수

    Button questSummaryButton;                           // 퀘스트 요약 버튼

    [SerializeField] TextMeshProUGUI titleText;          // 제목 텍스트
    [SerializeField] TextMeshProUGUI contentText;        // 내용 텍스트
    [SerializeField] TextMeshProUGUI currentCountText;   // 현재 개수(마리수) 텍스트
    [SerializeField] TextMeshProUGUI maxCountText;       // 최대 개수(마리수) 텍스트

    [SerializeField] GameObject countTexts;              // 개수(마리수) 텍스트 오브젝트

    // 크기 조절용
    [SerializeField] RectTransform contentRectTransform; // 퀘스트 내용의 RectTransform 컴포넌트
    RectTransform rectTransform;                         // 퀘스트 요약 버튼(전체)의 RectTransform 컴포넌트

    QuestData questData;                                 // 퀘스트 내용

    private void Awake()
    {
        // Button 컴포넌트 불러오기 및 이벤트 등록
        questSummaryButton = GetComponent<Button>();
        questSummaryButton.onClick.AddListener(ClickQuestSummaryButton);

        // RectTransform 컴포넌트 불러오기
        rectTransform = GetComponent<RectTransform>();
    }

    // 퀘스트 요약 버튼 세팅
    public void SetQuestSummary(QuestData quest)
    {
        questData = quest;

        if (questData != null)
        {
            // 세팅할 퀘스트가 존재 하는 경우

            // 제목 텍스트 세팅
            // 글자 수가 표기 글자수 이하이면 그대로 출력, 아닌 경우 표기 글자수 만큼 자르고 뒤에 ... 추가 후 출력
            if (questData.QuestTitle.Length <= STRING_LENGTH) titleText.text = questData.QuestTitle;
            else titleText.text = questData.QuestTitle.Substring(0, STRING_LENGTH) + "...";

            if (questData.CurrentCount < questData.MaxCount)
            {
                // 현재 개수(마리수)가 최대 개수(마리수)보다 적은 경우 = 퀘스트 완료 조건이 달성되지 않은 경우

                // 내용 텍스트 세팅
                contentText.text = questData.QuestGoal;

                if (questData.QuestType != QuestData.Type.Planet)
                {
                    // 퀘스트 타입이 행성 방문이 아닌 경우

                    // 현재 개수(마리수)와 최대 개수(마리수) 텍스트 세팅
                    currentCountText.text = questData.CurrentCount.ToString();
                    maxCountText.text = questData.MaxCount.ToString();

                    // 개수(마리수) 텍스트 활성화
                    countTexts.SetActive(true);
                }
                else
                {
                    // 퀘스트 타입이 행성 방문인 경우

                    // 개수(마리수) 텍스트 비활성화
                    countTexts.SetActive(false);
                }
            }
            else
            {
                // 현재 개수(마리수)가 최대 개수(마리수) 이상인 경우 = 퀘스트 완료 조건이 달성된 경우

                // 내용 텍스트 세팅 및 개수(마리수) 텍스트 비활성화
                contentText.text = "완료 가능";
                countTexts.SetActive(false);
            }
        }

        // 내용에 맞춰 버튼 크기 세팅
        SetButtonSize();
    }

    // 내용에 맞춰 버튼 크기 세팅
    public void SetButtonSize()
    {
        // 내용 크기에 맞춰 요약 버튼(전체)의 크기 변경
        rectTransform.sizeDelta = contentRectTransform.sizeDelta;
    }

    // 퀘스트 요약 버튼 클릭 처리
    void ClickQuestSummaryButton()
    {
        //Debug.Log("퀘스트 요약 버튼 클릭");

        if (SubUIManager_RSA.instance != null)
        {
            // 서브 UI (퀘스트 UI)가 존재 하는 경우
            // 퀘스트 UI 열기
            GameManager.instance.CurrentRoom = ROOMSTATE.QUEST;
            SubUIManager_RSA.instance.OpenQuestUI(questData);
        }
    }

    // 버튼의 상호작용 세팅
    public void SetButtonInteractable(bool interactable)
    {
        questSummaryButton.interactable = interactable;
    }
}
