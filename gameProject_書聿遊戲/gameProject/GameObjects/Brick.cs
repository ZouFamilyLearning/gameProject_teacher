using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject.GameObjects
{
    class Brick : PictureBox
    {
        private int brickWidth, brickHeight;// 定義磚塊的長寬
        private Size clientSize;

        // 建構子
        public Brick(Point position, String uri, int brickWidth, int brickHeight, Size clientSize) : base()// 呼叫不帶參數父建構子
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;// 圖片填滿範圍

            this.brickWidth = brickWidth;
            this.brickHeight = brickHeight;
            this.clientSize = clientSize;

            this.Load(uri);
            SetBrickSize(position, brickWidth, brickHeight);// 設定設定位置
        }

        // 設定磚塊的位置
        public void SetPosition(Point position)
        {
            this.Location = new Point(position.X - brickWidth / 2, position.Y - brickHeight / 2);// 進行座標修正
        }
        // end

        // 設定磚塊的大小
        public void SetBrickSize(Point brickPosition, int brickWidth, int brickHeight)
        {
            this.Size = new Size(brickWidth, brickHeight);// 更新球的大小
            this.SetPosition(brickPosition);// 調整位子
        }
        // end
    }
}
