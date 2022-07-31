using PanteonStrategyDemo.Concretes.Managers;

namespace PanteonStrategyDemo.Concretes.GameData
{
    public class PlacementConditionData
    {
        public void UpdateTiles(float xPosition, float yPosition, int cellWidth, int cellHeight)
        {
            GetTileOverCursor(xPosition, yPosition, out int row, out int column);

            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    BoardManager.Instance.BoardTiles[i + row, j + column].IsAvailable = false;
                }
            }
        }

        public bool IsPlaceAvailable(float xPosition, float yPosition, int cellWidth, int cellHeight)
        {
            GetTileOverCursor(xPosition, yPosition, out int row, out int column);

            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    if (!BoardManager.Instance.BoardTiles[i + row, j + column].IsAvailable)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void GetTileOverCursor(float xPosition, float yPosition, out int row, out int column)
        {
            row = BoardManager.Instance.GridHeight;
            column = BoardManager.Instance.GridWidth;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (BoardManager.Instance.BoardTiles[i, j].XPosition == xPosition && BoardManager.Instance.BoardTiles[i, j].YPosition == yPosition)
                    {
                        row = i;
                        column = j;
                        return;
                    }
                }
            }
        }
    }
}