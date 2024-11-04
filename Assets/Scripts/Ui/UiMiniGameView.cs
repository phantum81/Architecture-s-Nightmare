using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMiniGameView : MonoBehaviour
{
    [Header("�̴ϰ��� �ǳ�"), SerializeField]
    private GameObject miniGamePanel;
    [Header("�̴ϰ��� ���¹�ư"), SerializeField]
    private Button resetBtn;
    [Header("�̴ϰ��� �����ư"), SerializeField]
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
