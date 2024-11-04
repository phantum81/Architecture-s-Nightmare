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
     
    private Dictionary<EObjectType, List<Transform>> _objectDic = new Dictionary<EObjectType, List<Transform>>();
    public Dictionary<EObjectType, List<Transform>> ObjectDic => _objectDic;

    [Header("�÷��̾� ��� ������"), SerializeField]
    private GamePlayerScriptsData _playerScriptsData;
    public GamePlayerScriptsData PlayerScriptsData => _playerScriptsData;


    public void ResetDictionary()
    {
        _objectDic.Clear();
    }

    public void LoadObjectDictionary()
    {
        List<IObject> objList = FindAllObjectsImplementingInterface<IObject>();

        foreach (var obj in objList)
        {
            obj.Init(); //�ʱ�ȭ�� ����
            EObjectType type = obj.GetObjectType(); // EobjectType ��������
            
            Transform transform = obj.GetTransform(); // Transform ��������

            // Dictionary�� �߰�
            if (!_objectDic.ContainsKey(type))
            {
                List<Transform> list = new List<Transform>();
                _objectDic[type] = list;
                _objectDic[type].Add(transform);
            }
            else
            {
                if (!_objectDic[type].Contains(transform))
                {
                    _objectDic[type].Add(transform);
                }
                else
                {
                    Debug.LogWarning("���ҽ� ������Ʈ ��ųʸ� ����Ʈ�� �ߺ�");
                }
                
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
