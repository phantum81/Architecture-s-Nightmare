using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Item : MonoBehaviour, IObject, IInteraction
{
    [Header("오브젝트 타입"), SerializeField]
    protected EObjectType eObjectType;

    protected SphereCollider interactionCollider;
    public virtual EObjectType GetObjectType()
    {
        return eObjectType;
    }

    public virtual void Init()
    {
        
        interactionCollider = transform.GetChild(0).GetComponent<SphereCollider>();
    }




    public virtual void OnInteraction()
    {
       
    }

    public virtual EInteractionType GetInteractionType()
    {
        return EInteractionType.Look;
    }

}
