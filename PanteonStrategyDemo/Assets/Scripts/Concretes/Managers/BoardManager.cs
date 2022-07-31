using UnityEngine;
using PanteonStrategyDemo.Concretes.GameData;

namespace PanteonStrategyDemo.Concretes.Managers
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] GameObject _tilePrefab;
        [SerializeField] GameObject _tileContainer;

        [SerializeField] int _gridHeight = 100;
        [SerializeField] int _gridWidth = 100;

        public BoardTileData[,] BoardTiles;

        public static BoardManager Instance;

        public int GridHeight => _gridHeight;
        public int GridWidth => _gridWidth;


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

        void OnEnable()
        {
            GameManager.Instance.OnGameBeginning += GenerateBoard;
            GameManager.Instance.OnGameBeginning += GenerateGameTiles;
        }

        void GenerateBoard()
        {
            BoardTiles = new BoardTileData[GridHeight, GridWidth];

            for (int i = 0; i < GridHeight; i++)
            {
                for (int j = 0; j < GridWidth; j++)
                {
                    BoardTiles[i, j] = new BoardTileData { XPosition = j, YPosition = i, IsAvailable = true };
                }
            }
        }

        void GenerateGameTiles()
        {
            for (int i = 0; i < GridHeight; i++)
            {
                for (int j = 0; j < GridWidth; j++)
                {
                    Vector2 position = new Vector2(j, i);

                    GameObject tileClone = Instantiate(_tilePrefab, position, Quaternion.identity);
                    tileClone.transform.SetParent(_tileContainer.transform);
                }
            }

            _tileContainer.transform.position += new Vector3(-0.5f, 0.5f, 0);
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameBeginning -= GenerateBoard;
            GameManager.Instance.OnGameBeginning -= GenerateGameTiles;
        }
    }
}