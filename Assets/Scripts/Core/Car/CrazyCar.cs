using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Manager.Game;

public class CrazyCar : CarBase
{
    public Sprite[] img;
    private void Awake()
    {
        base.Setting(img);
    }

    void Start()
    {
        // 등장 UI 띄우기
        GameManager._instance.ShowNews(NewsKind.A);

        Invoke("CrazyAbility", 1f);
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
        if (collision.gameObject.CompareTag("Car"))
        {
            Dead();
        }
    }

    public void CrazyAbility()
    {
        // 경찰차 소환
        var d = DirectionKind.NONE;

        if (moveDir == Vector3.left)
            d = DirectionKind.Left;
        else if (moveDir == Vector3.right)
            d = DirectionKind.Right;
        else if (moveDir == Vector3.up)
            d = DirectionKind.Up;
        else if (moveDir == Vector3.down)
            d = DirectionKind.Down;

        GameManager._instance.SpawnCar(CarKind.Police, d);
    }
}
