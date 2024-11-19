using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MaxMapController : MonoBehaviour
{
    /// <summary>
    /// 동 북 서 남
    /// </summary>
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
                Instantiate(brokenBuildingPrefab, brokenSpotList[i].GetChild(j).position, rotations[i]);
            }
        }
    }
    private IEnumerator CoSetBrokenBuilding()
    {
        Quaternion[] rotations =
        {
        Quaternion.Euler(-90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, -90f),
        Quaternion.Euler(-90f, 0f, 180f),
        Quaternion.Euler(-90f, 0f, 90f)
    };

        for (int i = 0; i < brokenSpotList.Count; i++)
        {
            for (int j = 0; j < brokenSpotList[i].childCount; j++)
            {
                Instantiate(brokenBuildingPrefab, brokenSpotList[i].GetChild(j).position, rotations[i]);
                if(i == 1)
                {
                    Debug.Log("1");
                }
                if ((i * brokenSpotList[i].childCount + j) % 10 == 0)  // 10개 생성 후 한 프레임 쉬기
                {
                    yield return null;

                }
                yield return null;
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



    public void OnEnable()
    {
        EventBus.SubscribeAction<Vector3>(EEventType.BrokeBuilding, SpawnWreck);
        EventBus.SubscribeAction<WreckObject>(EEventType.ReleaseBuilding, DeSpawnWreck);
    }
    public void OnDisable()
    {
        EventBus.UnsubscribeAction<Vector3>(EEventType.BrokeBuilding, SpawnWreck);
        EventBus.SubscribeAction<WreckObject>(EEventType.ReleaseBuilding, DeSpawnWreck);
    }
}
