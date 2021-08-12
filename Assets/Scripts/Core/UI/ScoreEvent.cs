using UnityEngine;
using UnityEngine.UI;
using TM.Manager.Game;

public class ScoreEvent : MonoBehaviour
{
    public Text _scoreText = null;

    float _fakeScore = 0;

    private void Update()
    {
        if (_fakeScore < GameManager._instance._setting.globalScore)
        {
            _fakeScore += Time.deltaTime * GameManager._instance._setting.globalScore;
            return;
        }
        _fakeScore = GameManager._instance._setting.globalScore;
    }

    private void LateUpdate()
    {
        _scoreText.text = "SCORE " + ((int)_fakeScore).ToString();
    }
}
