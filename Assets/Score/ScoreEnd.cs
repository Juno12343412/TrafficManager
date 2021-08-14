using UnityEngine;
using UnityEngine.UI;
using TM.Manager.Game;

public class ScoreEnd : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = GameManager._instance._setting.upScore.ToString() + "+";
    }

    public void ScoreUpEnd()
    {
        GameManager._instance.subScoreText.gameObject.SetActive(false);
        GameManager._instance._setting.upScore = 0;
    }
}
