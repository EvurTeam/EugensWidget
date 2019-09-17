using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public class JokeItem : IParsingResultItem
    {
        private Control _control;

        public void SetViewer(Control control)
        {
            _control = control;
        }

        public void ShowResult()
        {
            var page = HtmlGrabber.GetDocument("https://www.anekdot.ru/random/anekdot/");
            var div = page.QuerySelectorAll(".topicbox").Skip(1).First().QuerySelector(".text");
            _control.Text = div.TextContent;
        }
    }
}
