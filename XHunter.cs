using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public partial class XHunter : Form
    {
        IntPtr savePtr = IntPtr.Zero;

        public XHunter()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += (s, e) => { if (savePtr != IntPtr.Zero) Clipboard.SetText(savePtr.ToString()); };
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            button6.Click += Button6_Click;
            button7.Click += Button7_Click;
            button8.Click += Button8_Click;
            button9.Click += Button9_Click;
            button10.Click += Button10_Click;
            button11.Click += Button11_Click;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (var proc in Process.GetProcesses())
            {
                listView1.Items.Add(
                    new ListViewItem(new[]
                    {
                        $"{proc.Id}",
                        $"{proc.ProcessName}",
                        $"{proc.MainWindowHandle}"
                    })
                );
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            if (string.IsNullOrWhiteSpace(textBox10.Text))
                WinAPI.SetWindowText(savePtr, "");
            else
                WinAPI.SetWindowText(savePtr, textBox10.Text);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            var w = Screen.PrimaryScreen.Bounds.Width;
            var h = Screen.PrimaryScreen.Bounds.Height;
            var gwl = WinAPI.GetWindowLong(ptr, -16);
            WinAPI.SetWindowLongPtr(ptr, -16, new IntPtr(gwl & ~WinAPI.WS_BORDER));
            WinAPI.SetWindowLongPtr(ptr, -16, new IntPtr(gwl & ~WinAPI.WS_CAPTION));
            WinAPI.MoveWindow(ptr, 0, 0, w, h, true);

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            var sb = WinAPI.GetWindowTextRaw(ptr);
            if (string.IsNullOrWhiteSpace(sb))
                textBox9.Text = "<окно не содержит текст>";
            else
                textBox9.Text = sb;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            int.TryParse(textBox7.Text, out var w);
            int.TryParse(textBox8.Text, out var h);
            if (w == 0 || h == 0) return;
            WinAPI.GetWindowRect(ptr, out var rect);
            WinAPI.MoveWindow(ptr, rect.Left, rect.Top, w, h, true);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            WinAPI.ShowWindow(ptr, WinAPI.SW_SHOW);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            WinAPI.ShowWindow(ptr, WinAPI.SW_HIDE);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var ptr = string.IsNullOrWhiteSpace(tbPtr.Text) ? IntPtr.Zero : new IntPtr(int.Parse(tbPtr.Text));
            if (ptr == IntPtr.Zero) return;
            WinAPI.SendMessage(ptr, WinAPI.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        void FailMsg()
        {
            savePtr = IntPtr.Zero;
            lblResult.Text = "Поиск не дал результатов";
        }

        void CompleteMsg(IntPtr ptr)
        {
            savePtr = ptr;
            lblResult.Text = $"Дескриптор окна {ptr}";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var procName = string.IsNullOrWhiteSpace(textBox5.Text) ? null : textBox5.Text;
            var proc = procName != null ? Process.GetProcessesByName(procName).FirstOrDefault() : null;
            var ptr = proc != null ? proc.MainWindowHandle : IntPtr.Zero;
            if (ptr == IntPtr.Zero)
                FailMsg();
            else
                CompleteMsg(ptr);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var clName = string.IsNullOrWhiteSpace(textBox2.Text) ? null : textBox2.Text;
            var title = string.IsNullOrWhiteSpace(textBox1.Text) ? null : textBox1.Text;
            var parent = string.IsNullOrWhiteSpace(textBox3.Text) ? IntPtr.Zero : new IntPtr(int.Parse(textBox3.Text));
            var child = string.IsNullOrWhiteSpace(textBox4.Text) ? IntPtr.Zero : new IntPtr(int.Parse(textBox4.Text));
            var result = WinAPI.FindWindowEx(parent, child, clName, title);
            if (result == IntPtr.Zero)
                FailMsg();
            else
                CompleteMsg(result); 
        }

        private void УбитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItems = listView1.SelectedItems;
            if (selectedItems.Count > 0)
            {
                foreach (ListViewItem item in selectedItems)
                {
                    var id = int.Parse(item.SubItems[0].Text);
                    var proc = Process.GetProcessById(id);
                    if (proc != null)
                    {
                        proc.Kill();
                        listView1.Items.Remove(item);
                    }
                }
            }
        }
    }
}
