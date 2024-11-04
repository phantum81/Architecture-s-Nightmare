using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMiniGameView : MonoBehaviour
{
    [Header("미니게임 판넬"), SerializeField]
    private GameObject miniGamePanel;
    [Header("미니게임 리셋버튼"), SerializeField]
    private Button resetBtn;
    [Header("미니게임 종료버튼"), SerializeField]
    private Button exitBtn;



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.EgameState == EGameState.MiniGameMimMapFirst || GameManager.Instance.EgameState == EGameState.MiniGameMimMapSecond)
        {
            miniGamePanel.SetActive(true);
        }
        else
        {
            miniGamePanel.SetActive(false);
        }
    }


    public void CloseMiniGamePanel()
    {
        miniGamePanel.SetActive(false);
        GameManager.Instance.SetGameState(EGameState.Playing);
    }
}
