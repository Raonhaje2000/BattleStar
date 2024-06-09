using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    List<List<QuestData>> progressQuests;

    [SerializeField] int questCheckMaxCount;

    int newQuestCount;

    public List<List<QuestData>> ProgressQuests
    { 
        get { return progressQuests; }
        set { progressQuests = value; }
    }

    public int QuestCheckMaxCount
    {
        get { return questCheckMaxCount; }
    }

    public int NewQuestCount
    {
        get { return newQuestCount; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            progressQuests = new List<List<QuestData>>();
            questCheckMaxCount = 3;

            //OnlyTest();

            ResetQuests();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        newQuestCount = 0;

        // �ش� �༺�� �湮�� ���� ���� ��� �༺�� ����Ʈ �޾ƿ���
        AddPlanetQuests(GameManager.instance.currentPlanet);
    }

    public void OnlyTest()
    {
        // *** �׽�Ʈ�� �ڵ� *** //
        List<PlanetInfo> testPlanet = new List<PlanetInfo>();

        testPlanet.Add(Resources.Load<PlanetInfo>("ItemData/PlanetData/Mercury"));
        testPlanet.Add(Resources.Load<PlanetInfo>("ItemData/PlanetData/Sun"));

        for (int i = 0; i < testPlanet.Count; i++)
        {
            AddPlanetQuests(testPlanet[i]);
            testPlanet[i].visit = false;
        }
    }

    // ���� ���� �� �ʱ�ȭ �� �ڵ�
    public void ResetQuests()
    {
        PlanetInfo[] planets = Resources.LoadAll<PlanetInfo>("ItemData/PlanetData/Planet");

        for(int i = 0; i < planets.Length; i++)
        {
            // ����Ʈ ���
            string path = "ItemData/QuestData/" + planets[i].name;
            QuestData[] quests = Resources.LoadAll<QuestData>(path);

            for (int j = 0; j < quests.Length; j++)
            {
                quests[j].CurrentCount = 0;

                quests[j].Progress = false;
                quests[j].CompletePossible = false;
            }

            planets[i].planetQuests = new List<QuestData>();
            planets[i].planetQuests.AddRange(quests);

            planets[i].visit = false;
        }
    }

    // �༺ ����Ʈ �߰�
    public void AddPlanetQuests(PlanetInfo planet)
    {
        // �༺�� �湮�� ���� ���� ��� (ù �湮�� ���)
        if(!planet.visit && planet.planetQuests != null && planet.planetQuests.Count > 0)
        {
            progressQuests.Add(planet.planetQuests); // �ش� �༺�� ����Ʈ ��Ͽ� �߰�
            newQuestCount = planet.planetQuests.Count;
            
            planet.visit = true; // �༺ �湮 ǥ��

            for(int i = 0; i < planet.planetQuests.Count; i++)
            {
                planet.planetQuests[i].Progress = true;
                planet.planetQuests[i].ViewCheck = false;
            }
        }
        else
        {
            newQuestCount = 0;
        }
    }

    // �ش� ����Ʈ�� ��� �󿡼��� �ε��� ã��
    public int[] FindQuest(QuestData quest)
    {
        int[] index = null;

        for(int i = 0; i < progressQuests.Count; i++)
        {
            for(int j = 0; j < progressQuests[i].Count; j++)
            {
                if(quest.QuestId == progressQuests[i][j].QuestId)
                {
                    index = new int[] { i, j };

                    Debug.Log($"Find Index: [{i}, {j}]");
                    break;
                }
            }
        }

        return index;
    }

    // ����Ʈ �����ϱ�
    public void RemoveQuest(QuestData quest)
    {
        for (int i = 0; i < progressQuests.Count; i++)
        {
            for (int j = 0; j < progressQuests[i].Count; j++)
            {
                if (quest.QuestId == progressQuests[i][j].QuestId)
                {
                    progressQuests[i].RemoveAt(j);
                    j--;
                }
            }

            if(progressQuests[i].Count == 0)
            {
                progressQuests.RemoveAt(i);
                i--;
            }
        }
    }

    // üũ�� ����Ʈ ��� ã��
    public List<QuestData> FindCheckQuests()
    {
        List<QuestData> checkQuests = new List<QuestData>();
        int count = 0;

        for (int i = 0; i < progressQuests.Count; i++)
        {
            for (int j = 0; j < progressQuests[i].Count; j++)
            {
                if (progressQuests[i][j].ViewCheck)
                {
                    checkQuests.Add(progressQuests[i][j]);
                    count++;
                }

                // üũ�� ����Ʈ ����� �� ã���� ��� (�ִ� üũ ����ŭ ã�� ���)
                if (count == questCheckMaxCount) break;
            }
        }

        return checkQuests;
    }

    public int CountCheckQuests()
    {
        int count = 0;

        for (int i = 0; i < progressQuests.Count; i++)
        {
            for (int j = 0; j < progressQuests[i].Count; j++)
            {
                if (progressQuests[i][j].ViewCheck) count++;

                // �ִ� üũ ����ŭ ã�� ���
                if (count == questCheckMaxCount) break;
            }
        }

        return count;
    }

    // ����Ʈ ���� Ȯ�� - �༺ �湮
    public void CheckQuestPlanet()
    {

        for (int i = 0; i < progressQuests.Count; i++)
        {
            for (int j = 0; j < progressQuests[i].Count; j++)
            {
                if (progressQuests[i][j].QuestType == QuestData.Type.Planet && !progressQuests[i][j].CompletePossible)
                {
                    if (progressQuests[i][j].QuestVisitPlanet != null && progressQuests[i][j].QuestVisitPlanet.ID == GameManager.instance.currentPlanet.ID)
                        progressQuests[i][j].CurrentCount++;
                }
            }
        }
    }

    // ����Ʈ ���� Ȯ�� - ������ ȹ��
    public void CheckQuestItem(ItemInfo item, int count)
    {
        for(int i = 0; i < progressQuests.Count; i++)
        {
            for(int j = 0; j < progressQuests[i].Count; j++)
            {
                if(progressQuests[i][j].QuestType == QuestData.Type.Item && !progressQuests[i][j].CompletePossible)
                {
                    if (progressQuests[i][j].QuestGetItem != null && progressQuests[i][j].QuestGetItem.itemID == item.itemID)
                        progressQuests[i][j].CurrentCount += count;
                }
            }
        }
    }

    // ����Ʈ ���� Ȯ�� - �� óġ
    public void CheckQuestEnemy()
    {
        for(int i = 0; i < progressQuests.Count; i++)
        {
            for(int j = 0; j < progressQuests[i].Count; j++)
            {
                if(progressQuests[i][j].QuestType == QuestData.Type.Enemy && !progressQuests[i][j].CompletePossible)
                {
                    if (progressQuests[i][j].QuestVisitPlanet != null && progressQuests[i][j].QuestVisitPlanet.ID == GameManager.instance.currentPlanet.ID)
                        progressQuests[i][j].CurrentCount++;
                }
            }
        }
    }

    // ����Ʈ �Ϸ�
    public void CompleteQuest(QuestData quest)
    {
        if(quest.QuestType == QuestData.Type.Item)
        {
            // ȹ���� ������ �Ұ� ó��
            GameManager.instance.DeleteItem(quest.QuestGetItem, quest.MaxCount);
        }

        // ���� ó��
        if (quest.RewardExp > 0) GameManager.instance.AddExp(quest.RewardExp);
        if (quest.RewardGold > 0) GameManager.instance.AddGold(quest.RewardGold);
        if (quest.RewardItems.Count > 0)
        {
            for (int i = 0; i < quest.RewardItems.Count; i++)
            {
                GameManager.instance.AddItem(quest.RewardItems[i], quest.RewardItemsCount[i]);
            }
        }

        // ��Ͽ��� ����
        RemoveQuest(quest);
    }
}
