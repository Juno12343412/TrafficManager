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

    protected void Setting()
    {
        if (transform.position.x < -7.5)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (transform.position.x > 7.5)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        else if (transform.position.y > 4)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        else if (transform.position.y < -4)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
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
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }


    protected virtual void Dead() // 자동차 충돌 시
    {
        isDead = true;
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
