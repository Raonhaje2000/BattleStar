using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationUI : MonoBehaviour
{
    [SerializeField] Slider hpBar;              // 체력바
    [SerializeField] Slider spBar;              // 스테미너바

    [SerializeField] Image expImage;            // 경험치 이미지
    [SerializeField] TextMeshProUGUI levelText; // 레벨 텍스트

    // 소지한 재화
    [SerializeField] TextMeshProUGUI gemText;   // 소지한 보석 텍스트
    [SerializeField] TextMeshProUGUI goldText;  // 소지한 골드 텍스트

    void Start()
    {
        SetHpBar();       // 체력바 세팅
        SetSpBar();       // 스테미너바 세팅
        SetLevelAndExp(); // 레벨 및 경험치 세팅
        SetMoney();       // 재화 세팅
    }

    void Update()
    {
        SetHpBar();       // 체력바 세팅
        SetSpBar();       // 스테미너바 세팅
        SetLevelAndExp(); // 레벨 및 경험치 세팅
        SetMoney();       // 재화 세팅
    }

    // 체력바 세팅
    public void SetHpBar()
    {
        // GameManager의 플레이어 최대 체력과 현재 체력 데이터를 받아와서 체력바 세팅
        hpBar.minValue = 0;
        hpBar.maxValue = GameManager.instance.currentPlayerData.maxHp;
        hpBar.value = GameManager.instance.currentPlayerData.hp;
    }

    public void SetSpBar()
    {
        // GameManager의 플레이어 최대 스테미너와 현재 스테이머 데이터를 받아와서 스테미너바 세팅
        spBar.minValue = 0;
        spBar.maxValue = GameManager.instance.currentPlayerData.maxSp;
        spBar.value = GameManager.instance.currentPlayerData.sp;
    }

    // 레벨 및 경험치 세팅
    public void SetLevelAndExp()
    {
        // GameManager의 플레이어 레벨 데이터를 받아와서 레벨 텍스트 세팅
        levelText.text = GameManager.instance.currentPlayerData.level.ToString();

        // GameManager의 플레이어 현재 경험치와 최대 경험치 데이터를 받아와서 해당 비율만큼 경험치 이미지가 채워진 비율 세팅
        expImage.fillAmount = GameManager.instance.currentPlayerData.exp / GameManager.instance.currentPlayerData.maxExp;
    }

    // 재화 세팅
    public void SetMoney()
    {
        // GameManager의 플레이어 소지 보석과 골드 데이터를 받아와서 보석 텍스트와 골드 텍스트 각각 세팅
        gemText.text = GameManager.instance.currentPlayerData.gem.ToString();
        goldText.text = GameManager.instance.currentPlayerData.money.ToString();
    }
}
