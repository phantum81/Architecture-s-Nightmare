using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MaxMapController : MonoBehaviour
{
    [Header("힌트 맵"), SerializeField]
    private Transform hintMap;


    [Header("부서지는 건물 위치"), SerializeField]
    private List<Transform> brokenSpotList;
    [Header("부서지는 건물 프리펩"), SerializeField]
    private GameObject brokenBuildingPrefab;
    [Header("잔해 프리펩"), SerializeField]
    private WreckObject wreckPrefab;
    private ObjectPool<WreckObject> wreckPool;

    [Header("플레이어"), SerializeField]
    private Transform player;

    [Header("플레이어 위치더미"), SerializeField]
    private Transform playerDummy;

    [Header("디졸브 머터리얼"), SerializeField]
    private List<Material> materialList ;

    [Header("지진 임펄스소스"), SerializeField]
    private CinemachineImpulseSource shakeImpulseSource;


    [Header("건물 부모"), SerializeField]
    private Transform buildingParent;

    private void Awake()
    {
        
    }

    void Start()
    {
        
        SetBrokenBuilding();

        wreckPool = new ObjectPool<WreckObject>
            (
                createFunc: () => Instantiate(wreckPrefab),
                actionOnGet: wreck => wreck.SetOn(),
                actionOnRelease: wreck => wreck.SetOff(),
                actionOnDestroy: wreck => Destroy(wreck),
                defaultCapacity: 10,
                maxSize: 20
            ) ;

        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    StartCoroutine(DissolveMaxMap());
        //}
        PlayerTransformHint();
    }

    private void SetBrokenBuilding()
    {
        Quaternion[] rotations =
        {
            Quaternion.Euler(0, 0f, 0f),
            Quaternion.Euler(0f, -90f, 0f),
            Quaternion.Euler(0f, 180f, 0f),
            Quaternion.Euler(0f, 90f, 0f)

        };

        for (int i = 0; i < brokenSpotList.Count; i++)
        {
            for (int j = 0; j < brokenSpotList[i].childCount; j++)
            {
                Instantiate(brokenBuildingPrefab, brokenSpotList[i].GetChild(j).position, rotations[i], buildingParent);
            }
        }
    }

    private void PlayerTransformHint()
    {
        if(Physics.Raycast(player.transform.position, Vector3.up,out RaycastHit hit , 100f, 1<<9))
        {
            playerDummy.gameObject.SetActive(true);  
            playerDummy.position = hit.point;
        }
        else
        {
            playerDummy.gameObject.SetActive(false);

        }
    }


    public void SpawnWreck(Vector3 _pos)
    {
        WreckObject wrec = wreckPool.Get();
        
        wrec.transform.position = _pos;
        
    }

    public void DeSpawnWreck(WreckObject _wreck)
    {
        wreckPool.Release(_wreck);
    }

    public void DissolveMap()
    {
        StartCoroutine(DissolveMaxMap());
    }

    public void OnEnable()
    {
        EventBus.SubscribeAction<Vector3>(EEventType.BrokeBuilding, SpawnWreck);
        EventBus.SubscribeAction<WreckObject>(EEventType.ReleaseBuilding, DeSpawnWreck);
        
    }
    public void OnDisable()
    {
        EventBus.UnsubscribeAction<Vector3>(EEventType.BrokeBuilding, SpawnWreck);
        EventBus.UnsubscribeAction<WreckObject>(EEventType.ReleaseBuilding, DeSpawnWreck);
        
    }



    private IEnumerator DissolveMaxMap()
    {
        float value = 0f;
        while(value < 1f)
        {
            value += Time.deltaTime / 3f;
            foreach(var m in materialList)
            {
                m.SetFloat("_Dissolve", value);
            }
            yield return null;
        }
        foreach (var m in materialList)
        {
            m.SetFloat("_Dissolve", 0f);
        }

        buildingParent.gameObject.SetActive(false);
        //hintMap.gameObject.SetActive(false);

    }



    public void ShakeFpsCamera()
    {
        EventBus.TriggerEventAction(EEventType.ShakeFpsCamera, shakeImpulseSource);
    }

    public void SetBuilding()
    {
        buildingParent.gameObject.SetActive(true);
    }

}
