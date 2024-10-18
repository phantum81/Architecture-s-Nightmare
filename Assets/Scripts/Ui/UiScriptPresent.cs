using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UiScriptPresent
{

    GamePlayerScriptsData data;
    IScriptView scriptView;
    public UiScriptPresent(IScriptView _scriptView)
    {
        scriptView = _scriptView;
        data = ResourceManager.Instance.PlayerScriptsData;
        if (data.PlayerScriptsDic.Count == 0 )
            data.Init();
    }


    public void OnScripts(EObjectType _type)
    {
        if (scriptView.GetScriptPanelActiveSelf())
        {
            scriptView.SetScriptPanel(false);
            return;
        }
        EPlayerScriptsType scriptType;
        switch (_type)
        {
            case EObjectType.DecoWinebBottle:
                scriptType = EPlayerScriptsType.StudioInteractionWineBottle;
                break;
            case EObjectType.DecoHuinDung:
                scriptType = EPlayerScriptsType.StudioInteractionHuindung;
                break;
            case EObjectType.Saboa:
                scriptType = EPlayerScriptsType.StudioInteractionSaboa;
                break;

            default:
                return;
        }
        scriptView.SetScriptPanel(true);
        scriptView.WriteScript(data.PlayerScriptsDic[scriptType]);
        


    }


    public void SetOffScriptPanel()
    {
        scriptView.SetScriptPanel(false);
    }


    public void CinemaScripts(EPlayerScriptsType _type)
    {
        scriptView.WriteScript(data.PlayerScriptsDic[_type]);
    }


}
