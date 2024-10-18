using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptsData", menuName = "ScriptableData/PlayerScriptsData", order = 1)]
public class GamePlayerScriptsData: ScriptableObject
{

    [Header("캐릭터 이름"), SerializeField]
    private string characterName;
    [Header("키, 대사"), SerializeField]
    private List<ScriptsEntry> scriptsEntries= new List<ScriptsEntry>();



    private Dictionary<EPlayerScriptsType, string> _playerScriptsDic = new Dictionary<EPlayerScriptsType, string>();
    public Dictionary<EPlayerScriptsType, string> PlayerScriptsDic => _playerScriptsDic;


    public void Init()
    {
        for(int i=0; i<scriptsEntries.Count; i++)
        {
            _playerScriptsDic.Add(scriptsEntries[i].key, scriptsEntries[i].sentences);
        }
    }




}
