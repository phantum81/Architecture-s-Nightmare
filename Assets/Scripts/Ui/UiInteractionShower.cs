using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInteractionShower : MonoBehaviour
{
    [Header("상호작용 판넬"), SerializeField]
    private GameObject interactionPanel;
    [Header("상호작용 텍스트"), SerializeField]
    private TextMeshProUGUI interactionText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.OnInteraction,  OnInteraction);
        EventBus.UnsubscribeAction(EEventType.OffInteraction, OffInteraction);
        EventBus.UnsubscribeAction<EInteractionType>(EEventType.OnInteraction, InteractionTextChange);

    }

    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.OnInteraction,OnInteraction);
        EventBus.SubscribeAction(EEventType.OffInteraction,OffInteraction);
        EventBus.SubscribeAction<EInteractionType>(EEventType.OnInteraction, InteractionTextChange);
    }
    private void OnInteraction()
    {
        interactionPanel.SetActive(true);
    }

    private void OffInteraction()
    {
        interactionPanel.SetActive(false);
    }

    private void InteractionTextChange(EInteractionType[] _type)
    {

        string maintain = string.Empty;
        if (_type.Length > 0)
        {
            EInteractionType interactionType = _type[0]; // 첫 번째 매개변수를 사용

            switch (interactionType)
            {
                case EInteractionType.Pick:
                    maintain = ConstBundle.INTERACTION_PICK;
                    break;
                case EInteractionType.Push:
                    maintain = ConstBundle.INTERACTION_PUSH;
                    break;
                case EInteractionType.Look:
                    maintain = ConstBundle.INTERACTION_LOOK;
                    break;
                case EInteractionType.Destroy:
                    maintain = ConstBundle.INTERACTION_DESTROY;
                    break;
            }
        }
        interactionText.text = maintain; // UI 업데이트
    }

}
