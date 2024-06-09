using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public static QuestUI instance;

    [Header("Äù½ºÆ® ¸ñ·Ï")]
    [SerializeField] GameObject listElementParent;

    GameObject questPlanetElement;
    GameObject questElement;

    [SerializeField]
    List<GameObject> questHeaderList;
    List<List<GameObject>> questList;
    List<GameObject> questListTemp;

    int questListCheckCount = 0;

    [Header("Äù½ºÆ® ¼³¸í")]
    [SerializeField] GameObject questDetailContents;
    QuestDetailContents questDetailContentsComponent;

    [Header("´Ý±â ¹öÆ°")]
    [SerializeField] Button exitButton;                         // ´Ý±â ¹öÆ°

    public int QuestListCheckCount
    {
        get { return questListCheckCount; }
        set { questListCheckCount = value; }
    }

    private void Awake()
    {
        if (instance == null) instance = this;

        questPlanetElement = Resources.Load<GameObject>("Prefabs/UI/QuestUI_QuestPlanetElement");
        questElement = Resources.Load<GameObject>("Prefabs/UI/QuestUI_QuestElement");

        questHeaderList = new List<GameObject>();
        questList = new List<List<GameObject>>();
        questListTemp = new List<GameObject>();

        if (questDetailContents != null) questDetailContentsComponent = questDetailContents.GetComponent<QuestDetailContents>();

        if (exitButton != null) exitButton.onClick.AddListener(ClickExitButton);

        ResetQuestUI();
    }

    void Start()
    {
        //ResetQuestUI();
    }

    void Update()
    {
        //OnlyTest();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickExitButton();
        }
    }

    public void OnlyTest()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            CreateQuestPlanetElement(null);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            CreateQuestElement(null);
        }
    }

    public void CreateQuestLists()
    {
        for (int i = 0; i < QuestManager.instance.ProgressQuests.Count; i++)
        {
            if (QuestManager.instance.ProgressQuests[i].Count > 0)
            {
                CreateQuestPlanetElement(QuestManager.instance.ProgressQuests[i][0]);

                questListTemp = new List<GameObject>();

                for (int j = 0; j < QuestManager.instance.ProgressQuests[i].Count; j++)
                {
                    CreateQuestElement(QuestManager.instance.ProgressQuests[i][j]);
                }

                questList.Add(questListTemp);
            }
        }
    }

    void CreateQuestPlanetElement(QuestData quest)
    {
        if(questPlanetElement != null && listElementParent != null)
        {
            GameObject newElement = Instantiate(questPlanetElement, listElementParent.transform);

            if (quest != null && newElement != null)
                newElement.GetComponent<QuestPlanetElement>().SetQuestPlanetElement(quest);

            questHeaderList.Add(newElement);
        }
    }

    void CreateQuestElement(QuestData quest)
    {
        if(questElement != null && listElementParent != null)
        {
            GameObject newElement = Instantiate(questElement, listElementParent.transform);

            if (quest != null && newElement != null)
                newElement.GetComponent<QuestElement>().SetQuestElement(quest);

            questListTemp.Add(newElement);
        }
    }

    public void SetQuestDetailContentsObject(QuestData quest)
    {
        if (quest != null)
        {
            questDetailContents.SetActive(true);

            questDetailContentsComponent.SetQuestDetailContents(quest);
        }
    }

    public void RemoveElement(QuestData quest)
    {
        int[] index = QuestManager.instance.FindQuest(quest);

        if (index != null)
        {
            QuestElement questElement = questList[index[0]][index[1]].GetComponent<QuestElement>();

            if (questElement != null && questElement.CheckObject.activeSelf) questListCheckCount--;

            Destroy(questList[index[0]][index[1]].gameObject);
            questList[index[0]].RemoveAt(index[1]);

            if (questList[index[0]].Count == 0)
            {
                questList.RemoveAt(index[0]);

                Destroy(questHeaderList[index[0]].gameObject);
                questHeaderList.RemoveAt(index[0]);
            }

            if (questDetailContents != null) questDetailContents.SetActive(false);
        }

        QuestManager.instance.CompleteQuest(quest);
    }

    void ResetQuestUI()
    {
        for(int i = 0; i < questHeaderList.Count; i++)
        {
            Destroy(questHeaderList[i].gameObject);
        }

        questHeaderList.Clear();

        for(int i = 0; i < questList.Count; i++)
        {
            for(int j = 0; j < questList[i].Count; j++)
            {
                Destroy(questList[i][j].gameObject);
            }
        }

        questList.Clear();

        if (questDetailContents != null) questDetailContents.SetActive(false);

        questListCheckCount = QuestManager.instance.CountCheckQuests();
    }

    void ClickExitButton()
    {
        // Äù½ºÆ® Ã¢ ÃÊ±âÈ­
        ResetQuestUI();
        GameManager.instance.currentRoom = ROOMSTATE.EXIT;
        gameObject.SetActive(false);
    }
}
