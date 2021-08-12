using System;
using UnityEngine;

namespace TM.Manager.Game
{
    public enum CarKind : int
    {
        Basic,
        Crazy,
        Police,
        Tank,
        Gold,
        NONE = 99
    }

    public enum DirectionKind : int
    {
        Left,
        Right,
        Up,
        Down,
        Max,
        NONE = 99
    }

    public enum UIKind : int
    {
        Main,
        Game,
        News,
        Over,
        Option,
        NONE = 99
    }

    public enum NewsKind : int
    {
        A,
        B,
        C,
        NONE = 99
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance = null;

        [Header("UI")]
        public GameObject[] uiObjs = null;

        [Header("Car")]
        public GameObject[] carObjs = null;
        public Transform carParent = null;

        [Serializable]
        public class Setting
        {
            public int globalScore = 0;
        }
        public Setting _setting = new Setting();

        void Awake()
        {
            Init();
        }

        void Init()
        {
            #region Singleton
            if (_instance == null) {

                _instance = this;
                DontDestroyOnLoad(this);
            
            } else {

                if (_instance != this)
                    Destroy(this);
            }
            #endregion
        }

        public void SpawnCar(CarKind kind, DirectionKind direction = DirectionKind.NONE)
        {
            if (direction == DirectionKind.NONE)
                direction = (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max);

            Vector3 v = Vector3.zero;
            switch (direction)
            {
                case DirectionKind.Left:
                    v = new Vector3(8.5f, 0f, 0f);
                    break;

                case DirectionKind.Right:
                    v = new Vector3(-8.5f, 0f, 0f);
                    break;

                case DirectionKind.Up:
                    v = new Vector3(0f, 5f, 0f);
                    break;

                case DirectionKind.Down:
                    v = new Vector3(0f, -5f, 0f);
                    break;

                case DirectionKind.NONE:
                default:
                    break;
            }
            Instantiate(carObjs[(int)kind], v, Quaternion.identity, carParent);
        }

        public void ShowPopUp(UIKind kind)
        {
            uiObjs[(int)kind].SetActive(true);
        }

        public void ShowNews(NewsKind kind)
        {
            uiObjs[(int)UIKind.News].SetActive(true);
            uiObjs[(int)UIKind.News].GetComponent<NewsEvent>()._kind = kind;
        }

        public void ClearUI()
        {
            foreach (var obj in uiObjs)
                obj.SetActive(false);
        }
    }
}