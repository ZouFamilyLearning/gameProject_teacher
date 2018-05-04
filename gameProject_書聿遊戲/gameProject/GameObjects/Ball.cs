using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject.GameObjects
{
    class Ball : PictureBox
    {
        private Point ballPosition;// 定義球的座標(圖片的中心)
        private int xSpeed, ySpeed;// 定義球在平面上的x,y軸速度
        private int ballSize;// 定義球的大小
        private Size clientSize;
        private System.Media.SoundPlayer collisionSoundPlayer;

        // 建構子
        public Ball(Point position, String uri, int xSpeed, int ySpeed, int ballSize, Size clientSize) : base()// 呼叫不帶參數父建構子
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;// 圖片填滿範圍

            this.ballPosition = position;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.ballSize = ballSize;
            this.clientSize = clientSize;

            this.Load(uri);
            SetBallSize(ballSize);// 設定球的大小/設定位置
            // SetLocation(x, y);
            collisionSoundPlayer = new System.Media.SoundPlayer();
            collisionSoundPlayer.SoundLocation = "Audios/sound_collision.wav";
        }

        // 設定球的位置
        public void SetPosition(Point position)
        {
            this.Location = new Point(position.X - ballSize / 2, position.Y - ballSize / 2);// 進行座標修正
        }
        // end

        // 設定球的大小
        public void SetBallSize(int ballSize)
        {
            this.Size = new Size(ballSize, ballSize);// 更新球的大小
            this.SetPosition(ballPosition);// 調整位子
        }
        // end

        // 球的移動事件 => return 是否落出範圍
        public bool Move()
        {
            // 調整位置
            this.Left += xSpeed;
            this.Top += ySpeed;
            this.ballPosition.X += xSpeed;
            this.ballPosition.Y += ySpeed;
            // 碰撞邊界
            if (this.ballPosition.X - ballSize / 2 <= 0// left
                || this.ballPosition.X + ballSize / 2 >= clientSize.Width)// right
            {
                xSpeed = -xSpeed;
            }
            else if (this.ballPosition.Y - ballSize / 2 <= 0) // top
            {
                ySpeed = -ySpeed;
            }
            else if (this.ballPosition.Y + ballSize / 2 >= clientSize.Height)// bot
            {
                return true;
            }
            return false;
        }
        // end

        // 判斷碰撞 => return 是否碰撞
        public bool IsCollision(PictureBox picturebox) // 丟入pictureBox 因此對PictureBox的座標處理無需做座標修正
        {
            if (this.ballPosition.X - ballSize / 2 > picturebox.Location.X + picturebox.Width)// 球的左方大於圖片的右方 == 沒有碰撞
            {
                return false;
            }
            if (this.ballPosition.Y - ballSize / 2 > picturebox.Location.Y + picturebox.Height)// 球的上方大於圖片的下方 == 沒有碰撞
            {
                return false;
            }
            if (this.ballPosition.X + ballSize / 2 < picturebox.Location.X)// 球的右方小於圖片的左方 == 沒有碰撞
            {
                return false;
            }
            if (this.ballPosition.Y + ballSize / 2 < picturebox.Location.Y)// 球的下方小於圖片的上方 == 沒有碰撞
            {
                return false;
            }

            // 計算四個方向的位移量，用來判斷哪個邊界被碰撞
            // 求出相對位移最小的部分，由最小部分來碰撞
            int topDistance = (this.ballPosition.Y + ballSize / 2) - picturebox.Location.Y;
            int botDistance = (picturebox.Location.Y + picturebox.Height) - (this.ballPosition.Y - ballSize / 2);
            int leftDistance = (this.ballPosition.X + ballSize / 2) - picturebox.Location.X;
            int rightDistance = (picturebox.Location.X + picturebox.Width) - (this.ballPosition.X - ballSize / 2);

            if (topDistance < leftDistance && topDistance < rightDistance) // 如果上面被碰撞，距離會比左右兩側皆小
            {
                ySpeed = -ySpeed;
            }
            else if(botDistance < leftDistance && botDistance < rightDistance)// 如果下方被碰撞，距離會比左右兩側皆小)
            {
                ySpeed = -ySpeed;
            }
            else if(leftDistance > rightDistance)// 左邊被碰撞
            {
                xSpeed = -xSpeed;
            }
            else // 右邊被碰撞
            {
                xSpeed = -xSpeed;
            }

            // collision sound
            collisionSoundPlayer.Play();
            // end
            return true;
        }
    }
}
