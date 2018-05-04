using gameProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject
{
    public partial class Form1 : Form
    {
        // Status Define
        private const int SCREEN_STATUS_MENU = 0;
        private const int SCREEN_STATUS_GAME_NORMAL = 1;

        // Controllers
        private MenuController menuController;
        private GameController gameController;

        // Program Status Control
        private int screenStatus;


        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(800, 600);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Init Program Status
            screenStatus = SCREEN_STATUS_MENU;
            // end

            // Init Controllers
            gameController = new GameController(this);
            menuController = new MenuController(this);
            // end

            // Timer Start
            this.mainTimer.Start();
            // end

            // Show Menu
            menuController.ShowMenu();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // 主選單
            if (screenStatus == SCREEN_STATUS_MENU)
            {
                if(menuController.GetMenuStatus() == MenuController.MENU_STATUS_START_CLICKED)
                {
                    screenStatus = SCREEN_STATUS_GAME_NORMAL;
                    gameController.GameStart();
                }
                return;
            }
            // end

            // 遊戲內容
            if(screenStatus == SCREEN_STATUS_GAME_NORMAL)
            {
                if (gameController.Action(IOController.GetCursorPosition(this)) == GameController.GAME_STATUS_STOP)
                {
                    // 遊戲結束做的事
                    screenStatus = SCREEN_STATUS_MENU;
                    menuController.ShowMenu();
                }
                return;
            }
            // end
        }
    }
}
