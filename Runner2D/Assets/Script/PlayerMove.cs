using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool Idle, Dead;
    private Animator anim;
    private float Height;
    public float MaxHeight, MinHeight;
    private float PosiX;
    public float MaxPosiX;
    [Range(0,30)]
    public float Speed;


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
    }

    // Update is called once per frame
    void Update()
    {

        Height -= (Speed / 10) * Time.deltaTime;
       
        PosiX += (Speed / 10) * Time.deltaTime;
        

        if ((Input.touchCount > 0) || (Input.GetMouseButton(0)))
            if (Height < MaxHeight)
            {
                Height += 2 * (Speed / 10) * Time.deltaTime;
            }



        if (Height > MaxHeight)
            Height = MaxHeight;
        if (Height < MinHeight)
            Height = MinHeight;
        if (PosiX > MaxPosiX)
            PosiX = MaxPosiX;

        estado = Estado.Walk;
        if (estado==Estado.Dead)
        {
            ResetTime += Time.deltaTime;
            if(ResetTime > MaxResetTime)
            {
                ResetTime = 0;
                Resetar();
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

    private void ChangeEstado()
    {

        //if (estado == Estado.Idle)
        //    estado = Estado.Walk;
        //else
        if (estado == Estado.Walk)
            estado = Estado.Dead;
        //else if (estado == Estado.Dead)
        //    estado = Estado.Idle;
    }
    public void Resetar()
    {
        estado = Estado.Idle;
    }
}
