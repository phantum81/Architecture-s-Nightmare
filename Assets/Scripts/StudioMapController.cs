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
        
        switch (_stage)
        {
            case EGameStage.Tutorial:
                tutorialDirecter.Play();

                break;
            case EGameStage.FirstMap:
                firstMapDirecter.Play();
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
