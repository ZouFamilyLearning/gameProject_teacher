using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameProject.Controllers
{
    class MenuController
    {
        // Static variable
        private const int buttonWidth = 100;
        private const int buttonHeight = 40;

        public const int MENU_STATUS_START_CLICKED = 1;
        public const int MENU_STATUS_WAIT = 0;

        // Parent Panel
        private Form parentForm;

        // Menu Object
        private Button startBtn;

        // Menu Status
        private int menuStatus;

        public MenuController(Form form)
        {
            // Set Parent Form
            this.parentForm = form;

            // Set Menu Status
            this.menuStatus = MENU_STATUS_WAIT;
        }

        public void ShowMenu()
        {
            // clear form
            parentForm.Controls.Clear();
            // end

            // set up button
            startBtn = new Button();
            startBtn.Location = new System.Drawing.Point((parentForm.ClientSize.Width - buttonWidth) / 2,
                (parentForm.ClientSize.Height - buttonHeight) / 2);
            startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            startBtn.Text = "Start Game";
            startBtn.Click += new System.EventHandler(StartBtn_Click);

            // add object
            parentForm.Controls.Add(startBtn);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            menuStatus = MENU_STATUS_START_CLICKED;
        }

        public int GetMenuStatus()
        {
            int result = menuStatus;
            menuStatus = MENU_STATUS_WAIT;
            return result;
        }
    }
}
