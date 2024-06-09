using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountElement : MonoBehaviour
{
    CanvasGroup canvasGroup;                     // 캔버스 그룹 컴포넌트

    [SerializeField] TextMeshProUGUI amountText; // 양 텍스트

    int amount;                                  // 양

    float faidInTime;                            // 페이드 인 시간
    float faidOutTime;                           // 페이드 아웃 시간

    float idleTime;                              // 대기 시간

    [SerializeField] float currentFaidTime;      // 현재 페이드 시간
    [SerializeField] float currentIdleTime;      // 현재 대기 시간


    [SerializeField] bool isFaidIn;              // 페이드 인 플래그
    [SerializeField] bool isFaidOut;             // 페이드 아웃 플래그
    [SerializeField] bool isIdle;                // 대기 플래그

    private void Awake()
    {
        // 캔버스 그룹 컴포넌트 불러오기
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // 초기화
        amount = 0;

        currentFaidTime = 0.0f;
        currentIdleTime = 0.0f;

        canvasGroup.alpha = 0.0f;
    }

    // 양 UI 요소 세팅
    public void SetActiveAmountElement()
    {
        amount = 0;

        currentFaidTime = 0.0f;
        currentIdleTime = 0.0f;

        isFaidIn = false;
        isFaidOut = false;
        isIdle = false;

        // 오브젝트 활성화
        gameObject.SetActive(true);

        // 페이드 인 시작
        StartCoroutine(FaidIn());
    }

    // 양 세팅
    public void SetAmount(int amount)
    {
        if(isFaidOut)
        {
            // 페이드 아웃 중인 경우
            // 페이드 아웃 중지 후 양 UI 요소 재세팅

            StopCoroutine("FaidOut");
            isFaidOut = false;

            SetActiveAmountElement();
        }
        else if(isIdle)
        {
            // 대기 중인 경우
            // 대기 시간 연장
            ExtendIdleTime();
        }

        // 현재 양에 누적 후 텍스트 변경
        this.amount += amount;
        amountText.text = this.amount.ToString();
    }

    // 시간 값 세팅
    public void SetTimeValues(float faidInTime, float faidOutTime, float idleTime)
    {
        this.faidInTime = faidInTime;
        this.faidOutTime = faidOutTime;
        this.idleTime = idleTime;
    }

    // 대기 시간 연장
    void ExtendIdleTime()
    {
        currentIdleTime = 0.0f;
    }

    // 페이드 인 처리
    IEnumerator FaidIn()
    {
        if (!isFaidIn)
        {
            isFaidIn = true;

            // 시간 및 투명도 값 초기화
            currentFaidTime = 0.0f;
            canvasGroup.alpha = 0.0f;

            // 현재 페이드 시간이 페이드 인 시간 보다 적은 경우
            while (currentFaidTime < faidInTime && isFaidIn)
            {
                // 시간 비율에 맞춰 투명도 조절
                if (faidInTime != 0) canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, currentFaidTime / faidInTime);

                yield return null;

                currentFaidTime += Time.deltaTime;
            }

            // 투명도 값 고정
            canvasGroup.alpha = 1.0f;

            // 대기 시작
            StartCoroutine(WaitIdle());
            StopCoroutine("FaidIn");

            isFaidIn = false;
        }
    }

    // 대기 처리
    IEnumerator WaitIdle()
    {
        if (!isIdle)
        {
            isIdle = true;

            // 시간 및 투명도 값 초기화
            currentIdleTime = 0.0f;
            canvasGroup.alpha = 1.0f;

            // 현재 대기 시간이 대기 시간 보다 적은 경우 (중간에 양 변경 시 연장 처리를 위함)
            while (currentIdleTime < idleTime)
            {
                yield return null;

                currentIdleTime += Time.deltaTime;
            }

            // 페이드 아웃 시작
            StartCoroutine(FaidOut());
            StopCoroutine("WaitIdle");

            isIdle = false;
        }
    }

    // 페이드 아웃 처리
    IEnumerator FaidOut()
    {
        if (!isFaidOut)
        {
            isFaidOut = true;

            // 시간 및 투명도 값 초기화
            currentFaidTime = 0.0f;
            canvasGroup.alpha = 1.0f;

            // 현재 페이드 시간이 페이드 아웃 시간 보다 적은 경우
            while (currentFaidTime < faidOutTime && isFaidOut)
            {
                // 시간 비율에 맞춰 투명도 조절
                if (faidOutTime != 0) canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, currentFaidTime / faidOutTime);

                yield return null;

                currentFaidTime += Time.deltaTime;
            }

            // 투명도 값 고정
            canvasGroup.alpha = 0.0f;

            // 해당 오브젝트 비활성화
            StopCoroutine("FaidOut");
            gameObject.SetActive(false);

            isFaidOut = false;
        }
    }
}
