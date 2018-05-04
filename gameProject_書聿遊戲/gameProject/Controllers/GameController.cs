using gameProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace gameProject.Controllers
{
    class GameController
    {
        // static variable 置於最上方
        private const int BRICK_NUMBER = 40;

        public const int GAME_STATUS_IN_PROGRESS = 1;
        public const int GAME_STATUS_STOP = 0; 

        //Parent Panel
        private Form parentForm;

        // Game Object
        private Paddle paddle;
        private Ball ball;
        private List<Brick> bricks;

        // Game Status
        private int gameStatus; // 考量未來加入其餘狀態因此使用int

        public GameController(Form form)
        {
            // Set Parent Form
            this.parentForm = form;

            // Set Game Status
            this.gameStatus = GAME_STATUS_STOP;
        }

        public void GameStart()
        {
            // clear form
            parentForm.Controls.Clear();
            // end

            // Init Paddle
            paddle = new Paddle(
                new Point(
                    (parentForm.ClientSize.Width - 104) / 2,
                    parentForm.ClientSize.Height - 24 * 3
                ),
                "Images//paddleBlue.png",
                104,
                24,
                parentForm.ClientSize
            );
            // 將此圖片置於Form中
            // end

            // Init Ball
            ball = new Ball(
                new Point(
                    (parentForm.ClientSize.Width - 20) / 2,
                    parentForm.ClientSize.Height / 2
                ),
                "Images//ballGrey.png",
                6,
                7,
                22,
                parentForm.ClientSize
            );
            // end

            // Init Brick
            bricks = new List<Brick>();
            int brickWidth = parentForm.ClientSize.Width / 16;
            int brickHeight = parentForm.ClientSize.Width / 32;

            for (int i = 0; i < BRICK_NUMBER; i++)
            {
                bricks.Add(new Brick(
                    new Point(
                        brickWidth + (i % 8) * brickWidth * 2,
                        brickHeight + (i / 8) * brickHeight * 2
                    ),
                    "Images//brickGrey.png",
                    brickWidth,
                    brickHeight,
                    parentForm.ClientSize
                ));
            }
            // end

            // add object
            parentForm.Controls.Add(paddle);
            parentForm.Controls.Add(ball);
            for(int i = 0; i < BRICK_NUMBER; i++)
            {
                parentForm.Controls.Add(bricks[i]);
            }
            // end

            // Game Status Start
            this.gameStatus = GAME_STATUS_IN_PROGRESS;
        }

        // 執行遊戲中的動作 => return 遊戲當前的狀態
        public int Action(Point cursorPosition)
        {
            // if game over, block
            if (gameStatus == GAME_STATUS_STOP)
            { 
                return gameStatus;
            }

            // paddle act
            paddle.Move(cursorPosition);
            // end

            // ball act
            if(ball.Move())
            {
                gameStatus = GAME_STATUS_STOP;
            }
            // end

            // ball/paddle collision
            ball.IsCollision(paddle);
            // end

            // ball/brick collision
            for(int i = 0; i < bricks.Count; i++) // 個數會改變 不能使用常數
            {
                if(ball.IsCollision(bricks[i]))
                {
                    // 如果碰撞了，移除方塊
                    parentForm.Controls.Remove(bricks[i]);
                    bricks.Remove(bricks[i]);
                    // end
                }
            }
            // end

            return gameStatus;
        }
    }
}
