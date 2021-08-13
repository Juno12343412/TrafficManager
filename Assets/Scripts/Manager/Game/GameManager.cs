using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM.Manager.Game
{
    public enum CarKind : int
    {
        Basic,
        Truck,
        Crazy,
        Gold,
        Police,
        NONE = 99
    }

    public enum DirectionKind : int
    {
        Left,
        Left2,
        Right,
        Right2,
        Up,
        Down,
        Max,
        NONE = 99
    }

    public enum UIKind : int
    {
        Main,
        Game,
        Over,
        Option,
        NONE = 99
    }

    public enum NewsKind : int
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        MAX,
        NONE = 99
    }

    public enum SNewsKind : int
    {
        Crazy1,
        Crazy2,
        Crazy3,
        CrazyMax,
        Police1,
        Police2,
        PoliceMax,
        Truck1,
        Truck2,
        Truck3,
        Truck4,
        TruckMax,
        Gold1,
        Gold2,
        GoldMax,
        MAX,
        NONE = 99
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance = null;

        [Header("UI")]
        public GameObject[] uiObjs = null;
        public GameObject scoreUi = null;

        public GameObject newsObj = null;

        [Header("Car")]
        public GameObject[] carObjs = null;
        public Transform carParent = null;

        [Header("Cam")]
        public GameObject originCam = null;
        public GameObject accidentCam = null;

        private List<Vector3> accPosList = new List<Vector3>();

        [Serializable]
        public class Setting
        {
            public int      globalScore = 0;

            public float    playTime = 0f;
            public bool     isGame = false;
            public bool     isPause = false;

            public int      newsCount = 0;
            public float    newsSpawnTime = 5f;
            public float    newsMaxSpawnTime = 2f;
            public bool     isNews = false;
            public bool     isSpecialNews = false;

            public float    carSpawnTime = 4f;
            public float    carMaxSpawnTime = 1.5f;

            public bool     isAccident = false;
            public bool     isRestart = false;
            public bool     isOver = false;
        }
        public Setting _setting = new Setting();

        void Awake()
        {
            Init();
        }

        void Init()
        {
            #region Singleton
            if (_instance == null)
            {

                _instance = this;
                DontDestroyOnLoad(this);

            }
            else
            {

                if (_instance != this)
                    Destroy(this);
            }
            #endregion
        }

        private void Update()
        {
            if(_setting.isOver)
            {
                if(_setting.isRestart)
                {
                    ResetGame();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    _setting.isRestart = true;
                }
            }
        }

        public void GetAccident()
        {
            originCam.SetActive(false);
        }

        public void ResetGame()
        {
            StopCoroutine("GameTimer");
            StopCoroutine("CarCycle");
            StopCoroutine("CarTimer");
            StopCoroutine("NewsCycle");
            StopCoroutine("NewsTimer");
            StopCoroutine("WaitSpawn");
            originCam.SetActive(true);
            _setting.isAccident = true;
            accPosList.Clear();
            _setting.isRestart = false;
            _setting.isOver = false;
            _setting.globalScore = 0;
            _setting.playTime = 0f;
            _setting.isGame = false;
            _setting.isPause = false;
            _setting.newsCount = 0;
            _setting.newsSpawnTime = 5f;
            _setting.newsMaxSpawnTime = 2f;
            _setting.isNews = false;
            _setting.isSpecialNews = false;
            _setting.carSpawnTime = 4f;
            _setting.carMaxSpawnTime = 1.5f;
            StartCycle();
        }


        public void SetAccidentPos(Vector3 position)
        {
            accPosList.Add(position);
            if (accPosList.Count == 2 && !_setting.isAccident)
            {
                _setting.isAccident = true;
                Vector3 posTemp = accPosList[0] + accPosList[1];
                accPosList.Clear();
                posTemp *= 0.5f;
                accidentCam.transform.position = new Vector3(posTemp.x, posTemp.y, accidentCam.transform.position.z);
            }
        }

        public void StartCycle()
        {
            _setting = new Setting();

            _setting.globalScore = 0;
            _setting.playTime = 0;
            _setting.isGame = true;

            StartCoroutine("GameTimer");

            StartCoroutine("CarCycle");
            StartCoroutine("CarTimer");

            StartCoroutine("NewsCycle");
            StartCoroutine("NewsTimer");
        }

        // 플레이 타임 계산하는거
        public IEnumerator GameTimer()
        {
            while (_setting.isGame)
            {
                _setting.playTime += Time.deltaTime;
                yield return null;
            }
        }

        // 자동차 스폰해주는 거
        public IEnumerator CarCycle()
        {
            while (_setting.isGame)
            {
                yield return new WaitForSeconds(_setting.carSpawnTime);

                if (!_setting.isSpecialNews && _setting.isGame)
                {
                    SpawnCar(CarKind.Basic, (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max));
                }
            }
        }

        // 자동차 스폰 시간 줄여주는 거
        public IEnumerator CarTimer()
        {
            while (_setting.isGame)
            {
                yield return new WaitForSeconds(10f);
                if (_setting.carSpawnTime > _setting.carMaxSpawnTime)
                    _setting.carSpawnTime -= 0.1f;
            }
        }

        // 뉴스 스폰해주는 거
        public IEnumerator NewsCycle()
        {
            while (_setting.isGame)
            {
                yield return new WaitForSeconds(_setting.newsSpawnTime);

                // 일반 뉴스
                if (_setting.newsCount < 3 && !_setting.isSpecialNews)
                {
                    ShowNews((NewsKind)UnityEngine.Random.Range((int)NewsKind.A, (int)NewsKind.MAX));
                    _setting.newsCount++;
                }
                else
                {
                    // 처음 / 60초 
                    if (_setting.playTime < 60f)
                    {
                        SpawnCar(CarKind.Truck, (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max));
                    }
                    // 60초
                    else if (_setting.playTime >= 60f && _setting.playTime < 150f)
                    {
                        SpawnCar((CarKind)UnityEngine.Random.Range((int)CarKind.Truck, (int)CarKind.Gold), (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max));
                    }
                    // 150초
                    else if (_setting.playTime >= 150f)
                    {
                        SpawnCar((CarKind)UnityEngine.Random.Range((int)CarKind.Truck, (int)CarKind.Police), (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max));
                    }
                }
            }
        }

        // 뉴스 스폰 시간 줄여주는 거
        public IEnumerator NewsTimer()
        {
            while (_setting.isGame)
            {
                yield return new WaitForSeconds(10f);
                if (_setting.newsSpawnTime > _setting.newsMaxSpawnTime)
                    _setting.newsSpawnTime -= 0.1f;
            }
        }

        // 자동차 스폰하는거
        public void SpawnCar(CarKind kind, DirectionKind direction = DirectionKind.NONE)
        {
            if (direction == DirectionKind.NONE)
                direction = (DirectionKind)UnityEngine.Random.Range((int)DirectionKind.Left, (int)DirectionKind.Max);

            // 방향 구하기
            Vector3 v = Vector3.zero;
            switch (direction)
            {
                case DirectionKind.Left:
                case DirectionKind.Left2:
                    v = new Vector3(8f, -0.37f, 0f);
                    break;

                case DirectionKind.Right:
                case DirectionKind.Right2:
                    v = new Vector3(-8f, 0.54f, 0f);
                    break;

                case DirectionKind.Up:
                    v = new Vector3(0.75f, 5f, 0f);
                    break;

                case DirectionKind.Down:
                    v = new Vector3(-0.75f, -5f, 0f);
                    break;

                case DirectionKind.NONE:
                default:
                    break;
            }

            // 자동차가 일반 차량이면 그냥 스폰시키고 특수 차량이면 특수 뉴스 띄우면서 소환함
            if (kind == CarKind.Basic)
            {
                StartCoroutine(WaitSpawn(carObjs[(int)kind], v, 0f));
            }
            else // 특수 차량
            {
                switch (kind)
                {
                    case CarKind.Truck:
                        ShowNews((SNewsKind)UnityEngine.Random.Range((int)SNewsKind.Truck1, (int)SNewsKind.TruckMax));
                        StartCoroutine(WaitSpawn(carObjs[(int)kind], v, 3f));
                        break;

                    case CarKind.Crazy:
                        ShowNews((SNewsKind)UnityEngine.Random.Range((int)SNewsKind.Crazy1, (int)SNewsKind.CrazyMax));
                        StartCoroutine(WaitSpawn(carObjs[(int)kind], v, 3f));
                        break;

                    case CarKind.Gold:
                        ShowNews((SNewsKind)UnityEngine.Random.Range((int)SNewsKind.Gold1, (int)SNewsKind.GoldMax));
                        StartCoroutine(WaitSpawn(carObjs[(int)kind], v, 3f));
                        break;

                    case CarKind.Police:
                        StartCoroutine(WaitSpawn(carObjs[(int)kind], v, 0.5f));
                        break;

                    case CarKind.NONE:
                    default:
                        break;
                }

                _setting.newsCount = 0;
            }
        }

        // UI창 띄우기
        public void ShowPopUp(UIKind kind)
        {
            uiObjs[(int)kind].SetActive(true);
        }

        // 일반 뉴스 보여주기
        public void ShowNews(NewsKind kind)
        {
            newsObj.GetComponent<NewsEvent>().Show(kind);
        }

        // 특수 뉴스 보여주기
        public void ShowNews(SNewsKind kind)
        {
            newsObj.GetComponent<NewsEvent>().Show(kind);
        }

        // UI 초기화
        public void ClearUI()
        {
            foreach (var obj in uiObjs)
                obj.SetActive(false);
        }

        // 자동차 기다렸다가 스폰하는거
        IEnumerator WaitSpawn(GameObject obj, Vector3 v, float t)
        {
            yield return new WaitForSeconds(t);
            Instantiate(obj, v, Quaternion.identity, carParent);
        }
    }
}