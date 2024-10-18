using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStartController : MonoBehaviour
{
    [Header("∞À¿∫ ∆«"), SerializeField]
    private GameObject blackPanel;
    [Header("»Ú ∆«"), SerializeField]
    private GameObject whitePanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPanelOff()
    {
        blackPanel.SetActive(false);
        whitePanel.SetActive(false);
    }


    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.SceneStart, SetPanelOff);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.SceneStart, SetPanelOff);
    }
}
