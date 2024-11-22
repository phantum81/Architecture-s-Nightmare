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
    [Header("작아지는 맵 시네마"), SerializeField]
    private PlayableDirector mimMapDirecter;
    [Header("커지는 맵 시네마"), SerializeField]
    private PlayableDirector maxMapDirecter;
    [Header("스튜디오에서 첫째맵 시네마"), SerializeField]
    private PlayableDirector hubToFirstMapDirecter;
    [Header("스튜디오에서 내가 작아지는 방 시네마"), SerializeField]
    private PlayableDirector hubToMinMapDirecter;
    [Header("스튜디오에서 내가 커지는 방 시네마"), SerializeField]
    private PlayableDirector hubToMaxMapDirecter;
    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.TutorialCinema, PlayTutorial);
        EventBus.SubscribeAction(EEventType.FirstMapCinema, PlayFirstMap);
        EventBus.SubscribeAction(EEventType.MimMapCinema, PlayMimMap);
        EventBus.SubscribeAction(EEventType.MaxMapCinema, PlayMaxMap);
        EventBus.SubscribeAction<EObjectType>(EEventType.StudioToAnotherScene, PlayStudioToAnotherScene);

    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.TutorialCinema, PlayTutorial);
        EventBus.UnsubscribeAction(EEventType.FirstMapCinema, PlayFirstMap);
        EventBus.UnsubscribeAction(EEventType.MimMapCinema, PlayMimMap);
        EventBus.UnsubscribeAction(EEventType.MaxMapCinema, PlayMaxMap);
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
    private void PlayMimMap()
    {
        mimMapDirecter.Play();
    }
    private void PlayMaxMap()
    {
        maxMapDirecter.Play();
    }


    private void PlayStudioToAnotherScene(EObjectType _type)
    {
        switch (_type)
        {
            case EObjectType.SleepChair:
                hubToFirstMapDirecter.Play();
                break;
            case EObjectType.SleepLapTop:
                hubToMinMapDirecter.Play();
                break;
            case EObjectType.SleepBed:
                hubToMaxMapDirecter.Play();
                break;

        }
        
    }



}
