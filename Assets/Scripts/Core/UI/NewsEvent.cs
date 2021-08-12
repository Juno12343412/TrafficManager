using TM.Manager.Game;
using UnityEngine;
using UnityEngine.UI;

public class NewsEvent : MonoBehaviour
{
    public NewsKind _kind = NewsKind.NONE;
    public Text _text = null;

    private void OnEnable()
    {
        Show();
        Invoke("Hide", 3.5f);
    }

    void Show()
    {
        switch (_kind)
        {
            case NewsKind.A:
                break;

            case NewsKind.B:
                break;

            case NewsKind.C:
                break;

            case NewsKind.NONE:
            default:
                break;
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
