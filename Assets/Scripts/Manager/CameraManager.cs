using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }



    void Start()
    {
        _curCamera = CameraDic[ECameraType.Fps];
        
       
    }

    // Update is called once per frame
    void Update()
    {
        _curCamera = GetCurCamera();
        
    }

    public void Init()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineVirtualCameraBase fpsCam = GameObject.FindGameObjectWithTag(ConstBundle.FPS_CAMERA_TAG).GetComponent<CinemachineVirtualCameraBase>();
        CinemachineVirtualCameraBase tutorialCam = GameObject.FindGameObjectWithTag(ConstBundle.TUTORIAL_CAMERA_TAG).GetComponent<CinemachineVirtualCameraBase>();
        _cameraDic.Add(ECameraType.Fps, fpsCam);
        _cameraDic.Add(ECameraType.TutorialCam, tutorialCam);
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

}
