using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float MaxHeight;
    public float MinHeight;
    private float Height;

    [Range(-30, 30)]
    public float ValueEnergy;

    private float PosiX;
    [Range(0,30)]
    public float Speed;
    private float speedFundo;
    public bool Running;
    private bool Dead;
    private float Sex;
    private Animator anim;
    private bool Gaming;



    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
            Sex = Random.Range(0, 9) % 2;
        }

        speedFundo = GameObject.Find("Backgrounds").GetComponent<CreateParallax>().Speed;

        PosiX = transform.position.x;
        Height = Random.Range(MinHeight, MaxHeight);
        Dead = false;
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-1000 * Height);
    }
    // Update is called once per frame
    void Update()
    {
        Gaming = GameObject.Find("Zombie").GetComponent<PlayerMove>().Gaming;
        if (!Running || Dead)
            Speed = speedFundo;
        if (!Gaming)
        {
            if (!Running || Dead)
                Speed = 0;
            else
                Speed = -10;
        }

        PosiX -= (Speed / 10) * Time.deltaTime;


        transform.position = new Vector3(PosiX, Height, 0);
            anim.SetBool("Dead", Dead);
        #region Animacoes
        if (anim != null)
        {
            anim.SetBool("Dead", Dead);
            anim.SetFloat("Sex", Sex);
        }
        #endregion 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
            Dead = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Destroy(gameObject);

        }
    }
}
