using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIS167_tictactoe_MasonMcLaughlin
{
    public partial class Form1 : Form
    {
        Board board;
        bool gameOver = false;
        int whoGoes;
        int winner;
        Cell cell;
        bool playerCanPlay;
        bool gameStarted = false;

        public Form1()
        {
            InitializeComponent();

            lbl_winner.Visible = false;
            lbl_newGame.Visible = false;
            newGameBtn.Enabled = false;
        }

        private void startBtnClick(object sender, EventArgs e)
        {
            setUpBoard();

            startBtn.Enabled = false;

            if(whoGoes == 1)
            {
                playerCanPlay = true;
                Console.WriteLine(playerCanPlay);
            }
            else if(whoGoes == 2)
            {
                Cell bestmove = null;
                bestmove = minimaxStart(board);

                board.getCell(bestmove.getRow(), bestmove.getCol()).setValue('O');
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().Text = "O";
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().ForeColor = Color.Black;

                playerCanPlay = true;
            }
            else
            {
                this.Close();
            }
        }

        private void newGameBtnClick(object sender, EventArgs e)
        {
            setUpBoard();

            startBtn.Enabled = false;
            newGameBtn.Enabled = false;
            gameOver = false;
            lbl_newGame.Visible=false;
            lbl_winner.Visible=false;

            if (whoGoes == 1)
            {
                playerCanPlay = true;
                Console.WriteLine(playerCanPlay);
            }
            else if (whoGoes == 2)
            {
                Cell bestmove = null;
                bestmove = minimaxStart(board);

                board.getCell(bestmove.getRow(), bestmove.getCol()).setValue('O');
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().Text = "O";
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().ForeColor = Color.Black;

                playerCanPlay = true;
            }
            else
            {
                this.Close();
            }
        }

        private void setUpBoard()
        {
            board = new Board();
            gameStarted = true;
            
            winner = -999;

            button1.Text = "00";
            button2.Text = "01";
            button3.Text = "02";
            button4.Text = "10";
            button5.Text = "11";
            button6.Text = "12";
            button7.Text = "20";
            button8.Text = "21";
            button9.Text = "22";

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            foreach (var btn in this.Controls.OfType<Button>())
            {


                if (btn.Text.ElementAt(0) != '0' && btn.Text.ElementAt(0) != '1' & btn.Text.ElementAt(0) != '2')
                {
                    //nothing
                    //Console.WriteLine(btn.Text);
                }
                else
                {
                    //Console.WriteLine(btn.Text);

                    int r = int.Parse(btn.Text.ElementAt(0).ToString());
                    btn.ForeColor = Color.SkyBlue;

                    int c = int.Parse(btn.Text.ElementAt(1).ToString());
                    btn.ForeColor = Color.SkyBlue;
                    //btn.Text = "";




                    cell = new Cell(r, c, btn, '-');
                    //Console.WriteLine(cell.getRow() + "," + cell.getCol());
                    board.setBoardCell(cell);
                }



            }
            board.clearBoard();
            enableBoard();
        }

        private async void onButtonClick(object sender, EventArgs e)
        {
            if(!playerCanPlay)
            {
                return;
            }

            Button btn = sender as Button;
            int r = int.Parse(btn.Text.ElementAt(0).ToString());
            int c = int.Parse(btn.Text.ElementAt(1).ToString());
            //Console.WriteLine(r + c);

            //Console.WriteLine(board.getCell(r, c).getValue());

            if (board.getCell(r,c).isEmpty())
            {
                board.getCell(r, c).setValue('X');
                board.getCell(r, c).getBtn().Text = "X";
                board.getCell(r,c).getBtn().ForeColor = Color.Black;
            }

            winner = board.checkWinner();

            if(winner != -999)
            {
                //game over
                lbl_winner.Text = "Game Over";
                lbl_winner.Visible = true;
                await Task.Delay(1000);
                gameOver = true;
                gameStarted = false;
                

                if (winner == -1)
                {
                    lbl_winner.Text = "Player has won";
                }
                else if (winner == 1)
                {
                    lbl_winner.Text = "The AI has won";
                }
                if (winner == 0)
                {
                    lbl_winner.Text = "The game has ended in a draw";
                }

                lbl_newGame.Visible = true;
                newGameBtn.Enabled = true;

            }


            playerCanPlay = false;
            await Task.Delay(1000);

            if(!gameOver)
            {
                Cell bestmove = null;
                bestmove = minimaxStart(board);

                board.getCell(bestmove.getRow(), bestmove.getCol()).setValue('O');
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().Text = "O";
                board.getCell(bestmove.getRow(), bestmove.getCol()).getBtn().ForeColor = Color.Black;

                winner = board.checkWinner();

                if (winner != -999)
                {
                    //game over
                    lbl_winner.Text = "Game Over";
                    lbl_winner.Visible = true;
                    await Task.Delay(1000);
                    gameOver = true;
                    gameStarted = false;

                    if (winner == -1)
                    {
                        lbl_winner.Text = "Player has won";
                    }
                    else if (winner == 1)
                    {
                        lbl_winner.Text = "The AI has won";
                    }
                    if (winner == 0)
                    {
                        lbl_winner.Text = "The game has ended in a draw";
                    }

                    lbl_newGame.Visible = true;
                    newGameBtn.Enabled = true;

                }
            }

            if (!gameOver)
            {
                playerCanPlay = true;
            }
        }

        private Cell minimaxStart(Board b)
        {
            int bestScore = Int32.MinValue;
            Cell bestCell = null;

            for(int r = 0; r < 3;r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if(b.getCell(r, c).isEmpty())
                    {
                        Board newBoard = b.boardClone();
                        newBoard.getCell(r, c).setValue('O');
                        int score = miniMaxAlg(newBoard, 0, false);
                        if(score > bestScore)
                        {
                            bestScore = score;
                            bestCell = b.getCell(r, c);
                        }
                    }
                }
            }

            return bestCell;
        }

        private int miniMaxAlg(Board clone, int depth, bool maximizing)
        {
            int bestScore;
            int checkWinner = -999;
            checkWinner = clone.checkWinner();
            if (checkWinner != -999)
            {
                return checkWinner;
            }
            if (maximizing)
            {
                bestScore = Int32.MinValue;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (clone.getCell(r, c).isEmpty())
                        {
                            clone.getCell(r, c).setValue('O');
                            int score = miniMaxAlg(clone, depth + 1, false);
                            clone.getCell(r, c).setEmpty();
                            if (score > bestScore)
                            {
                                bestScore = score;
                            }
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                bestScore = Int32.MaxValue;
                for (int r = 0; r <  3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if(clone.getCell(r, c).isEmpty())
                        {
                            clone.getCell(r, c).setValue('X');
                            int score = miniMaxAlg(clone, depth + 1, true);
                            clone.getCell(r, c).setEmpty();
                            if (score < bestScore)
                            {
                                bestScore= score;
                            }
                        }
                    }
                }
                return bestScore;
            }


        }

        private void enableBoard()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = board.getCell(r, c);
                    if (cell != null)
                    {
                        Button btn = cell.getBtn();
                        if (btn != null)
                        {
                            btn.Enabled = true;
                        }
                    }
                }
            }
        }

        private void disableBoard()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = board.getCell(r, c);
                    if (cell != null)
                    {
                        Button btn = cell.getBtn();
                        if (btn != null)
                        {
                            btn.Enabled = false;
                        }
                    }
                }
            }
        }

        private void exitBtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(!gameStarted)
            {
                whoGoes = 1;
            }
            
            //Console.WriteLine("Player 1 goes first");
            //Console.WriteLine(whoGoes);
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                whoGoes = 2;
            }
            //Console.WriteLine("The AI goes first");
            //Console.WriteLine(whoGoes);

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                Random rnd = new Random();
                whoGoes = rnd.Next(1, 3);

                if (whoGoes == 1)
                {
                    whoGoes = 1;
                    //Console.WriteLine("1 was picked at random");
                    //Console.WriteLine(whoGoes);

                }
                else if (whoGoes == 2)
                {
                    whoGoes = 2;
                    //Console.WriteLine("2 was picked at random");
                    //Console.WriteLine(whoGoes);

                }
            }
        }
    }
}
