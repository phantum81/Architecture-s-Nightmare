using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{


    public void OnLoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
    private void OnEnable()
    {
        // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
        // ���� ��ε�Ǳ� ������ ȣ��
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // ���� ��ε�Ǳ� ������ ȣ��
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        if(SceneManager.GetActiveScene().name != ConstBundle.SCENE_TUTORIAL)
        {
            GameManager.Instance.SceneStart();
        }

        if (SceneManager.GetActiveScene().name == ConstBundle.SCENE_FIRST)
        {
            

        }
        if(SceneManager.GetActiveScene().name == ConstBundle.SCENE_STUDIO)
        {
            
            EventBus.TriggerEventAction(EEventType.StudioEnter, GameManager.Instance.EgameStage);
            GameManager.Instance.SetGameStage();
        }



    }


    private void OnSceneUnloaded(Scene _current)
    {
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {

        }
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            
        }
    }
}
