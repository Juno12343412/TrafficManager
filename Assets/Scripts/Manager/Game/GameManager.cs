using System;
using UnityEngine;

namespace TM.Manager.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance = null; 

        [Serializable]
        public class Setting
        {
            
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
    }
}