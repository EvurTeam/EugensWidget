using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace EugensWidget
{
    public partial class Form1 : Form
    {
        const int BASE_HEIGHT = 199;
        const int MAX_HEIGHT = 518;

        const int MINUTE = 60 * 1000;
        const int WEATHER_INTERVAL = MINUTE * 20;
        const int CURRENCY_INTERVAL = MINUTE * 5;
        const int JOKE_INTERVAL = MINUTE * 2;

        Random rnd = new Random();
        JokeItem jokeItem;

        public Form1()
        {
            InitializeComponent();
            Height = BASE_HEIGHT;
            Top = 0;
            Left = (Screen.PrimaryScreen.Bounds.Width / 2) - Width / 2;
            var curItem = new CurrencyItem();
            curItem.SetViewer(label1);
            jokeItem = new JokeItem();
            jokeItem.SetViewer(textBox1);
            var weatherItem = new WeatherItem();
            weatherItem.SetViewer(label2);
            var t = new Timer();
            t.Interval = CURRENCY_INTERVAL;
            t.Tick += (s, e) => curItem.ShowResult();
            t.Start();
            var t2 = new Timer();
            t2.Interval = JOKE_INTERVAL;
            t2.Tick += (s, e) => jokeItem.ShowResult();
            t2.Start();
            var t3 = new Timer();
            t3.Interval = WEATHER_INTERVAL;
            t3.Tick += (s, e) => weatherItem.ShowResult();
            t3.Start();
            linkLabel1.Click += (s, e) => Close();
            linkLabel2.Click += (s, e) => СвернутьВТрейToolStripMenuItem_Click(null, null);
            button1.Click += (s, e) => SafeProcessStart("https://youtube.com");
            button2.Click += (s, e) => SafeProcessStart("steam://rungameid/570");
            button3.Click += (s, e) => SafeProcessStart("https://hh.ru");
            button4.Click += (s, e) => SafeProcessStart("https://yandex.ru/news/rubric/index?from=index");
            textBox1.Click += (s, e) => jokeItem.ShowResult(); t2.Stop(); t2.Start();
            button7.Click += (s, e) => SafeProcessStart("https://www.avito.ru/");
            button8.Click += (s, e) => SafeProcessStart("https://www.vk.com/");
            button9.Click += (s, e) => SafeProcessStart("calc");
            button10.Click += (s, e) => SafeProcessStart("notepad");
            button5.Click += (s, e) => Screenshot();
            button11.Click += (s, e) => SafeProcessStart("http://htmlbook.ru/");
            button12.Click += (s, e) => SafeProcessStart("mspaint");
            button13.Click += Button13_Click;
            button14.Click += GoogleIt;
            button6.Click += (s,e) => SafeProcessStart("explorer.exe");
            button15.Click += (s, e) => new TicTacToe().Show();
            textBox2.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) GoogleIt(null, null);
                e.Handled = true;
            };
            textBox2.Text = GetRandomQuest();
            checkBox1.CheckedChanged += (s, e) => TopMost = checkBox1.Checked;
            checkBox1.Checked = true;
            checkBox2.CheckedChanged += (s, e) => Opacity = checkBox2.Checked ? 0.4 : 1;
            curItem.ShowResult();
            jokeItem.ShowResult();
            weatherItem.ShowResult();

        }

        private string GetRandomQuest()
        {                                           
            var arr = new[]
            {
                "Как поднять ммр",
                "Контрпик инвокера",
                "Продать рапиру союзника",
                "Новая мета",
                "Гайд на манки кинга",
                "Аркана на фамильяров",
                "Фура роумер или друид",
                "Коги не ломаются что делать",
                "Сплитпуш на хайграунде уроки",
                "Буст аккаунта за 3к",
                "Gogi map скачать бесплатно",
            };
            return arr[rnd.Next(arr.Length)];
        }

        private void GoogleIt(object sender, EventArgs e)
        {
            var question = textBox2.Text;
            if (string.IsNullOrWhiteSpace(question)) return;
            var url = $"https://www.google.com/search?q={question.Replace(" ","+")}";
            Process.Start(url);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            var path64 = $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\sysnative\\snippingtool.exe";
            var path32 = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\snippingtool.exe";
            SafeProcessStartVariant(path32, path64);
        }

        private void Screenshot()
        {
            Opacity = 0;
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            bmp.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + $"\\screenshot{DateTime.Now:hh_mm_ss}.jpeg", ImageFormat.Jpeg);
            Opacity = checkBox2.Checked ? 0.4 : 1;
        }

        private void Flex()
        {
            var r = rnd.Next(0, 3);
            // ricardo
            if (r == 0)
            {
                r = rnd.Next(0, 3);
                var arr = new[] {
                    "https://www.youtube.com/watch?v=bVg4bgI5Lpg",
                    "https://www.youtube.com/watch?v=SVnzS-jO2BA",
                    "https://www.youtube.com/watch?v=7rYB912b3O8"
                };
                SafeProcessStart(arr[r]);
            }
            // dawg & slidan
            else if (r == 1)
            {
                r = rnd.Next(0, 5);
                var arr = new[] {
                    "https://www.youtube.com/watch?v=bZHPxvF5vVM",
                    "https://www.youtube.com/watch?v=PcB3_GHjgc8",

                    "https://www.youtube.com/watch?v=JOZKkFayudg",

                    "https://www.youtube.com/watch?v=GAOLeojIS_k",
                    "https://www.youtube.com/watch?v=x2DsiwBPTqs"
                };
                SafeProcessStart(arr[r]);
            }
            // other
            else if (r == 2)
            {
                r = rnd.Next(0, 3);
                var arr = new[] {
                    "https://www.youtube.com/watch?v=-x_WVQllXoA",
                    "https://www.youtube.com/watch?v=-6Y2DEZLxpU",
                    "https://www.youtube.com/watch?v=kOCxHu_F5xo&list=PL-VMa2rh7q_ZQvmRt0dqidd9GUC-_42pG",
                };
                SafeProcessStart(arr[r]);
            }
        }

        void ChangeTheme(StyleTheme theme)
        {
            BackColor = theme.BackColor;
            ForeColor = theme.ForeColor;
            foreach (Control item in Controls)
            {
                if (item is TextBox || item is Button) continue;
                item.BackColor = theme.BackColor;
                item.ForeColor = theme.ForeColor;
                foreach (Control item2 in item.Controls)
                {
                    if (item2 is TextBox || item is Button) continue;
                    item2.BackColor = theme.BackColor;
                    item2.ForeColor = theme.ForeColor;
                }
            }
        }

        private void СтандартнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(StyleTheme.DefaultTheme);
        }

        private void НочнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(StyleTheme.NightTheme);
        }

        private void ДневнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(StyleTheme.LightTheme);
        }

        private void АдынToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://softtorrent.ru/windows-soft-torrent/13724-kmsauto-lite.html");
        }

        private void ДваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("http://ww1.torrented.xyz/6192-kmsauto-lite-124-dc.html");
        }

        private void ЧекунутьВакансииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://hh.ru/search/vacancy?order_by=publication_time&text=%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82&schedule=remote&area=113&clusters=true&search_field=name&enable_snippets=true");
        }

        private void УбитьЭксплорерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.GetProcessesByName("explorer").FirstOrDefault().Kill();
        }

        private void ВоскреситьЭксплорерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("explorer.exe");
        }

        private void RegeditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("regedit");
        }

        private void CmdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("cmd");
        }

        private void ПингToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "cmd";
            proc.StartInfo.Arguments = "/c ping -n 10 4.2.2.2";
            SafeProcessStart(proc);
        }

        private void ФлексToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flex();
        }

        private void DrWebCureItToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://free.drweb.ru/download+cureit+free/");
        }

        private void AdwCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://toolslib.net/downloads/viewdownload/1-adwcleaner/");
        }

        void SafeProcessStart(string path)
        {
            try
            {
                Process.Start(path);
            } catch
            {
                MessageBox.Show($"Пути \"{path}\" не существует или файл скрыт.");
            }
        }

        void SafeProcessStartVariant(string path, string pathCatch)
        {
            try
            {
                Process.Start(path);
            }
            catch
            {
                try
                {
                    Process.Start(pathCatch);
                }
                catch
                {
                    MessageBox.Show($"Пути \"{path}\" не существует или файл скрыт.");
                }
            }
        }

        void SafeProcessStart(Process proc)
        {
            try
            {
                proc.Start();
            }
            catch
            {
                MessageBox.Show($"Пути \"{proc.StartInfo.FileName}\" не существует или файл скрыт.");
            }
        }

        private void XHunterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new XHunter();
            wnd.Show();
        }

        private void TheBindingOfIsaacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://thelastgame.ru/the-binding-of-isaac-afterbirth-plus/");
        }

        private void CC2019ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("http://torrent-track.net/14424-adobe-photoshop-cc-2019-200013785-pc-repack-by-kpojiuk-multi-ru.html");
        }

        private void СтараяПодборкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("http://waspeer.info/sobranie-knig-po-php-na-russkom-t195812.html");
        }

        private void PHP7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://rutracker.org/forum/viewtopic.php?t=5582907");
        }

        private void CSS3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeProcessStart("https://vk.com/wall-54530371_1177");
        }

        private void СвернутьВТрейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void КопироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void НовыйАнекдотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jokeItem.ShowResult();
        }

        private void ВклвыклРасширенныйРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Height <= BASE_HEIGHT)
            {
                Height = MAX_HEIGHT;
            } else
            {
                Height = BASE_HEIGHT;
            }
        }
    }
}
