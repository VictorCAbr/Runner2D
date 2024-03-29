﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private bool Idle, Dead;
    public GameObject gbPrincipal, gbHUD, gbMenu;
    public GameObject VfxWalk;
    [Range(1,4)]
    public float MaxSkins;
    public float Skin;
    public bool Gaming, Play;
    private Animator anim;
    private float Height;
    public float MaxHeight, MinHeight;
    private float PosiX;
    public float MaxPosiX;
    public float DelatX;
    [Range(0, 3)]
    public float MaxDeltaX;
    [Range(0, 100)]
    public float Speed;
    public float XSpeed;


    public Slider BarEnergy;
    [Range(1, 2)]
    public float IniEnergy = 1.3f;
    public float MaxEnergy;
    public float CurrentEnergy;
    [Range(0, 100)]
    public float EnergySpeed;

    public Text TxtDistancia;
    public float CurrentDistancia=0;
    [Range(0, 100)]
    public float DisntanciaSpeed;

    public Text TxtScore;
    public Text TxtPontos;
    public float CurrentPontos = 0;

    private enum Estado { Idle, Walk, Dead};
    private Estado estado;

    private enum Tela { Principal, Game, Reset};
    private Tela tela;
         
  //  public bool CanChange;
    private float ResetTime;
    public float MaxResetTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Height = transform.position.y;
        PosiX = transform.position.x;
        CurrentEnergy = MaxEnergy * IniEnergy;
        XSpeed = Speed;
        tela = Tela.Principal;
    }

    // Update is called once per frame
    void Update()
    {
        gbPrincipal.SetActive(tela == Tela.Principal);
        transform.GetChild(0).gameObject.SetActive(tela == Tela.Principal);
        gbHUD.SetActive(tela == Tela.Game);
        gbMenu.SetActive(tela == Tela.Reset);
        VfxWalk.SetActive(estado == Estado.Walk);

        if (Gaming)
        {
            CurrentDistancia += (DisntanciaSpeed / 10) * Time.deltaTime;
            if (CurrentDistancia >= 10000)
                CurrentDistancia -= 10000;

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
        TxtPontos.text = "    x" + (int)CurrentPontos;
        BarEnergy.value = (CurrentEnergy / MaxEnergy);

        if (Height > MaxHeight)
            Height = MaxHeight;
        if (Height < MinHeight)
            Height = MinHeight;
        if (PosiX > MaxPosiX)
            PosiX = MaxPosiX;

        if (CurrentEnergy <= 0)
            estado = Estado.Dead;
        if (Play)
        {
            estado = Estado.Walk;
            Play = false;
        }

        DelatX = MaxPosiX - PosiX;
        Gaming = (estado == Estado.Walk);


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
        MaxSkins = (int)MaxSkins;
        anim.SetFloat("Skin", Skin);
        anim.SetBool("Idle", Idle);
        anim.SetBool("Dead", Dead);
        #endregion
    }

    public void MudaSkin()
    {
        Skin++;
        Skin %= MaxSkins;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estado == Estado.Walk)
        {
            if (collision.tag == "Human" || collision.tag == "Mushroom")
                CurrentEnergy += collision.GetComponent<Inimigo>().ValueEnergy;
            if (collision.tag == "Human")
            {
                MaxEnergy++;
                CurrentPontos++;
                if (CurrentEnergy > MaxEnergy)
                    CurrentEnergy = MaxEnergy;
            }
            if (collision.tag == "Obstaculo")
                XSpeed = -collision.GetComponent<Inimigo>().Speed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Obstaculo")
                CurrentEnergy += (collision.GetComponent<Inimigo>().ValueEnergy * (DelatX / 2)) / 10;
        if (DelatX > MaxDeltaX)
            XSpeed = Speed;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstaculo")
            XSpeed = Speed;
    }
    public void Resetar()
    {
        if (CurrentDistancia > 100)
            MaxSkins = 3;
        if (CurrentDistancia > 150)
            MaxSkins = 4;
        estado = Estado.Idle;
        TxtScore.text = TxtDistancia.text + "\n" + TxtPontos.text;
        CurrentEnergy = MaxEnergy * IniEnergy;
        CurrentDistancia = 0;
        CurrentPontos = 0;
        tela = Tela.Reset;

        GameObject[] Deletar = GameObject.FindGameObjectsWithTag("Human");
        for (int i = 0; i < Deletar.Length; i++)
            Destroy(Deletar[i]);

        Deletar = GameObject.FindGameObjectsWithTag("Mushroom");
        for (int i = 0; i < Deletar.Length; i++)
            Destroy(Deletar[i]);

        Deletar = GameObject.FindGameObjectsWithTag("Obstaculo");
        for (int i = 0; i < Deletar.Length; i++)
            Destroy(Deletar[i]);

        Deletar = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < Deletar.Length; i++)
            Deletar[i].GetComponent<Create>().Resetar();

    }
    public void StarPlay()
    {
        Play = true;
        tela = Tela.Game;
    }
    public void VoltarMenu()
    {
        tela = Tela.Principal;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
