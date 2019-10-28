using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private bool Idle, Dead;
    public bool Gaming;
    private Animator anim;
    private float Height;
    public float MaxHeight, MinHeight;
    private float PosiX;
    public float MaxPosiX;
    public float DelatX;
    [Range(0,30)]
    public float Speed;
    public float XSpeed;


    public Slider BarEnergy;
    public float MaxEnergy;
    public float CurrentEnergy;
    [Range(0,30)]
    public float EnergySpeed;

    public Text TxtDistancia;
    public float CurrentDistancia=0;
    [Range(0,30)]
    public float DisntanciaSpeed;

    private enum Estado { Idle, Walk, Dead};
    private Estado estado;

  //  public bool CanChange;
    private float ResetTime;
    public float MaxResetTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Height = transform.position.y;
        PosiX = transform.position.x;
        CurrentEnergy = MaxEnergy;
        XSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        //
        //
     //   CurrentEnergy = MaxEnergy;
        //



        if (estado != Estado.Dead)
        {
            CurrentDistancia += (DisntanciaSpeed / 10) * Time.deltaTime;
            if (CurrentDistancia >= 1000)
                CurrentDistancia -= 1000;

            CurrentEnergy -= (EnergySpeed / 10) * Time.deltaTime;
            if (CurrentEnergy < 0)
                CurrentEnergy = 0;

            Height -= (Speed / 10) * Time.deltaTime;
            PosiX += (XSpeed / 10) * Time.deltaTime;

            if ((Input.touchCount > 0) || (Input.GetMouseButton(0)))
                if (Height < MaxHeight)
                {
                    Height += 2 * (Speed / 10) * Time.deltaTime;
                }
        }

        TxtDistancia.text = "  D: " + (int)CurrentDistancia + "m";
        BarEnergy.value = (CurrentEnergy / MaxEnergy);

        if (Height > MaxHeight)
            Height = MaxHeight;
        if (Height < MinHeight)
            Height = MinHeight;
        if (PosiX > MaxPosiX)
            PosiX = MaxPosiX;
        DelatX = MaxPosiX - PosiX;
        if (CurrentEnergy <= 0)
            estado = Estado.Dead;
        if (estado == Estado.Idle)
            estado = Estado.Walk;
        Gaming = (estado != Estado.Dead);
        if (estado==Estado.Dead)
        {
            ResetTime += Time.deltaTime;
            if(ResetTime > MaxResetTime)
            {
                ResetTime = 0;
               // Resetar();
            }
        }

        
        transform.position = new Vector3(PosiX, Height, 0);
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-1000 * Height);

        #region Animacoes
        Idle = (estado == Estado.Idle);
        Dead = (estado == Estado.Dead);
        anim.SetBool("Idle", Idle);
        anim.SetBool("Dead", Dead);
        #endregion
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Human" || collision.tag == "Mushroom")
            CurrentEnergy += collision.GetComponent<Inimigo>().ValueEnergy;
        if (collision.tag == "Obstaculo")
            XSpeed = -collision.GetComponent<Inimigo>().Speed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Obstaculo")
            CurrentEnergy +=( collision.GetComponent<Inimigo>().ValueEnergy *(DelatX/2))/ 10;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstaculo")
            XSpeed = Speed;
    }
    public void Resetar()
    {
        estado = Estado.Idle;
    }
}
