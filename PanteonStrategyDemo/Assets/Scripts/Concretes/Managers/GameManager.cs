using UnityEngine;
using System;
using PanteonStrategyDemo.Abstracts.Enums;

namespace PanteonStrategyDemo.Concretes.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnGameBeginning;
        public event Action OnBuildingPlacement;

        public static GameManager Instance { get; private set; }

        public GameState GameState { get; set; }


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        void Start()
        {
            InitializeOnGameBegin();
        }

        public void InitializeOnGameBegin()
        {
            OnGameBeginning?.Invoke();
        }

        public void InitializeOnBuildingPlacement()
        {
            OnBuildingPlacement?.Invoke();
        }
    }
}