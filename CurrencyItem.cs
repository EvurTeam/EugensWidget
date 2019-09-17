using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public class CurrencyItem : IParsingResultItem
    {
        private Control _control;

        public void ShowResult()
        {
            var page = HtmlGrabber.GetDocument("https://www.banki.ru/products/currency/cash");
            var parentBlocks = page.QuerySelectorAll(".currency-table__bordered-row");
            var eur = parentBlocks[0].QuerySelector("div.currency-table__large-text").TextContent;
            var usd = parentBlocks[1].QuerySelector("div.currency-table__large-text").TextContent;
            _control.Text = $"EUR: {eur}\nUSD: {usd}\n(обновлено в {DateTime.Now:hh:mm})";
        }

        public void SetViewer(Control control)
        {
            _control = control;
        }
    }
}
