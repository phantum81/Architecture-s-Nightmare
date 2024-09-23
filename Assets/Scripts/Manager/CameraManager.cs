using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Dictionary<ECameraType , CinemachineVirtualCameraBase> _cameraDic = new Dictionary<ECameraType, CinemachineVirtualCameraBase>();
    public Dictionary<ECameraType, CinemachineVirtualCameraBase> CameraDic => _cameraDic;

    private CinemachineVirtualCameraBase _curCamera;
    public CinemachineVirtualCameraBase CurCamera => _curCamera;

    private CinemachineBrain cinemachineBrain;
    void Start()
    {
        _curCamera = GetCurCamera();
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
        _cameraDic.Add(ECameraType.Fps, fpsCam);
    }

    public CinemachineVirtualCameraBase GetCurCamera()
    {
        if (cinemachineBrain != null)
        {
            CinemachineVirtualCameraBase activeCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
            return activeCamera;
        }
        else
            return null;
    }
}
