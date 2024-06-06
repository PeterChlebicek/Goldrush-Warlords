using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    [SerializeField] private Transform resourceTreeTransform;
    [SerializeField] private Transform storageTransform;

    private void Awake()
    {
        instance = this;
    }

    private Transform GetResource()
    {
        return resourceTreeTransform;
    }
    public static Transform GetResource_Static()
    {
        return instance.GetResource();
    }
    private Transform GetStorage()
    {
        return storageTransform;
    }
    public static Transform GetStorage_Static()
    {
        return instance.GetStorage();
    }
}
