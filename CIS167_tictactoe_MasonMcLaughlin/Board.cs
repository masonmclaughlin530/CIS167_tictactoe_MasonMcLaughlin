using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS167_tictactoe_MasonMcLaughlin
{
    public class Board
    {
        Cell[,] gameBoard = new Cell[3, 3];


        public Cell getCell(int r, int c)
        {
            return gameBoard[r, c];
        }

        public void setBoardCell(Cell cell)
        {
            gameBoard[cell.getRow(), cell.getCol()] = cell;
        }

        public void clearBoard()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    gameBoard[r, c].setEmpty();
                }
            }

        }

        public Board boardClone()
        {
            Board boardClone = new Board();

            boardClone.gameBoard = new Cell[3, 3];
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    boardClone.gameBoard[r, c] = this.gameBoard[r, c].cloneCell();
                }
            }

            return boardClone;
        }

        public int whoWon()
        {
            //horizontal
            for (int r = 0; r < 3; r++)
            {
                if (this.getCell(r, 0).getValue().Equals('X') && this.getCell(r, 1).getValue().Equals('X') && this.getCell(r, 2).getValue().Equals('X'))
                {
                    return -1;
                }
                else if (this.getCell(r, 0).getValue().Equals('O') && this.getCell(r, 1).getValue().Equals('O') && this.getCell(r, 2).getValue().Equals('O'))
                {
                    return 1;
                }
                //vertical
                for (int c = 0; c < 3; c++)
                {
                    if (this.getCell(0, c).getValue().Equals('X') && this.getCell(1, c).getValue().Equals('X') && this.getCell(2, c).getValue().Equals('X'))
                    {
                        return -1;
                    }
                    else if (this.getCell(0, c).getValue().Equals('O') && this.getCell(1, c).getValue().Equals('O') && this.getCell(2, c).getValue().Equals('O'))
                    {
                        return 1;
                    }
                }

                if (this.getCell(0, 0).getValue().Equals('X') && this.getCell(1, 1).getValue().Equals('X') && this.getCell(2, 2).getValue().Equals('X'))
                {
                    return -1;
                }
                else if (this.getCell(2, 0).getValue().Equals('X') && this.getCell(1, 1).getValue().Equals('X') && this.getCell(0, 2).getValue().Equals('X'))
                {
                    return -1;
                }
                else if (this.getCell(0, 0).getValue().Equals('O') && this.getCell(1, 1).getValue().Equals('O') && this.getCell(2, 2).getValue().Equals('O'))
                {
                    return 1;
                }
                else if (this.getCell(2, 0).getValue().Equals('O') && this.getCell(1, 1).getValue().Equals('O') && this.getCell(0, 2).getValue().Equals('O'))
                {
                    return 1;
                }
            }

            return -999;
        }

        public int checkWinner()
        {
            int winner = whoWon();
            if (winner == 1)
            {
                return 1;
            }
            else if (winner == -1)
            {
                return -1;
            }

            if (tieGame())
            {
                return 0;
            }

            return -999;


        }

        public bool tieGame()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (this.getCell(r, c).isEmpty())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
