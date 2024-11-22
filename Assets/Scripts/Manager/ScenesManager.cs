using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private Transform mimPuzzle;

    public void OnLoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
    private void OnEnable()
    {
        // 씬이 로드될 때마다 호출되는 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 씬이 언로드되기 직전에 호출
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // 씬이 언로드되기 직전에 호출
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
        if (SceneManager.GetActiveScene().name == ConstBundle.SCENE_MIM)
        {
            mimPuzzle = ResourceManager.Instance.ObjectDic[EObjectType.Puzzle][0];
        }
        if (SceneManager.GetActiveScene().name == ConstBundle.SCENE_MAX)
        {
            

        }
        if (SceneManager.GetActiveScene().name == ConstBundle.SCENE_END)
        {
            Transform building = ResourceManager.Instance.ObjectDic[EObjectType.BuildingMap][0];
            Transform hint = ResourceManager.Instance.ObjectDic[EObjectType.BuildingHintMap][0];

            mimPuzzle.gameObject.SetActive(true);;
            hint.gameObject.SetActive(true);
            building.gameObject.SetActive(true);


            EventBus.TriggerEventAction(EEventType.EndMapSetting, new Transform[3] {mimPuzzle,hint, building });



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
