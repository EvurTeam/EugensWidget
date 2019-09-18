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
        const char EMPTY_CHAR = ' ';
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
            Control.CheckForIllegalCrossThreadCalls = false;
            cellData = new char[9];
            cells = new Label[9];
            int n = 0;
            for (int i = 0; i < 9; i++)
            {
                var item = (Label)Controls[$"label{i + 1}"];
                item.Click += Item_Click;
                cells[n++] = item;
            }
            button1.Click += (s, e) => { NewGame(true); RefreshScore(); }
            NewGame(true);
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
                var result = CheckField();
                ParseResult(result);
            }
        }

        private TurnResult CheckField()
        {
            var row1 = cellData[0] == cellData[1] && cellData[0] == cellData[2] && cellData[0] != EMPTY_CHAR;
            var row2 = cellData[3] == cellData[4] && cellData[3] == cellData[5] && cellData[3] != EMPTY_CHAR;
            var row3 = cellData[6] == cellData[7] && cellData[6] == cellData[8] && cellData[6] != EMPTY_CHAR;
            var col1 = cellData[0] == cellData[3] && cellData[0] == cellData[6] && cellData[0] != EMPTY_CHAR;
            var col2 = cellData[1] == cellData[4] && cellData[1] == cellData[7] && cellData[1] != EMPTY_CHAR;
            var col3 = cellData[2] == cellData[5] && cellData[2] == cellData[8] && cellData[2] != EMPTY_CHAR;
            var d1 = cellData[0] == cellData[4] && cellData[0] == cellData[8] && cellData[0] != EMPTY_CHAR;
            var d2 = cellData[2] == cellData[4] && cellData[2] == cellData[6] && cellData[2] != EMPTY_CHAR;
            if (row1 || row2 || row3 || col1 || col2 || col3 || d1 || d2)
            {
                return (currentTurn == AI_TURN) ? TurnResult.AiWin : TurnResult.HumanWin;
            }
            return cellData.Contains(EMPTY_CHAR) ? TurnResult.None : TurnResult.NoWay;
        }

        void ParseResult(TurnResult result)
        {
            if (result == TurnResult.AiWin)
            {
                aiScore++;
                RefreshScore();
                NewGame(false);
            }
            else if (result == TurnResult.HumanWin)
            {
                playerScore++;
                RefreshScore();
                NewGame(false);
            }
            else if (result == TurnResult.None)
            {
                currentTurn = (currentTurn == 0) ? 1 : 0;
                if (currentTurn == AI_TURN) BotTurn();
            }
            else
            {
                NewGame(false);
            }
        }

        void NewGame(bool newScore)
        {
            if (newScore)
            {
                playerScore = 0;
                aiScore = 0;
            }
            for (int i = 0; i < 9; i++)
            {
                cellData[i] = EMPTY_CHAR;
                cells[i].Text = cellData[i].ToString();
            }
            var rnd = new Random();
            currentTurn = rnd.Next(2);
            if (currentTurn == AI_TURN) BotTurn();
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
            var indexInList = rnd.Next(emptyCells.Count);
            var realIndex = emptyCells.ElementAt(indexInList);
            cellData[realIndex] = AI_CHAR;
            // genius...
            var tcb = new System.Threading.TimerCallback((x) => {
                cells[realIndex].Text = AI_CHAR.ToString();
                var result = CheckField();
                ParseResult(result);
            });
            var t = new System.Threading.Timer(tcb, null, new Random().Next(200, 501), System.Threading.Timeout.Infinite);
            // todo если начать новую игру, то таймер срабатывает. Надо блочить кнопку на ход бота
        }
    }

    public enum TurnResult
    {
        None,
        NoWay,
        AiWin,
        HumanWin
    }
}
