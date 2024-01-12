using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;
    // Start is called before the first frame update
    // void Start()
    // {
    //     lightObject.SetActive(false);
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightObject.SetActive(false);
        }
    }
}
