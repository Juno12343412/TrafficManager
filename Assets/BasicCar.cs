using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Manager.Game;

public class BasicCar : CarBase
{
    public Sprite[] img;
    private void Awake()
    {
        base.Setting(img);
    }
    void Start()
    {
    }

    void Update()
    {
        base.Cycle();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Dead()
    {
        base.Dead();
    }

    protected override void Pass()
    {
        base.Pass();
    }

    protected override void Clicked()
    {
        base.Clicked();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("asdf");
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("asdf22");

            Dead();
        }
    }
}
