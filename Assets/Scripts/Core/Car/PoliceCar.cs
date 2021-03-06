using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Manager.Game;

public class PoliceCar : CarBase
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
        if (collision.gameObject.CompareTag("Car"))
        {
            if (collision.gameObject.GetComponent<CarBase>().kind == CarKind.Crazy)
            {
                // 점수 오르기
                GameManager._instance.subScoreText.gameObject.SetActive(true);
                GameManager._instance._setting.upScore += 20;
                GameManager._instance._setting.globalScore += 20;

                Destroy(collision.gameObject);
                Destroy(gameObject);
                return;
            }
            Dead();
        }
    }
}