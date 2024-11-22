using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    private CameraManager cameraMgr;
    private InputManager inputMgr;
    private Transform target;
    

    private List<Vector3> originFirstPos = new List<Vector3>();
    private List<Vector3> originSecondPos = new List<Vector3>();
    private quaternion originFirstRotate;
    private quaternion originSecondRotate;

    private quaternion onMouseChangeItRotate;

    private List<Transform> changeItFirstObj;
    private List<Transform> changeItSecondObj;


    private List<Transform> targetList = new List<Transform>();
    [Header("이동 타겟 부모"),SerializeField]
    private Transform targetParent;

    void Start()
    {
        cameraMgr = GameManager.Instance.CameraMgr;
        inputMgr = GameManager.Instance.InputMgr;
        changeItFirstObj = new List<Transform>( ResourceManager.Instance.ObjectDic[EObjectType.MiniGameChaneItFirst]);
        changeItSecondObj = new List<Transform>(ResourceManager.Instance.ObjectDic[EObjectType.MiniGameChaneItSecond]);
        foreach(Transform t in changeItFirstObj) 
        { 
            originFirstPos.Add(t.position);
        }
        foreach(Transform t in changeItSecondObj)
        {
            originSecondPos.Add(t.position);
        }


        originFirstRotate = changeItFirstObj[0].rotation;
        originSecondRotate = changeItSecondObj[0].rotation;
        onMouseChangeItRotate = changeItFirstObj[0].rotation;



    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.EgameState == EGameState.MiniGameMimMapFirst)
        {
            StartMiniGame( changeItFirstObj, originFirstRotate, originFirstPos);

        }
        else if(GameManager.Instance.EgameState == EGameState.MiniGameMimMapSecond)
        {
            StartMiniGame( changeItSecondObj, originSecondRotate, originSecondPos);
        }
    }



    private void StartMiniGame(List<Transform> _objList, quaternion _originRotate, List<Vector3> _originPos)
    {


        Vector3 hitPoint;

        

        (target, hitPoint) = cameraMgr.GetRayCastToTagTarget(ConstBundle.MINIGAME_CHANGE_OBJECT);



        if (_objList.Count != 0)
        {
            if (target != null && !targetList.Contains(target))
            {

                _objList[0].position = target.position;
                _objList[0].rotation = target.rotation;

                if (inputMgr.InputDic[EUserAction.Click])
                {
                    targetList.Add(target);
                    _objList[0].gameObject.layer = 0;
                    if(GameManager.Instance.EgameState == EGameState.MiniGameMimMapFirst)
                        _objList[0].parent = targetParent;
                    _objList.Remove(_objList[0]);
                }

            }
            else if (hitPoint != Vector3.zero)
            {
                _objList[0].position = hitPoint;
                _objList[0].rotation = onMouseChangeItRotate;
            }
            else
            {
                _objList[0].position = _originPos[_originPos.Count - _objList.Count];
                _objList[0].rotation = _originRotate;
            }
        }
        
    }




    public void ResetChangeIt()
    {
        if(GameManager.Instance.EgameState == EGameState.MiniGameMimMapFirst)
        {
            changeItFirstObj = new List<Transform>(ResourceManager.Instance.ObjectDic[EObjectType.MiniGameChaneItFirst]);

            for (int i = 0; i < changeItFirstObj.Count; i++)
            {
                changeItFirstObj[i].gameObject.layer = 2;
                changeItFirstObj[i].position = originFirstPos[i];
                changeItFirstObj[i].rotation = originFirstRotate;
                changeItFirstObj[i].parent = null;
            }
            
        }
        else
        {
            changeItSecondObj = new List<Transform>(ResourceManager.Instance.ObjectDic[EObjectType.MiniGameChaneItSecond]);

            for (int i = 0; i < changeItSecondObj.Count; i++)
            {
                changeItSecondObj[i].gameObject.layer = 2;
                changeItSecondObj[i].position = originSecondPos[i];
                changeItSecondObj[i].rotation = originSecondRotate;
            }
        }
        targetList.Clear();


    }


    public void PuzzleSetActive(bool _active)
    {
        targetParent.gameObject.SetActive(_active);
    }
}
