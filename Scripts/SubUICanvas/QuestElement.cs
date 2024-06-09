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
            //Debug.Log("����Ʈ ��ư ���� ��ư Ŭ��");

            // Ŭ���� ����Ʈ�� ���� ���� ����
            if (questData != null)
            {
                QuestUI.instance.SetQuestDetailContentsObject(questData);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("����Ʈ ��ư ������ ��ư Ŭ��");

            // üũ�ڽ��� ���� ���¿� �ݴ�� ����
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
                // �׽�Ʈ �뵵
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
