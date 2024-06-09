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

        // 해당 행성을 방문한 적이 없는 경우 행성의 퀘스트 받아오기
        AddPlanetQuests(GameManager.instance.currentPlanet);
    }

    public void OnlyTest()
    {
        // *** 테스트용 코드 *** //
        List<PlanetInfo> testPlanet = new List<PlanetInfo>();

        testPlanet.Add(Resources.Load<PlanetInfo>("ItemData/PlanetData/Mercury"));
        testPlanet.Add(Resources.Load<PlanetInfo>("ItemData/PlanetData/Sun"));

        for (int i = 0; i < testPlanet.Count; i++)
        {
            AddPlanetQuests(testPlanet[i]);
            testPlanet[i].visit = false;
        }
    }

    // 게임 시작 시 초기화 용 코드
    public void ResetQuests()
    {
        PlanetInfo[] planets = Resources.LoadAll<PlanetInfo>("ItemData/PlanetData/Planet");

        for(int i = 0; i < planets.Length; i++)
        {
            // 퀘스트 등록
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

    // 행성 퀘스트 추가
    public void AddPlanetQuests(PlanetInfo planet)
    {
        // 행성에 방문한 적이 없는 경우 (첫 방문인 경우)
        if(!planet.visit && planet.planetQuests != null && planet.planetQuests.Count > 0)
        {
            progressQuests.Add(planet.planetQuests); // 해당 행성의 퀘스트 목록에 추가
            newQuestCount = planet.planetQuests.Count;
            
            planet.visit = true; // 행성 방문 표시

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

    // 해당 퀘스트의 목록 상에서의 인덱스 찾기
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

    // 퀘스트 제거하기
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

    // 체크된 퀘스트 목록 찾기
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

                // 체크된 퀘스트 목록을 다 찾았을 경우 (최대 체크 수만큼 찾은 경우)
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

                // 최대 체크 수만큼 찾은 경우
                if (count == questCheckMaxCount) break;
            }
        }

        return count;
    }

    // 퀘스트 조건 확인 - 행성 방문
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

    // 퀘스트 조건 확인 - 아이템 획득
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

    // 퀘스트 조건 확인 - 적 처치
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

    // 퀘스트 완료
    public void CompleteQuest(QuestData quest)
    {
        if(quest.QuestType == QuestData.Type.Item)
        {
            // 획득한 아이템 소거 처리
            GameManager.instance.DeleteItem(quest.QuestGetItem, quest.MaxCount);
        }

        // 보상 처리
        if (quest.RewardExp > 0) GameManager.instance.AddExp(quest.RewardExp);
        if (quest.RewardGold > 0) GameManager.instance.AddGold(quest.RewardGold);
        if (quest.RewardItems.Count > 0)
        {
            for (int i = 0; i < quest.RewardItems.Count; i++)
            {
                GameManager.instance.AddItem(quest.RewardItems[i], quest.RewardItemsCount[i]);
            }
        }

        // 목록에서 제거
        RemoveQuest(quest);
    }
}
