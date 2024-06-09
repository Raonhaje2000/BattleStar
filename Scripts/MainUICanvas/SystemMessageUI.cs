using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    CanvasGroup canvasGroup; // 캔버스 그룹 컴포넌트

    float idleTime;          // 대기 시간
    float faidOutTime;       // 페이드 아웃 시간

    float time;              // 현재 시간

    bool isIdle;             // 대기 플래그
    bool isFaidOut;          // 페이드 아웃 플래그
 
    [SerializeField] TextMeshProUGUI systemMessageText; // 시스템 메세지 텍스트

    private void Awake()
    {
        // 캔버스 그룹 컴포넌트 불러오기
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // 초기화
        idleTime = 5.0f;
        faidOutTime = 1.0f;

        time = 0.0f;

        canvasGroup.alpha = 1.0f;

        // 대기 시작
        StartCoroutine(WaitIdle());
    }

    // 시스템 메세지 텍스트 세팅
    public void SetSystemMessage(string message)
    {
        systemMessageText.text = "[System] " + message;
    }

    // 대기 처리
    IEnumerator WaitIdle()
    {
        if (!isIdle)
        {
            isIdle = true;

            // 대기 시간 동안 대기
            yield return new WaitForSeconds(idleTime);

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
            time = 0.0f;
            canvasGroup.alpha = 1.0f;

            // 현재 시간이 페이드 아웃 시간 보다 적은 경우
            while (time < faidOutTime)
            {
                // 시간 비율에 맞춰 투명도 조절
                if (faidOutTime != 0) canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time / faidOutTime);

                yield return null;

                time += Time.deltaTime;
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
