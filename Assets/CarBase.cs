using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TM.Manager.Game;

public class CarBase : MonoBehaviour
{
    public float speed;
    public float score;
    public bool isStop = false;
    protected bool isDead = false;
    public bool horizontal = false;
    public bool reverse = false;
    Vector3 moveDir;

    protected void Setting(Sprite[] img)
    {
        if (transform.position.x < -7.5)
        {
            moveDir = Vector3.right;
            GetComponent<Animator>().SetInteger("MoveDir", 1);
            GetComponent<SpriteRenderer>().sprite = img[1];
        }
        else if (transform.position.x > 7.5)
        {
            moveDir = Vector3.left;
            GetComponent<Animator>().SetInteger("MoveDir", 0);
            GetComponent<SpriteRenderer>().sprite = img[0];
        }
        else if (transform.position.y > 4)
        {
            moveDir = Vector3.down;
            GetComponent<Animator>().SetInteger("MoveDir", 3);
            GetComponent<SpriteRenderer>().sprite = img[3];
        }
        else if (transform.position.y < -4)
        {
            moveDir = Vector3.up;
            GetComponent<Animator>().SetInteger("MoveDir", 2);
            GetComponent<SpriteRenderer>().sprite = img[2];
        }

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0;

    }

    

    protected void Cycle()
    {
        if (transform.position.x < -10)
            Pass();
        else if (transform.position.x > 10)
            Pass();
        else if (transform.position.y > 8)
            Pass();
        else if (transform.position.y < -8)
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
        if (!isStop)
        {
            Move();
        }
    }

    protected virtual void Move() // 자동차 움직일 때
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }


    protected virtual void Dead() // 자동차 충돌 시
    {
        isDead = true;
        GetComponent<Animator>().SetBool("isStop", true);

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

    IEnumerator CR_Accident()
    {
        speed = speed * (3.0f / speed);
        float leftSpeed = speed;
        while (speed > 0)
        {
            speed -= leftSpeed / 10;
            yield return new WaitForSeconds(0.1f);
        }
        speed = 0;
        isStop = true;
    }
}
