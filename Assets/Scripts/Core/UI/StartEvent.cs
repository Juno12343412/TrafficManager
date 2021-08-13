using TM.Manager.UI;
using TM.Manager.Game;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class StartEvent : ActionButton
{
    public Text _textObj;
    public Animator _animtor;

    public override void OnClick()
    {
        _animtor.SetBool("isStart", true);
        Invoke("UIOn", 2f);
        Invoke("WaitStart", 2f);

        // 뭔가 싸이클 시작하는 코드 쓰기
        GameManager._instance.StartCycle();
    }

    void OnDisable()
    {
        StopCoroutine("TextOn");
        _animtor.SetBool("isStart", false);
    }

    void WaitStart()
    {
        StartCoroutine(TextOn(true));
    }

    public void UIOn()
    {
        GameManager._instance.ClearUI();
        GameManager._instance.ShowPopUp(UIKind.Game);
    }

    IEnumerator TextOn(bool v)
    {
        yield return new WaitForSeconds(2f);
        _textObj.gameObject.SetActive(v);
        StartCoroutine(TextOn(!v));
    }
}
