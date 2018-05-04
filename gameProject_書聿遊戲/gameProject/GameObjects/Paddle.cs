using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject.GameObjects
{
    class Paddle : PictureBox
    {
        private int paddleWidth, paddleHeight;// 定義球拍的長寬
        private Size clientSize;

        // 建構子
        public Paddle(Point position, String uri, int paddleWidth, int paddleHeight, Size clientSize) : base()// 呼叫不帶參數父建構子
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;// 圖片填滿範圍

            this.paddleWidth = paddleWidth;
            this.paddleHeight = paddleHeight;
            this.clientSize = clientSize;

            this.Load(uri);
            SetPaddleSize(position, paddleWidth, paddleHeight);// 設定設定位置
        }

        // 設定球拍的位置
        public void SetPosition(Point position)
        {
            this.Location = new Point(position.X - paddleWidth / 2, position.Y - paddleHeight / 2);// 進行座標修正
        }
        // end

        // 設定球拍的大小
        public void SetPaddleSize(Point paddlePosition, int paddleWidth, int paddleHeight)
        {
            this.Size = new Size(paddleWidth, paddleHeight);// 更新球的大小
            this.SetPosition(paddlePosition);// 調整位子
        }
        // end

        // 球拍的移動事件
        public void Move(Point position)
        {
            //球拍的移動
            if (position.X + this.Width / 2 >= clientSize.Width)// 右邊界判斷
            {
                this.Left = clientSize.Width - this.Width;
            }
            else if (position.X - this.Width / 2 <= 0)// 左邊界判斷
            {
                this.Left = 0;
            }
            else
            {
                this.Left = position.X - this.Width / 2; // 如果不在邊界上 球拍的水平位置 = 游標位置(減去1/2寬度置中)
            }
        }
    }
}
