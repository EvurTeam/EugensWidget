using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EugensWidget
{
    public class StyleTheme
    {
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }

        public static readonly StyleTheme DefaultTheme = new StyleTheme(Color.SteelBlue, SystemColors.ButtonHighlight);
        public static readonly StyleTheme LightTheme = new StyleTheme(Color.White, Color.Black);
        public static readonly StyleTheme NightTheme = new StyleTheme(Color.Black, Color.White);

        public StyleTheme(Color back, Color fore)
        {
            BackColor = back;
            ForeColor = fore;
        }
    }
}
