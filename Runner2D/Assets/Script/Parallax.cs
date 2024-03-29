﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    #region Statue
    public bool Statue;
    private Vector3 Posi;
    private float NwX;
    [Range(-10, 0)]
    public float MinX = -6;
    [Range(0, 10)]
    public float MaxX = 3;
    #endregion

    public GameObject Copia;
    public GameObject[] Copias;
    private GameObject Gerador;
    private float PosiX;
    private float deltaX;
    private float speed;
    private float deltaSpeed=5;
    public int Profundidade;
    public float XCreateNew;
    public bool CriarUm = false;
    private bool Gaming;

    // Start is called before the first frame update
    void Start()
    {
        Gerador = GameObject.Find("Backgrounds");
        Copias = Resources.LoadAll<GameObject>("BackgroundsParallax");
        Copia = Copias[Profundidade];

        deltaSpeed = Gerador.GetComponent<CreateParallax>().DeltaSpeed;
        speed = Gerador.GetComponent<CreateParallax>().Speed;
        deltaX = Gerador.GetComponent<CreateParallax>().DeltaX;

        PosiX = transform.position.x;
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-1000 - 5 * Profundidade);


        #region Statue
        if (Profundidade == 2)
        {
            Posi = transform.position;
            Posi.x += Random.Range(MinX, MaxX);
            transform.position = Posi;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Gaming = GameObject.Find("Zombie").GetComponent<PlayerMove>().Gaming;
        if (Gaming)
        {
            PosiX = transform.position.x;
            PosiX -= ((speed * ((100 - (Profundidade * deltaSpeed)) / 100)) / 10) * Time.deltaTime; ;

            if (PosiX < XCreateNew && !CriarUm)
                if (Copia != null)
                {
                    CriarUm = true;
                    Instantiate(Copia, new Vector3(PosiX + deltaX, 0, 0), Quaternion.identity);
                }
            transform.position = new Vector3(PosiX, 0, 0);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Destroy(gameObject);

        }
    }
}
