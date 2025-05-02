using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIS167_tictactoe_MasonMcLaughlin
{
    public class Cell
    {
        private int row;
        private int col;
        Button btn;
        private char value = '-';

        public Cell (int row, int col, Button btn, char value)
        {
            this.row = row;
            this.col = col;
            this.btn = btn;
            this.value = value;
        }

        //getters
        public int getRow()
        {
            return row;
        }
        public int getCol()
        {
            return col;
        }
        public Button getBtn()
        {
            return btn;
        }

        public char getValue()
        {
            return value;
        }

        //setters
        public void setValue(char v)
        {
            value = v;
        }


        public void setEmpty()
        {
            value = '-';
        }

        public bool isEmpty()
        {
            if(value == '-')
            {
                return true;
            }
            return false;
        }

        public Cell cloneCell()
        {
            Cell clonedCell = new Cell(row, col, btn, value);

            return clonedCell;
        }



    }
}
