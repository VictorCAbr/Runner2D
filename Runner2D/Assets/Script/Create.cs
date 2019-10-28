using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject Target;
    private float CountTime;
    public float MaxTime, MinTime, DeltaTime;
    private bool Gaming;

    void Update()
    {
        Gaming = GameObject.Find("Zombie").GetComponent<PlayerMove>().Gaming;
        if (Gaming)
        {
            CountTime += Time.deltaTime;
            if (CountTime > MaxTime)
            {
                CountTime = 0;
                Instantiate(Target, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
            }
        }
    }
}
