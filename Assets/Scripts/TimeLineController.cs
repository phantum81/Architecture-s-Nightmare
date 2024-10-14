using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [Header("튜토리얼 시네마"),SerializeField]
    private PlayableDirector TutorialDirecter;

    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.TutorialCinema, PlayTutorial);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.TutorialCinema, PlayTutorial);
    }


    public void PlayTutorial()
    {
        TutorialDirecter.Play();
    }
}
