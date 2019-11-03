using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject Target;
    private float CountTime;
    public float IniTime, FinTime, DeltaTime, CurTime;
    private bool Gaming;
    private Vector3 Posi;

    private void Start()
    {
        Resetar();
    }
    public void Resetar()
    {
        Posi = transform.position;
        CurTime = IniTime;
        CountTime = 3;
    }
    void Update()
    {
        Gaming = GameObject.Find("Zombie").GetComponent<PlayerMove>().Gaming;
        if (Gaming)
        {
            CountTime += Time.deltaTime;
            if (CountTime > CurTime)
            {
                CountTime = 0;
                Instantiate(Target, Posi, Quaternion.identity);
                CurTime = Mathf.MoveTowards(CurTime, FinTime, DeltaTime / 17);
            }
        }
    }
}
