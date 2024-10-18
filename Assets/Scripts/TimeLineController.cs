using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [Header("튜토리얼 시네마"),SerializeField]
    private PlayableDirector tutorialDirecter;
    [Header("첫째맵 시네마"), SerializeField]
    private PlayableDirector firstMapDirecter;
    [Header("스튜디오에서 첫째맵 시네마"), SerializeField]
    private PlayableDirector hubToFirstMapDirecter;
    [Header("스튜디오에서 작아지는 방 시네마"), SerializeField]
    private PlayableDirector hubToMinMapDirecter;
    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.TutorialCinema, PlayTutorial);
        EventBus.SubscribeAction(EEventType.FirstMapCinema, PlayFirstMap);
        EventBus.SubscribeAction<EObjectType>(EEventType.StudioToAnotherScene, PlayStudioToAnotherScene);

    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.TutorialCinema, PlayTutorial);
        EventBus.UnsubscribeAction(EEventType.FirstMapCinema, PlayFirstMap);
        EventBus.UnsubscribeAction<EObjectType>(EEventType.StudioToAnotherScene, PlayStudioToAnotherScene);

    }


    private void PlayTutorial()
    {
        tutorialDirecter.Play();
    }
    
    private void PlayFirstMap()
    {
        firstMapDirecter.Play();
    }

    private void PlayStudioToAnotherScene(EObjectType _type)
    {
        switch (_type)
        {
            case EObjectType.SleepChair:
                hubToFirstMapDirecter.Play();
                break;
        }
        
    }



}
