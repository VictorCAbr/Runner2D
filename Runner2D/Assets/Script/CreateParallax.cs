using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParallax : MonoBehaviour
{
    public GameObject[] Copias;
    public float DeltaX = 20.61657f;
    [Range(0, 100)]
    public float Speed;
    [Range(0, 15)]
    public float DeltaSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Copias= Resources.LoadAll<GameObject>("BackgroundsParallax");

    }

    
}
