using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject _go = GameObject.Find("ResourceManager");
                if (_go == null)
                {
                    _instance = _go.AddComponent<ResourceManager>();

                }
                if (_instance == null)
                {
                    _instance = _go.GetComponent<ResourceManager>();
                }
            }
            return _instance;

        }
    }
     
    private Dictionary<EObjectType, Transform> _objectDic = new Dictionary<EObjectType, Transform>();
    public Dictionary<EObjectType, Transform> ObjectDic => _objectDic;


    private void Awake()
    {
        LoadObjectDictionary();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void LoadObjectDictionary()
    {
        List<IObject> objList = FindAllObjectsImplementingInterface<IObject>();

        foreach (var obj in objList)
        {
            EObjectType type = obj.GetObjectType(); // EobjectType 가져오기
            obj.Init(); //초기화도 진행
            Transform transform = (obj as Transform).transform; // Transform 가져오기

            // Dictionary에 추가
            if (!_objectDic.ContainsKey(type))
            {
                _objectDic[type] = transform;
            }
            else
            {
                Debug.LogWarning($"Duplicate entry for {type}. Object already exists in the dictionary.");
            }
        }
    }
    private List<T> FindAllObjectsImplementingInterface<T>() where T : class
    {
        List<T> foundObjects = new List<T>();
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (var obj in allObjects)
        {
            if (obj.TryGetComponent<T>(out var component))
            {
                foundObjects.Add(component);
            }
        }

        return foundObjects;
    }


}
