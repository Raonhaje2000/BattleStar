using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RADAR : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] GameObject rotation;

    [SerializeField] GameObject pointsParent;
    GameObject pointPrefab;
    List<GameObject> points;

    [SerializeField] Transform pointPosition;
    Vector3 initLocalPosition;

    [SerializeField] int pointCount;

    float rotationSpeed;
    float pointFaidTime;
    float randomFaidTimeRange;
    int randomPointPositionRange;

    private void Awake()
    {
        pointPrefab = Resources.Load<GameObject>("Prefabs/UI/RADAR_Point");
        points = new List<GameObject>();

        initLocalPosition = pointPosition.transform.localPosition;
    }

    void Start()
    {
        pointCount = 5;

        rotationSpeed = 120.0f;
        pointFaidTime = (360.0f / rotationSpeed) / pointCount;
        randomFaidTimeRange = pointFaidTime / 2.0f;
        randomPointPositionRange = Mathf.RoundToInt(pointsParent.GetComponent<RectTransform>().rect.height / 2.0f - 5.0f);

        StartCoroutine(FaidInPoints());
    }

    void Update()
    {
        rotation.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    void CreatePoint()
    {
        GameObject newPoint = Instantiate(pointPrefab, pointsParent.transform);

        int positionY = Random.Range(0, randomPointPositionRange);
        pointPosition.transform.localPosition += new Vector3(0, positionY, 0);
        newPoint.transform.position = pointPosition.transform.position;

        points.Add(newPoint);

        pointPosition.transform.localPosition = initLocalPosition;
    }

    IEnumerator FaidInPoints()
    {
        for(int i = 0; i < pointCount; i++)
        {
            float time = pointFaidTime + Random.Range(-randomFaidTimeRange, randomFaidTimeRange);

            CreatePoint();

            yield return new WaitForSeconds(time);
        }
    }
}
