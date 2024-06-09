using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject shipPosition;

    void Start()
    {
        CreateShip();
    }

    void CreateShip()
    {
        if (GameManager.instance.currentPlayerData.currentShip != null && GameManager.instance.currentPlayerData.currentShip.shipModel != null)
        {
            GameObject shipObject = Instantiate(GameManager.instance.currentPlayerData.currentShip.shipModel, shipPosition.transform.position, shipPosition.transform.rotation);
            shipObject.transform.parent = shipPosition.transform;

            shipObject.GetComponent<Ship_01Script>().shipState = SHIPSTATE.WARP;
        }
    }

    public void ClickStartButton()
    {
        Debug.Log("게임 시작 버튼 클릭");

        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    public void ClickQuitButton()
    {
        Debug.Log("게임 종료 버튼 클릭");

        Application.Quit();
    }
}
