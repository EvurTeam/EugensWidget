using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public class WeatherItem : IParsingResultItem
    {
        private Control _control;

        public void ShowResult()
        {
            var page = HtmlGrabber.GetDocument("https://yandex.ru/search/?text=погода%20сегодня");
            var t = page.QuerySelector(".weather-forecast__current-temp").TextContent;
            var w_node = page.QuerySelector(".weather-forecast__current-details");
            var sb = new StringBuilder(w_node.TextContent);
            foreach (var child in w_node.Children)
            {
                sb = sb.Replace(child.TextContent, "");
            }
            _control.Text = $"{t} C° {sb} (обновлено в {DateTime.Now:HH:mm})";
        }

        public void SetViewer(Control control)
        {
            _control = control;
        }
    }
}
