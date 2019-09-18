using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public partial class TicTacToe : Form
    {
        const char EMPTY_CHAR = '#';
        const char PLAYER_CHAR = 'x';
        const char AI_CHAR = 'o';
        const int AI_TURN = 0;
        const int PLAYER_TURN = 1;

        char[] cellData;
        Label[] cells;
        int playerScore, aiScore;
        int currentTurn;

        public TicTacToe()
        {
            InitializeComponent();
            cellData = new char[9];
            cells = new Label[9];
            int n = 0;
            foreach (Control item in Controls)
            {
                if (item is Label)
                {
                    if (item != label10)
                    {
                        item.Click += Item_Click;
                        cells[n++] = item as Label;
                    }
                }
            }
            button1.Click += (s, e) => NewGame();
            NewGame();
            RefreshScore();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var label = sender as Label;
            var name = label.Name;
            var num = int.Parse(name.Replace("label","")) - 1;
            if (currentTurn == PLAYER_TURN && IsEmpty(label))
            {
                label.Text = PLAYER_CHAR.ToString();
                cellData[num] = PLAYER_CHAR;
                // todo
                BotTurn();
            }
        }

        void NewGame()
        {
            playerScore = 0;
            aiScore = 0;
            for (int i = 0; i < 9; i++)
            {
                cellData[i] = EMPTY_CHAR;
                cells[i].Text = cellData[i].ToString();
            }
            var rnd = new Random();
            currentTurn = rnd.Next(2);
        }

        void RefreshScore()
        {
            label10.Text = $"{Environment.UserName}: {playerScore}\nБот: {aiScore}";
        }

        bool IsEmpty(Label cell)
        {
            return cell.Text == EMPTY_CHAR.ToString();
        }

        void BotTurn()
        {
            var emptyCells = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (cellData[i] == EMPTY_CHAR) emptyCells.Add(i);
            }
            var rnd = new Random();
            var index = rnd.Next(emptyCells.Count);
            var rndCell = emptyCells.ElementAt(index);
            cellData[rndCell] = AI_CHAR;
            cells[rndCell].Text = AI_CHAR.ToString();
            // todo
            currentTurn = PLAYER_TURN;
        }
    }
}
