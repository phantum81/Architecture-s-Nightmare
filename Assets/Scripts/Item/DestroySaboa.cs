using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySaboa : MonoBehaviour
{




    private void OffSaboaKinematic()
    {
        Rigidbody[] rigds = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody r in rigds)
        {
            r.isKinematic = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (EObjectType.RollObject == collision.transform.GetComponent<IObject>()?.GetObjectType())
        {
            //OffSaboaKinematic();
            EventBus.TriggerEventAction(EEventType.MimMapCinema);

        }


    }

    private void OnDisable()
    {
        EventBus.UnsubscribeAction(EEventType.MimMapCinema, OffSaboaKinematic);
    }

    private void OnEnable()
    {
        EventBus.SubscribeAction(EEventType.MimMapCinema, OffSaboaKinematic);
    }
}
