using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject.Controllers
{
    class IOController
    {
        public static Point GetCursorPosition(Form clientWindow)
        {
            return clientWindow.PointToClient(Cursor.Position);// 轉換螢幕座標為視窗座標
        }
    }
}
