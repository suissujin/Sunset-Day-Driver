using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartScript : MonoBehaviour
{
    public static OnStartScript instance;
    public int carIndex = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(instance);
    }
}
