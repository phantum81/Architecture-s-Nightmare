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
                break;
            case EGameStage.MinMap:
                MimMapDirecter.Play();
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
