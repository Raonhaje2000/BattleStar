using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class QuestElement : MonoBehaviour, IPointerClickHandler
{
    const int STRING_LENGTH = 18;

    [SerializeField] TextMeshProUGUI questTitleText;
    [SerializeField] GameObject checkObject;

    QuestData questData;

    public GameObject CheckObject
    { 
        get { return checkObject; }
    }

    void Start()
    {

    }

    public void SetQuestElement(QuestData quest)
    {
        questData = quest;

        if (questData != null)
        {
            if (questTitleText != null)
            {
                if (questData.QuestTitle.Length <= STRING_LENGTH) questTitleText.text = questData.QuestTitle;
                else questTitleText.text = questData.QuestTitle.Substring(0, STRING_LENGTH) + "...";
            }

            if (checkObject != null) checkObject.SetActive(questData.ViewCheck);
        }
        else
        {
            if (checkObject != null) checkObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("퀘스트 버튼 왼쪽 버튼 클릭");

            // 클릭한 퀘스트의 세부 내용 띄우기
            if (questData != null)
            {
                QuestUI.instance.SetQuestDetailContentsObject(questData);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("퀘스트 버튼 오른쪽 버튼 클릭");

            // 체크박스를 기존 상태와 반대로 변경
            if (questData != null && checkObject != null)
            {
                if(!questData.ViewCheck && QuestUI.instance.QuestListCheckCount < QuestManager.instance.QuestCheckMaxCount)
                {
                    QuestUI.instance.QuestListCheckCount++;
                    questData.ViewCheck = true;
                }
                else if(questData.ViewCheck && QuestUI.instance.QuestListCheckCount > 0)
                {
                    QuestUI.instance.QuestListCheckCount--;
                    questData.ViewCheck = false;
                }

                checkObject.SetActive(questData.ViewCheck);
            }
            else if(checkObject != null)
            {
                // 테스트 용도
                if(!checkObject.activeSelf)
                {
                    if (QuestUI.instance.QuestListCheckCount < QuestManager.instance.QuestCheckMaxCount)
                    {
                        QuestUI.instance.QuestListCheckCount++;
                        checkObject.SetActive(!checkObject.activeSelf);
                    }
                }
                else
                {
                    if(QuestUI.instance.QuestListCheckCount > 0)
                    {
                        QuestUI.instance.QuestListCheckCount--;
                        checkObject.SetActive(!checkObject.activeSelf);
                    }
                }
            }
        }
    }
}
