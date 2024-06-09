using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSummaryUI : MonoBehaviour
{
    [SerializeField] Button questSummaryHeadButton;        // ����Ʈ ��� ��� ��ư
    [SerializeField] TextMeshProUGUI questSummaryHeadText; // ����Ʈ ��� ��� �ؽ�Ʈ

    GameObject[] questSummaryList;                         // ����Ʈ ��� ��ư ������Ʈ ����Ʈ
    QuestSummaryButton[] questSummaryComponents;           // ����Ʈ ��� ��ư ������Ʈ ����Ʈ
    GameObject questSummaryButtonPrefab;                   // ����Ʈ ��� ��ư ������

    [SerializeField] Transform questSummaryListTransform;  // ����Ʈ ��� ��ư ���� ��ġ

    bool isQuestSummaryActive;                             // ����Ʈ ��� ��ư Ȱ��ȭ ����

    private void Awake()
    {
        // ���� ���ҽ� �ҷ�����
        LoadResources();
    }

    void Start()
    {
        // �ʱ�ȭ
        Initialize();
    }

    void Update()
    {
        // ����Ʈ ��� ��ư ����
        SetQuestSummary();
    }

    // ���� ���ҽ� �ҷ�����
    void LoadResources()
    {
        questSummaryButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/MainUICanvas_QuestSummaryButton");
    }

    // �ʱ�ȭ
    void Initialize()
    {
        // ��ư �̺�Ʈ ���
        questSummaryHeadButton.onClick.AddListener(ClickHeadButton);

        // ����Ʈ ��� ����Ʈ ����
        questSummaryList = new GameObject[QuestManager.instance.QuestCheckMaxCount];
        questSummaryComponents = new QuestSummaryButton[QuestManager.instance.QuestCheckMaxCount];

        // ����Ʈ ��� ��ư�� ����
        CreateQuestSummaryButtons();

        isQuestSummaryActive = true;

        // ��� ����Ʈ ��� ��ư Ȱ��ȭ �� ��� �ؽ�Ʈ ����
        SetActiveAllQuestSummaryButtons(true);
        isQuestSummaryActive = true;

        questSummaryHeadText.text = "����Ʈ ��� ����";
    }

    // ����Ʈ ��� ��ư�� ����
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

    // ����Ʈ ��� ����Ʈ ��ü Ȱ��ȭ ���� ����
    void SetActiveAllQuestSummaryButtons(bool active)
    {
        for(int i = 0; i < questSummaryList.Length; i++)
        {
            if (questSummaryList[i] != null) questSummaryList[i].SetActive(active);
        }
    }

    // ��� ��ư Ŭ�� ó��
    void ClickHeadButton()
    {
        //Debug.Log("����Ʈ ��ư Ŭ��");

        if (isQuestSummaryActive)
        {
            // ����Ʈ ��� ��ư���� Ȱ��ȭ ������ ���
            // ��� ����Ʈ ��� ��ư ��Ȱ��ȭ �� ��� �ؽ�Ʈ ����
            SetActiveAllQuestSummaryButtons(false);
            isQuestSummaryActive = false;

            questSummaryHeadText.text = "����Ʈ ��� ��ġ��";
        }
        else
        {
            // ����Ʈ ��� ��ư���� ��Ȱ��ȭ ������ ���
            // ��� ����Ʈ ��� ��ư Ȱ��ȭ �� ��� �ؽ�Ʈ ����
            SetActiveAllQuestSummaryButtons(true);
            isQuestSummaryActive = true;

            questSummaryHeadText.text = "����Ʈ ��� ����";
        }
    }

    // ����Ʈ ��� ��ư ����
    void SetQuestSummary()
    {
        if (isQuestSummaryActive)
        {
            // ����Ʈ ��� ��ư���� Ȱ��ȭ ������ ���

            // üũ ǥ�õ� ����Ʈ ��� �޾ƿ���
            List<QuestData> checkQuests = QuestManager.instance.FindCheckQuests();

            // ����Ʈ ��� ��ư ����
            for (int i = 0; i < questSummaryList.Length; i++)
            {
                if (i < checkQuests.Count)
                {
                    // üũ ǥ�õ� ���� ���� ���� ���
                    // ����Ʈ ��� ��ư Ȱ��ȭ �� ����Ʈ�� ���� ����Ʈ ��� ��ư ����
                    questSummaryList[i].SetActive(true);
                    questSummaryComponents[i].SetQuestSummary(checkQuests[i]);
                }
                else
                {
                    // üũ ǥ�õ� �������� ���� ���
                    // ����Ʈ ��� ��ư ��Ȱ��ȭ �� null�� ���� ����Ʈ ��� ��ư ����
                    questSummaryList[i].SetActive(false);
                    questSummaryComponents[i].SetQuestSummary(null);
                }
            }
        }
    }

    // ��ư���� ��ȣ�ۿ� ����
    public void SetButtonInteractable(bool interactable)
    {
        questSummaryHeadButton.interactable = interactable;

        for (int i = 0; i < QuestManager.instance.QuestCheckMaxCount; i++)
        {
            questSummaryComponents[i].SetButtonInteractable(interactable);
        }
    }
}
