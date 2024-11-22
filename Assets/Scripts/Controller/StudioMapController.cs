using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StudioMapController : MonoBehaviour
{
    [Header("Ʃ�丮�� �ó׸�"), SerializeField]
    private PlayableDirector tutorialDirecter;
    [Header("ù°�� �ó׸�"), SerializeField]
    private PlayableDirector firstMapDirecter;
    [Header("�̴ϸظ� �ó׸�"), SerializeField]
    private PlayableDirector MimMapDirecter;

    [Header("ù°�� ����"), SerializeField]
    private GameObject firstMapObject;
    [Header("�̴ϸظ� ����"), SerializeField]
    private GameObject minMapObject;
    [Header("�ƽ��� ����"), SerializeField]
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
