using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraManager : MonoBehaviour
{
    private Dictionary<ECameraType , CinemachineVirtualCameraBase> _cameraDic = new Dictionary<ECameraType, CinemachineVirtualCameraBase>();
    public Dictionary<ECameraType, CinemachineVirtualCameraBase> CameraDic => _cameraDic;

    private CinemachineVirtualCameraBase _curCamera;
    public CinemachineVirtualCameraBase CurCamera => _curCamera;

    private CinemachineBrain cinemachineBrain;
    public CinemachineBrain CinemachineBrain => cinemachineBrain;




    void Start()
    {
        _curCamera = CameraDic[ECameraType.Fps];
        
       
    }

    // Update is called once per frame
    void Update()
    {
        _curCamera = GetCurCamera();

        switch (GameManager.Instance.EgameState)
        {
            case EGameState.None:
                break;
            case EGameState.Playing:
                ChangeCamera(ECameraType.Fps);
                break;
            case EGameState.MiniGameMimMapFirst:
                ChangeCamera(ECameraType.MiniGameFirst);
                break;
            case EGameState.MiniGameMimMapSecond:
                ChangeCamera(ECameraType.MiniGameSecond);
                break;
        }


    }

    public void Init()
    {
        
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineVirtualCameraBase fpsCam = GameObject.FindGameObjectWithTag(ConstBundle.FPS_CAMERA_TAG).GetComponent<CinemachineVirtualCameraBase>();
        CinemachineVirtualCameraBase MiniGameFirst = GameObject.FindGameObjectWithTag(ConstBundle.MINIGAME_FIRST_CAMERA_TAG)?.GetComponent<CinemachineVirtualCameraBase>();
        CinemachineVirtualCameraBase MiniGameSecond = GameObject.FindGameObjectWithTag(ConstBundle.MINIGAME_SECOND_CAMERA_TAG)?.GetComponent<CinemachineVirtualCameraBase>();



        AddCameraToDictionary(ECameraType.Fps, fpsCam);
        AddCameraToDictionary(ECameraType.MiniGameFirst, MiniGameFirst);
        AddCameraToDictionary(ECameraType.MiniGameSecond, MiniGameSecond);

    }

    public CinemachineVirtualCameraBase GetCurCamera()
    {
        if (cinemachineBrain != null)
        {
            CinemachineVirtualCameraBase activeCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
            if (activeCamera == null)
                return _cameraDic[ECameraType.Fps];
            return activeCamera;
        }
        else
            return null;
    }

    public void ChangeCamera(ECameraType _type)
    {
        if (_cameraDic.TryGetValue(_type, out CinemachineVirtualCameraBase value))
        {

            foreach (var kvp in _cameraDic)
            {
                if (kvp.Key != _type) 
                {
                    kvp.Value.Priority = 5; 
                }
            }

            value.Priority = 10;
            _curCamera = value;
        }
        else
            Debug.LogWarning("카메라 못찾아옴");
    }


    private void AddCameraToDictionary(ECameraType _camType, CinemachineVirtualCameraBase _cam)
    {
        if (_cam != null)
        {
            _cameraDic[_camType] = _cam;
        }
    }

    public (Transform, Vector3) GetRayCastToTagTarget(string _tag)
    {
        Ray ray = cinemachineBrain.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 2) return(null, Vector3.zero);
            if (hit.collider.gameObject.CompareTag(_tag))
            {
                return (hit.transform, hit.point);
               
            }
            else if (hit.collider.gameObject.CompareTag("Ignore"))
            {
                return (null, Vector3.zero);
            }
            else
            {
                return (null, hit.point);
            }
            
        }
        else
            return (null, Vector3.zero);
    }


    public void OnDrawGizmos()
    {

        Ray ray = cinemachineBrain.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);


        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }

}
