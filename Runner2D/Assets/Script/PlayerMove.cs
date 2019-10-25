using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool Idle, Dead;
    private Animator anim;
    private float Height;

    private enum Estado { Idle, Walk, Dead};
    private Estado estado;

    public bool CanChange;
    private float ResetTime;
    public float MaxResetTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) || (Input.GetMouseButton(0)))
        {
            CanChange = true;
            if (estado == Estado.Idle)
                estado = Estado.Walk;
        }
        else if (CanChange)
        {
            CanChange = false;
            ChangeEstado();
        }
        if (estado==Estado.Dead)
        {
            ResetTime += Time.deltaTime;
            if(ResetTime > MaxResetTime)
            {
                ResetTime = 0;
                Resetar();
            }
        }


        Height = transform.position.y;

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
