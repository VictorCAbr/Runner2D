using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Debug : MonoBehaviour
{
    private Vector3 posi, Ppp;
    private float pX, pY, dX, dY;
    // Start is called before the first frame update
    void Start()
    {
        posi = transform.position;
        pX = posi.x;
        pY = posi.y;
        dX = pX + 1;
        dY = pY + 1;    
    }

    // Update is called once per frame
    void Update()
    {
        posi = new Vector3(pX, pY, 0);
        if (Input.GetMouseButton(0))
            posi.x = dX;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            posi.y = dY;
        }
        transform.position = posi;

    }
}
