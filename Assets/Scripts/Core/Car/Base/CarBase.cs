﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TM.Manager.Game;

public class CarBase : MonoBehaviour
{
    public CarKind kind = CarKind.NONE;
    public DirectionKind direction = DirectionKind.NONE;

    public float speed;
    public float score;

    public bool isStop = false;
    public bool isGoldStop = false;

    protected bool isDead = false;

    public bool horizontal = false;
    public bool reverse = false;
    public Vector3 moveDir;
    public bool isAccidented = false;

    protected void Setting(Sprite[] img)
    {
        if (transform.position.x <= -7.5f)
        {
            direction = DirectionKind.Right;

            moveDir = Vector3.right;
            GetComponent<Animator>().SetInteger("MoveDir", 1);
            GetComponent<SpriteRenderer>().sprite = img[1];
        }
        else if (transform.position.x >= 7.5f)
        {
            direction = DirectionKind.Left;

            moveDir = Vector3.left;
            GetComponent<Animator>().SetInteger("MoveDir", 0);
            GetComponent<SpriteRenderer>().sprite = img[0];
        }
        else if (transform.position.y >= 4.0f)
        {
            direction = DirectionKind.Down;

            moveDir = Vector3.down;
            GetComponent<Animator>().SetInteger("MoveDir", 3);
            GetComponent<SpriteRenderer>().sprite = img[3];
        }
        else if (transform.position.y <= -4.0f)
        {
            direction = DirectionKind.Up;

            moveDir = Vector3.up;
            GetComponent<Animator>().SetInteger("MoveDir", 2);
            GetComponent<SpriteRenderer>().sprite = img[2];
        }

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0;
        //GetComponent<Rigidbody2D>().mass = 1;

    }

    protected void Cycle()
    {
        if (GameManager._instance._setting.isRestart)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -8.5f)
            Pass();
        else if (transform.position.x > 8.5f)
            Pass();
        else if (transform.position.y > 5.5f)
            Pass();
        else if (transform.position.y < -5.5f)
            Pass();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                Clicked();
            }
        }
        if (!isStop && !GameManager._instance._setting.isAccident)
        {
            Move();
        }
        if (isAccidented)
            transform.Translate(moveDir * speed * Time.deltaTime);
        

    }

    protected virtual void Move() // 자동차 움직일 때
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    protected virtual void Dead() // 자동차 충돌 시
    {
        Debug.Log("죽음 들어옴");
        isDead = true;
        GetComponent<Animator>().SetBool("isStop", true);

        GameManager._instance._setting.isGame = false;

        StartCoroutine(CR_Accident());
    }

    protected virtual void Pass() // 자동차 아무 이상없이 통과 시 
    {
        Destroy(gameObject);
    }

    protected virtual void Clicked() // 자동차 클릭 체크 함수
    {
        if (!isDead)
        {
            isStop = !isStop;
            GetComponent<Animator>().SetBool("isStop", isStop);
        }
    }

    public void GoldStop(bool state)
    {
        if ((!state && !isGoldStop) || (state && isStop))
            return;

        isGoldStop = isStop = state;
        GetComponent<Animator>().SetBool("isStop", state);
    }

    IEnumerator CR_Accident()
    {
        isAccidented = true;
        speed = speed * 0.3f;
        float leftSpeed = speed;
        while (speed > 0)
        {
            speed -= leftSpeed / 10;
            yield return new WaitForSeconds(0.1f);
        }
        GameManager._instance.SetAccidentPos(transform.position);
        GameManager._instance.GetAccident();
        speed = 0;
        isStop = true;
        GameManager._instance.ShowPopUp(UIKind.Over);
        GameManager._instance._setting.isOver = true;

    }

   
}
