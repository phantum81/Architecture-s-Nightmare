using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCinemaController : MonoBehaviour
{
    [Header("��ŷ��ġ"),SerializeField]
    private Transform[] rankPoints = new Transform[2];


    private void TranslateToRankPoint(EObjectType _type)
    {
        Transform[] targets = new Transform[2];
        targets[0] = ResourceManager.Instance.ObjectDic[EObjectType.WineBottle];
        targets[1] = ResourceManager.Instance.ObjectDic[EObjectType.HuinDung];
        switch(_type)
        {
            case EObjectType.WineBottle:
                targets[0].transform.position = rankPoints[0].transform.position;
                ResourceManager.Instance.ObjectDic[EObjectType.HuinDung].transform.position = rankPoints[1].transform.position;
                break;
            case EObjectType.HuinDung:
                targets[1].transform.position = rankPoints[0].transform.position;
                ResourceManager.Instance.ObjectDic[EObjectType.WineBottle].transform.position = rankPoints[1].transform.position;
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