using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RADAR_Point : MonoBehaviour
{
    Image pointImage;
    Color pointColor;

    float faidInTime;
    float time;

    private void Awake()
    {
        pointImage = GetComponent<Image>();
        pointColor = pointImage.color;
    }

    void Start()
    {
        faidInTime = 0.1f;
        time = 0.0f;

        pointImage.color = new Color(pointColor.r, pointColor.g, pointColor.b, 0);

        StartCoroutine(WaitFaidIn());
    }

    IEnumerator WaitFaidIn()
    {
        while (time < faidInTime)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, time / faidInTime);
            pointImage.color = new Color(pointColor.r, pointColor.g, pointColor.b, alpha / 255.0f);

            yield return null;

            time += Time.deltaTime;
        }

        pointImage.color = new Color(pointColor.r, pointColor.g, pointColor.b, 1);
    }
}
