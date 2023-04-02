using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public static class Page
    {
        private const string header = "<html>" +
            "<head><meta charset=utf-8></head>" +
            "<body>";

        private const string footer = "</body></html>";

        public const string Index = header +
            
            "<h1><strong>Main MATH Page&nbsp;</strong>➕</h1>" + 
            "<p>Добро пожаловать на самый математический сайт в мире!&nbsp;😎</p>" + 
            "<p>Тут вы сможете складывать числа!&nbsp;😍</p>" +
            footer;

        public const string Error = header +
            "<h1>Error</h1>" + footer;

        public const string Info = header +
            "<h1>Info</h1>" + footer;

        public static string Math = header + "Что-то пошло не так..." + footer;

        public static string Input = header +
            "<form> action=\"math\">" +
            "<label>A: </label>" +
            "<input type=\"text\" name=\"a\"><br>" +
            "<label>B: </label>" +
            "<input type=\"text\" name=\"b\"><br>" +
            "<input type=\"submit\">" +
            "</form>" +
            footer;

        public static string MathTemplate = header +
            "<h1>Math</h1>" +
            "<h2>{a} + {b} = {sum}</h2>" +
            footer;
    }
}
