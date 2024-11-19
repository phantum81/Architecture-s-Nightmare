using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WreckObject : MonoBehaviour
{
    List<Transform> childList = new List<Transform>();
    List<Vector3> originPosList = new List<Vector3>();
    List<Quaternion> originRotList = new List<Quaternion>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childList.Add(transform.GetChild(i).transform);
            originPosList.Add(transform.GetChild(i).position);
            originRotList.Add(transform.GetChild(i).rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOn()
    {
        gameObject.SetActive(true);
        for(int i =0; i< childList.Count; i++)
        {
            childList[i].transform.position = originPosList[i];
            childList[i].transform.rotation = originRotList[i];
        }
        StartCoroutine(FadeOutObject());
    }

    public void SetOff()
    {
        gameObject.SetActive(false);

    }

    private IEnumerator FadeOutObject()
    {
        yield return new WaitForSeconds(4f);
        for(int i =0; i< childList.Count; i++)
        {
            Rigidbody rigd = childList[i].GetComponent<Rigidbody>();
            rigd.Sleep();
            rigd.useGravity= false;
            childList[i].GetComponent<Collider>().enabled= false;
        }
        while (transform.position.y > -1f)
        {

            transform.position += 0.5f*Vector3.down * Time.deltaTime;
            yield return null;
     
        }
        EventBus.TriggerEventAction(EEventType.ReleaseBuilding, this.gameObject);

    }




}
