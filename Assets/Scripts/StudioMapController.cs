using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StudioMapController : MonoBehaviour
{
    [Header("Æ©Åä¸®¾ó ½Ã³×¸¶"), SerializeField]
    private PlayableDirector tutorialDirecter;
    [Header("Ã¹Â°¸Ê ½Ã³×¸¶"), SerializeField]
    private PlayableDirector firstMapDirecter;
    [Header("¹Ì´Ï¸Ø¸Ê ½Ã³×¸¶"), SerializeField]
    private PlayableDirector MimMapDirecter;

    [Header("Ã¹Â°¸Ê ¹°°Ç"), SerializeField]
    private GameObject firstMapObject;
    [Header("¹Ì´Ï¸Ø¸Ê ¹°°Ç"), SerializeField]
    private GameObject minMapObject;
    [Header("¸Æ½º¸Ê ¹°°Ç"), SerializeField]
    private GameObject maxMapObject;


    private void Start()
    {

    }

    private void PlayTimeLineAsset(EGameStage _stage)
    {
        
        //Transform laptop = ResourceManager.Instance.ObjectDic[EObjectType.SleepLapTop][0];

        
        //SphereCollider laptopCol = chair?.GetComponent<Item>().InteractionCollider;

        switch (_stage)
        {
            case EGameStage.Tutorial:
                tutorialDirecter.Play();
                
                break;
            case EGameStage.FirstMap:
                firstMapDirecter.Play();
                Transform chair = ResourceManager.Instance.ObjectDic[EObjectType.SleepChair][0];
                SphereCollider chairCol = chair?.GetComponent<Item>().InteractionCollider;
                chairCol.enabled = false;
                firstMapObject.SetActive(true);
                break;
            case EGameStage.MinMap:
                MimMapDirecter.Play();
                firstMapObject.SetActive(true);
                minMapObject.SetActive(true);
                break;
            case EGameStage.MaxMap:

                break;

        }
    }


    private void OnEnable()
    {
        EventBus.SubscribeAction<EGameStage>(EEventType.StudioEnter, PlayTimeLineAsset);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction<EGameStage>(EEventType.StudioEnter, PlayTimeLineAsset);
    }
}
