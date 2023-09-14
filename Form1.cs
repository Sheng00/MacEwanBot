using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V114.CSS;
using OpenQA.Selenium.Edge;
using System.Threading;

namespace MacEwan_Auto_scheduler
{
    public partial class Form1 : Form
    {

        string buildData = "Assignments Due\n---------------------\n";
        public int index = 0;

        IWebDriver driver;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            driver = new EdgeDriver();
            driver.Url = "https://meskanas.macewan.ca/my/courses.php";
            driver.Navigate();
            IWebElement loginElement = driver.FindElement(By.ClassName("login-identityprovider-btn"));
            loginElement.Click();
            IWebElement user = driver.FindElement(By.Id("username"));
            IWebElement pass = driver.FindElement(By.Id("password"));
            IWebElement login = driver.FindElement(By.ClassName("form-button"));

            user.Click();
            user.SendKeys(textBox1.Text);
            pass.Click();
            pass.SendKeys(textBox2.Text);
            login.Click();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                var element = driver.FindElements(By.ClassName("dashboard-card-img"));


                for (int i = 0 + index; i < element.Count; i++)
                {

                    //Thread.Sleep(1000);
                    element = driver.FindElements(By.ClassName("dashboard-card-img"));
                    var names = driver.FindElements(By.ClassName("multiline"));
                    string className = names[i].Text;

                    // Click class and scrape assignments
                    element[i].Click();

                    var assignments = driver.FindElements(By.ClassName("instancename"));


                    //var assignment_selector_datas = driver.FindElements(By.ClassName("sectionname"));
                    if (driver.PageSource.Contains("Assignments"))
                    {
                        //for (int y = 0; y < assignment_selector_datas.Count; y++)
                        {
                            //string datas = assignment_selector_datas[y].Text.ToLower();
                            //if (datas.Contains("assignment") || datas == "assignment" || datas.Contains("lab") || datas.Contains("qui"))
                            {

                                richTextBox1.Text += "Assignments SECTION FOUND in " + className + "\n";
                                //for (int x = 0; x < assignments.Count; x++)
                                {
                                    /*if (assignments[x].Text.Contains("Available"))
                                    {
                                        richTextBox1.Text += $"\n{className}\n---------------------\n{assignments[x].Text}";
                                    }*/
                                    //else
                                    //{
                                    /*assignments = driver.FindElements(By.ClassName("instancename"));
                                    string txt = assignments[x].Text.ToLower();
                                    if (txt.Contains("assignment") || txt.Contains("questions") || txt.Contains("quiz") || txt.Contains("lab"))
                                    {
                                        richTextBox1.Text += $"\n{assignments[x].Text}";
                                    }*/


                                    var possibleAssignments = driver.FindElements(By.ClassName("activityname"));

                                    string curURL = driver.Url;

                                    for (int z = 0; z < possibleAssignments.Count; z++)
                                    {
                                        //Thread.Sleep(300);
                                        possibleAssignments = driver.FindElements(By.ClassName("activityname"));
                                        string name_ = possibleAssignments[z].Text;
                                        if (name_.ToLower().Contains("lab") || name_.ToLower().Contains("assignmen") || name_.ToLower().Contains("qui") || name_.ToLower().Contains("projec") || name_.ToLower() == "lab")
                                        {
                                            possibleAssignments[z].Click();
                                            richTextBox1.Text += $"\n{name_}\n------------\n";
                                            string DueDateAndInformation = "";
                                            string opt_1 = "no-overflow";
                                            string opt_2 = "description-inner";
                                            string opt_3 = "activity-dates";

                                            try
                                            {
                                                DueDateAndInformation = driver.FindElement(By.ClassName(opt_1)).Text;
                                                richTextBox1.Text += DueDateAndInformation + "\n";
                                            }
                                            catch (Exception ex)
                                            {
                                                richTextBox1.Text += $"\nNo information\n";
                                            }

                                            
                                                try
                                                {
                                                    DueDateAndInformation = driver.FindElement(By.ClassName(opt_2)).Text;
                                                richTextBox1.Text += DueDateAndInformation + "\n";

                                            }
                                            catch (Exception ex)
                                                {
                                                    //richTextBox1.Text += $"\nNo information\n";
                                                }

                                            try
                                            {
                                                DueDateAndInformation = driver.FindElement(By.ClassName(opt_3)).Text;
                                                richTextBox1.Text += DueDateAndInformation + "\n";

                                            }
                                            catch (Exception ex)
                                            {
                                                //richTextBox1.Text += $"\nNo information\n";
                                            }


                                        }
                                        driver.Url = curURL;
                                        driver.Navigate();
                                        // }




                                    }
                                    // }



                                }
                            }
                        }
                        /* else
                         {
                             richTextBox1.Text += "No assigments in " + className;

                         }*/


                    }
                    driver.Url = "https://meskanas.macewan.ca/my/courses.php";
                    driver.Navigate();
                    index++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR... SCRAPE AGAIN!");
                index--;

            }

            }
        }
    }
