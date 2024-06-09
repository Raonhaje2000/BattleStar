using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "퀘스트")]
public class QuestData : ScriptableObject
{
    public const int REWARD_ITEM_MAX_COUNT = 3;

    public enum Type
    { 
        Planet = 0, // 행성 방문
        Item,       // 아이템 획득
        Enemy       // 적 처치
    }

    [Header("퀘스트 정보")]
    [SerializeField] int questId;                // 퀘스트 ID
    [SerializeField] string questTitle;          // 퀘스트 제목

    [Header("퀘스트 내용")]
    [SerializeField] Sprite questPlanetIcon;     // 퀘스트 행성 아이콘
    [SerializeField] string questPlanetName;     // 퀘스트 행성 이름

    [SerializeField] Sprite questNpcImage;       // 퀘스트 NPC 이미지
    [SerializeField] string questNpcName;        // 퀘스트 NPC 이름

    [SerializeField] Type questType;              // 퀘스트 유형
    [SerializeField] PlanetInfo questVisitPlanet; // 퀘스트 방문 행성
    [SerializeField] ItemInfo questGetItem;       // 퀘스트 획득 아이템

    [SerializeField] string questGoal;           // 퀘스트 목표
    [SerializeField] int maxCount;               // 퀘스트 완료를 위한 수량
    [SerializeField] int currentCount = 0;       // 현재 달성한 수량

    [TextArea]
    [SerializeField] string questContent;        // 퀘스트 내용

    [Header("퀘스트 보상")]
    [SerializeField] int rewardExp;              // 보상 경험치
    [SerializeField] int rewardGold;             // 보상 골드
    [SerializeField] List<ItemInfo> rewardItems; // 보상 아이템들
    [SerializeField] List<int> rewardItemsCount; // 보상 아이템 개수

    bool progress = false;                       // 퀘스트 진행 확인 플래그
    bool completePossible = false;               // 퀘스트 완료 가능 확인 플래그

    [SerializeField]
    bool viewCheck = false;                      // 퀘스트 체크 표시 확인 플래그

    public int QuestId
    {
        get { return questId; }
    }

    public string QuestTitle
    {
        get { return questTitle; }
    }

    public Sprite QuestPlanetIcon
    {
        get { return questPlanetIcon; }
    }

    public string QuestPlanetName
    {
        get { return questPlanetName; }
    }

    public Sprite QuestNpcImage
    {
        get { return questNpcImage; }
    }

    public string QuestNpcName
    {
        get { return questNpcName; }
    }

    public Type QuestType
    { 
        get { return questType; }
    }

    public PlanetInfo QuestVisitPlanet
    { 
        get { return questVisitPlanet; }
    }

    public ItemInfo QuestGetItem
    {
        get { return questGetItem; }
    }

    public string QuestGoal
    {
        get { return questGoal; }
    }

    public int MaxCount
    {
        get
        {
            if (questType == Type.Planet) maxCount = 1;
            return maxCount; 
        }
    }

    public int CurrentCount
    {
        get { return currentCount; }
        set { currentCount = value; }
    }

    public string QuestContent
    {
        get { return questContent; }
    }

    public int RewardExp
    {
        get { return rewardExp; }
    }

    public int RewardGold
    {
        get { return rewardGold; }
    }

    public List<ItemInfo> RewardItems
    {
        get { return rewardItems; }
    }

    public List<int> RewardItemsCount
    {
        get { return rewardItemsCount; }
    }

    public bool Progress
    {
        get { return progress; }
        set { progress = value; }
    }

    public bool CompletePossible
    { 
        get
        {
            completePossible = (currentCount >= maxCount) ? true : false;

            return completePossible; 
        }
        set { completePossible = value; }
    }

    public bool ViewCheck
    {
        get { return viewCheck; }
        set { viewCheck = value; }
    }
}
