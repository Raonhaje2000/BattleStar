using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI positionText; // 위치 텍스트

    string planet;   // 현재 행성
    string position; // 세부 위치

    private void Awake()
    {
        // 관련 리소스 불러오기
        LoadResources();
    }

    void Start()
    {
        // 초기화
        Initialize();
    }

    // 관련 리소스 불러오기
    public void LoadResources()
    {
        positionText = transform.Find("PositionBackground/PositionText").GetComponent<TextMeshProUGUI>();
    }

    // 초기화
    public void Initialize()
    {
        planet = "이름 없는 행성";
        position = "";

        // 행성 위치 세팅
        SetPlanet();
    }

    // 행성 위치 세팅
    public void SetPlanet()
    {
        if (GameManager.instance.currentPlanet != null && GameManager.instance.currentPlanet.name != "")
        {
            // 현재 위치한 행성 데이터가 존재 하는 경우
            // 해당 행성 이름으로 저장
            this.planet = GameManager.instance.currentPlanet.planetName;
            this.position = "";
        }
        else
        {
            // 현재 위치한 행성 데이터가 존재 하지 않는 경우
            // 디폴트 값으로 저장
            this.planet = "이름 없는 행성";
            this.position = "";
        }

        // 텍스트 세팅
        SetText();
    }

    // 세부 위치 세팅
    public void SetPosition(string position)
    {
        // 세부 위치 값을 받아 값 저장
        this.position = position;

        // 텍스트 세팅
        SetText();
    }

    // 텍스트 세팅
    void SetText()
    {
        // 세부 위치가 없는 경우 '행성 이름', 세부 위치가 있는 경우 '행성 이름 (세부 위치)' 형식으로 텍스트 변경
        if (position == "") positionText.text = planet;
        else positionText.text = string.Format("{0} ({1})", planet, position);
    }
}
