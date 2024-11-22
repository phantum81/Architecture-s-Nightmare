using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [Header("Ʃ�丮�� �ó׸�"),SerializeField]
    private PlayableDirector tutorialDirecter;
    [Header("ù°�� �ó׸�"), SerializeField]
    private PlayableDirector firstMapDirecter;
    [Header("�۾����� �� �ó׸�"), SerializeField]
    private PlayableDirector mimMapDirecter;
    [Header("Ŀ���� �� �ó׸�"), SerializeField]
    private PlayableDirector maxMapDirecter;
    [Header("��Ʃ������� ù°�� �ó׸�"), SerializeField]
    private PlayableDirector hubToFirstMapDirecter;
    [Header("��Ʃ������� ���� �۾����� �� �ó׸�"), SerializeField]
    private PlayableDirector hubToMinMapDirecter;
    [Header("��Ʃ������� ���� Ŀ���� �� �ó׸�"), SerializeField]
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
