using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MoveButtonUI : MonoBehaviour
{
    [SerializeField] Button moveButton; // 이동 버튼

    void Start()
    {
        // 버튼 이벤트 등록
        moveButton.onClick.AddListener(ClickMoveButton);
    }

    // 이동 버튼 클릭 처리
    void ClickMoveButton()
    {
        //Debug.Log("이동 버튼 클릭");

        // 씬 중첩으로 행성 선택 버튼 씬(인덱스 3번) 불러오기
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    // 버튼 상호작용 세팅
    public void SetButtonInteractable(bool interactable)
    {
        moveButton.interactable = interactable;
    }
}
