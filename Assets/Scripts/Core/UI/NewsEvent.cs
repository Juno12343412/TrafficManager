using TM.Manager.Game;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewsEvent : MonoBehaviour
{
    public NewsKind _kind = NewsKind.NONE;
    public SNewsKind _kind2 = SNewsKind.NONE;
    public Text _text = null;
    public Image _board = null;

    public Material _mat = null;

    public void Show(NewsKind kind)
    {
        _kind = kind;

        PrivateShow();
        Invoke("Hide", 4f);
    }

    public void Show(SNewsKind kind)
    {
        _kind2 = kind;

        PrivateShow();
        Invoke("Hide", 4f);
    }

    void PrivateShow()
    {
        GameManager._instance._setting.isNews = true;
        _text.gameObject.SetActive(true);

        if (_kind2 != SNewsKind.NONE)
            StartCoroutine("Glitch");

        switch (_kind)
        {
            case NewsKind.A:
                _text.text = "대통령 [모든 도시에 전기 공급량 증가 추진]";
                break;

            case NewsKind.B:
                _text.text = "B시티 최악의 화재... Z시티의 1.7배 면적 잿더미로";
                break;

            case NewsKind.C:
                _text.text = "요즘 대세는 로봇 동물원!";
                break;

            case NewsKind.D:
                _text.text = "밤하늘 수놓은 드론쇼...일 주일 더 본다";
                break;

            case NewsKind.E:
                _text.text = "유럽 인공 자궁 보관소 복지 추진...최대 1000억 지원";
                break;

            case NewsKind.F:
                _text.text = "오늘도 일하세요 A시티는 언제나 여러분을 지켜봅니다";
                break;

            case NewsKind.G:
                _text.text = "여러분 우리 게임 열심히 해주세요 허허 - 기획자 올림";
                break;

            case NewsKind.NONE:
            default:
                break;
        }

        switch (_kind2)
        {
            case SNewsKind.Crazy1:
                _text.text = "정체 불명의 폭주족, 도심을 누비고 있어...";
                break;

            case SNewsKind.Crazy2:
                _text.text = "폭주족들의 등장.. 교통 정리 AI들 업데이트 필요";
                break;

            case SNewsKind.Crazy3:
                _text.text = "A시티에서 난폭 운전 발생... 신속한 대처 필요";
                break;

            case SNewsKind.Police1:
                _text.text = "폭주족과 경찰과의 대치 상황 발생";
                break;

            case SNewsKind.Police2:
                _text.text = "모든 AI는 폭주족의 차량 관리 유의";
                break;

            case SNewsKind.Truck1:
                _text.text = "B시티와의 거래량 급증... 교통량 증가";
                break;

            case SNewsKind.Truck2:
                _text.text = "트럭 운수 물량 증가, 관련 법률 재조정";
                break;

            case SNewsKind.Truck3:
                _text.text = "밤새 움직이는 커다란 차량들 신고 들어와...";
                break;

            case SNewsKind.Truck4:
                _text.text = "샌즈와의 거래 불발...반품 물량 트럭 급증";
                break;

            case SNewsKind.Gold1:
                _text.text = "A시티, 움직이는 빌딩 출몰";
                break;

            case SNewsKind.Gold2:
                _text.text = "모든 차량들, 금빛 차에 경계 태세";
                break;

            case SNewsKind.NONE:
            default:
                break;
        }
    }

    void Hide()
    {
        GameManager._instance._setting.isNews = false;

        _kind = NewsKind.NONE;
        _kind2 = SNewsKind.NONE;

        _text.gameObject.SetActive(false);
    }

    IEnumerator Glitch()
    {
        _board.GetComponent<Image>().material = _mat;
        yield return new WaitForSeconds(2f);
        _board.GetComponent<Image>().material = null;
    }
}
