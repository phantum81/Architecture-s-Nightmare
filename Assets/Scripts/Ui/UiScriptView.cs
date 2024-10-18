using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScriptView : MonoBehaviour, IScriptView
{
    [Header("대사 판넬"), SerializeField]
    private GameObject scriptPanel;
    [Header("대사 텍스트"), SerializeField]
    private TextMeshProUGUI scriptTxt;

    UiScriptPresent present;
    Coroutine curCoroutine;
    void Awake()
    {
        present = new UiScriptPresent(this);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    #region 인터페이스함수
    public void WriteScript(string _script)
    {
        scriptTxt.text = _script;
    }
    public void SetScriptPanel(bool _bol)
    {
        scriptPanel.SetActive(_bol);
    }

    public bool GetScriptPanelActiveSelf()
    {
        return scriptPanel.activeSelf;
    }


    public void InvisibleScriptsPanel(float _time, float _wait)
    {
        if(curCoroutine == null)
            curCoroutine = StartCoroutine(UiManager.Instance.SetUiInvisible(scriptPanel.transform, _time, _wait));
    }
    #endregion



    #region 시네마 대사 타임라인 시그널용 함수
    public void CinemaStudioToFirstMapScripts ()
    {
        present.CinemaScripts(EPlayerScriptsType.StudioToFirstMap);
    }

    public void CinemaStudioToMimMapScripts()
    {
        present.CinemaScripts(EPlayerScriptsType.StudioToMimMap);
    }


    #endregion


    private void OnEnable()
    {
        EventBus.SubscribeAction<EObjectType>(EEventType.StudioDecoInteraction, present.OnScripts);
        EventBus.SubscribeAction(EEventType.OffInteraction, present.SetOffScriptPanel);
       
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction<EObjectType>(EEventType.StudioDecoInteraction, present.OnScripts);
        EventBus.UnsubscribeAction(EEventType.OffInteraction, present.SetOffScriptPanel);
        
    }

}
