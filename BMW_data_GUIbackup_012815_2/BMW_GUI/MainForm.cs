﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*Added*/
using EEGDatabase;
using Emotiv;
using System.IO;
using System.Threading;
using System.Reflection;

namespace BMW_GUI
{

    public partial class MainForm : Form
    {

        #region Forms
        TrainingForm TF;
        ControlForm CF;

        #endregion


        #region GloabalVar

        public EEG_DataReader dataReader = new EEG_DataReader();
        Database db = new Database();

        #endregion
        public MainForm()
        {
            InitializeComponent();

        }

  


        private void button_submit_Click(object sender, EventArgs e)
        {
            button_submit.Enabled = false;
            Boolean operationContinue = true;
            //User is new

            //if user does not enter any text
            if (textBox_Name.Text == "")
            {
                label_warning.Text = "Please enter a name";
                operationContinue = false;
            }
        
            /*if user did enter name - 
            going through operation to check whether user is eligible to continue operation
             
             */
            else
            {
                //if user is new, check if the name user enters existed. If existed, do not proceed.
                if (checkBox_New.Checked == true)
                {
                    db.SetUserName(textBox_Name.Text);
                    Boolean userExist = db.CreateUser();
                    if (userExist)
                    {
                        label_warning.Text = "Name existed. \nPlease enter another name";
                        operationContinue=false;
                    }
 
                }
                else if (checkBox_New.Checked == false)
                {
                    db.SetUserName(textBox_Name.Text);
                    Boolean userExist = db.CreateUser();
                    if (!userExist)
                    {
                        label_warning.Text = "Name does not exist in the database. \nPlease select new user or try another name";
                        operationContinue = false;
                    }

                }


                if (operationContinue == true)
                {
                    switch (listBox_Mode.SelectedIndex)
                    {

                        //Training
                        case 0:
                            TF = new TrainingForm(dataReader, db, textBox_Name.Text);
                            TF.Show();

                            break;
                        //Control
                        case 1:
                            CF = new ControlForm();
                            CF.Show();
                            break;

                        default: break;
                    }
                }
            }

            button_submit.Enabled = true;







        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataReader.sessionOver(); 
            e.Cancel = true;

        }
  

    }
}
