using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAbility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            other.GetComponent<CarBase>().GoldStop(true);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Car"))
    //    {
    //        other.GetComponent<CarBase>().GoldStop(false);
    //    }
    //}
}