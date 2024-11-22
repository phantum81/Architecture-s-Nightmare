using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapController : MonoBehaviour
{
    [Header("MimMap ¿ÀºêÁ§Æ®À§Ä¡"), SerializeField]
    private Transform mimMap;
    [Header("MaxMap ÈùÆ®¸ÊÀ§Ä¡"), SerializeField]
    private Transform maxMapHint;
    [Header("MaxMap ºôµù¸ÊÀ§Ä¡"), SerializeField]
    private Transform maxMapBuilding;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 0:mim, 1: hint, 2: building
    /// </summary>
    /// <param name="_trans"></param>
    public void SetEachMapObject(Transform[] _trans )
    {

        _trans[0].SetPositionAndRotation(mimMap.position, mimMap.rotation);
        _trans[1].SetPositionAndRotation(maxMapHint.position, maxMapHint.rotation);
        _trans[2].SetPositionAndRotation(maxMapBuilding.position, maxMapBuilding.rotation);


        //_trans[0].SetPositionAndRotation(maxMapHint.position, maxMapHint.rotation);
        //_trans[1].SetPositionAndRotation(maxMapBuilding.position, maxMapBuilding.rotation);
    }

    public void OnEnable()
    {
        EventBus.SubscribeAction<Transform[]>(EEventType.EndMapSetting, SetEachMapObject);
    }
    public void OnDisable()
    {
        EventBus.UnsubscribeAction<Transform[]>(EEventType.EndMapSetting, SetEachMapObject);
    }
}
