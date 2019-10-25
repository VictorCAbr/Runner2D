using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject Target;
    private float CountTime;
    public float MaxTime, MinTime, DeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;
        if (CountTime > MaxTime)
        {
            CountTime = 0;
            Instantiate(Target, new Vector3(transform.position.x, 0, 0),Quaternion.identity);
        }
    }
}
