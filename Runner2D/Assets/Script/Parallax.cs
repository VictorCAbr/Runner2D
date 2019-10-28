using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private GameObject Copia;
    private GameObject Gerador;
    private float PosiX;
    private float deltaX;
    private float speed;
    private float deltaSpeed=5;
    public int Profundidade;
    public float XCreateNew;
    public bool CriarUm = false;

    // Start is called before the first frame update
    void Start()
    {
        Gerador = GameObject.Find("Backgrounds");
        Copia = Gerador.GetComponent<CreateParallax>().Copias[Profundidade];

        deltaSpeed = Gerador.GetComponent<CreateParallax>().DeltaSpeed;
        speed = Gerador.GetComponent<CreateParallax>().Speed;
        deltaX = Gerador.GetComponent<CreateParallax>().DeltaX;

        PosiX = transform.position.x;
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-1000 - 5 * Profundidade);

    }

    // Update is called once per frame
    void Update()
    {
        PosiX = transform.position.x;
        PosiX -= ((speed * ((100 - (Profundidade * deltaSpeed)) / 100)) / 10) * Time.deltaTime; ;
        
        if (PosiX < XCreateNew && !CriarUm)
        {
            CriarUm = true;
            Instantiate(Copia, new Vector3(PosiX + deltaX, 0, 0), Quaternion.identity);
        }
       


        transform.position = new Vector3(PosiX, 0, 0);

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Destroy(gameObject);

        }
    }
}
