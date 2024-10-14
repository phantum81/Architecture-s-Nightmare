using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [Header("Ʃ�丮�� �ó׸�"),SerializeField]
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
