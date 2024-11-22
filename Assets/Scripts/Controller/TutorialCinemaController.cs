using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCinemaController : MonoBehaviour
{
    [Header("·©Å·À§Ä¡"),SerializeField]
    private Transform[] rankPoints = new Transform[2];


    private void TranslateToRankPoint(EObjectType _type)
    {
        Transform[] targets = new Transform[2];
        targets[0] = ResourceManager.Instance.ObjectDic[EObjectType.WineBottle][0];
        targets[1] = ResourceManager.Instance.ObjectDic[EObjectType.HuinDung][0];
        switch(_type)
        {
            case EObjectType.WineBottle:
                targets[0].transform.position = rankPoints[0].transform.position;
                targets[1].transform.position = rankPoints[1].transform.position;
                break;
            case EObjectType.HuinDung:
                targets[1].transform.position = rankPoints[0].transform.position;
                targets[0].transform.position = rankPoints[1].transform.position;
                break;

            default:
                break;
        }
    }

    
    


    private void OnEnable()
    {
        EventBus.SubscribeAction<EObjectType>(EEventType.TutorialCinema, TranslateToRankPoint);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction<EObjectType>(EEventType.TutorialCinema, TranslateToRankPoint);
    }
}
