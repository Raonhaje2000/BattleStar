using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSummaryButton : MonoBehaviour
{
    const int STRING_LENGTH = 18;                        // ǥ�� ���ڼ�

    Button questSummaryButton;                           // ����Ʈ ��� ��ư

    [SerializeField] TextMeshProUGUI titleText;          // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI contentText;        // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI currentCountText;   // ���� ����(������) �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI maxCountText;       // �ִ� ����(������) �ؽ�Ʈ

    [SerializeField] GameObject countTexts;              // ����(������) �ؽ�Ʈ ������Ʈ

    // ũ�� ������
    [SerializeField] RectTransform contentRectTransform; // ����Ʈ ������ RectTransform ������Ʈ
    RectTransform rectTransform;                         // ����Ʈ ��� ��ư(��ü)�� RectTransform ������Ʈ

    QuestData questData;                                 // ����Ʈ ����

    private void Awake()
    {
        // Button ������Ʈ �ҷ����� �� �̺�Ʈ ���
        questSummaryButton = GetComponent<Button>();
        questSummaryButton.onClick.AddListener(ClickQuestSummaryButton);

        // RectTransform ������Ʈ �ҷ�����
        rectTransform = GetComponent<RectTransform>();
    }

    // ����Ʈ ��� ��ư ����
    public void SetQuestSummary(QuestData quest)
    {
        questData = quest;

        if (questData != null)
        {
            // ������ ����Ʈ�� ���� �ϴ� ���

            // ���� �ؽ�Ʈ ����
            // ���� ���� ǥ�� ���ڼ� �����̸� �״�� ���, �ƴ� ��� ǥ�� ���ڼ� ��ŭ �ڸ��� �ڿ� ... �߰� �� ���
            if (questData.QuestTitle.Length <= STRING_LENGTH) titleText.text = questData.QuestTitle;
            else titleText.text = questData.QuestTitle.Substring(0, STRING_LENGTH) + "...";

            if (questData.CurrentCount < questData.MaxCount)
            {
                // ���� ����(������)�� �ִ� ����(������)���� ���� ��� = ����Ʈ �Ϸ� ������ �޼����� ���� ���

                // ���� �ؽ�Ʈ ����
                contentText.text = questData.QuestGoal;

                if (questData.QuestType != QuestData.Type.Planet)
                {
                    // ����Ʈ Ÿ���� �༺ �湮�� �ƴ� ���

                    // ���� ����(������)�� �ִ� ����(������) �ؽ�Ʈ ����
                    currentCountText.text = questData.CurrentCount.ToString();
                    maxCountText.text = questData.MaxCount.ToString();

                    // ����(������) �ؽ�Ʈ Ȱ��ȭ
                    countTexts.SetActive(true);
                }
                else
                {
                    // ����Ʈ Ÿ���� �༺ �湮�� ���

                    // ����(������) �ؽ�Ʈ ��Ȱ��ȭ
                    countTexts.SetActive(false);
                }
            }
            else
            {
                // ���� ����(������)�� �ִ� ����(������) �̻��� ��� = ����Ʈ �Ϸ� ������ �޼��� ���

                // ���� �ؽ�Ʈ ���� �� ����(������) �ؽ�Ʈ ��Ȱ��ȭ
                contentText.text = "�Ϸ� ����";
                countTexts.SetActive(false);
            }
        }

        // ���뿡 ���� ��ư ũ�� ����
        SetButtonSize();
    }

    // ���뿡 ���� ��ư ũ�� ����
    public void SetButtonSize()
    {
        // ���� ũ�⿡ ���� ��� ��ư(��ü)�� ũ�� ����
        rectTransform.sizeDelta = contentRectTransform.sizeDelta;
    }

    // ����Ʈ ��� ��ư Ŭ�� ó��
    void ClickQuestSummaryButton()
    {
        //Debug.Log("����Ʈ ��� ��ư Ŭ��");

        if (SubUIManager_RSA.instance != null)
        {
            // ���� UI (����Ʈ UI)�� ���� �ϴ� ���
            // ����Ʈ UI ����
            GameManager.instance.CurrentRoom = ROOMSTATE.QUEST;
            SubUIManager_RSA.instance.OpenQuestUI(questData);
        }
    }

    // ��ư�� ��ȣ�ۿ� ����
    public void SetButtonInteractable(bool interactable)
    {
        questSummaryButton.interactable = interactable;
    }
}
