using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace smart_rockets
{
    public partial class Form1 : Form
    {

        int width = 600, height = 400;
        int len = 100; // אורך המערכים בתכנית
        PictureBox obstacle;// מכשול
        PictureBox[] r;// מערך של קוביות שבעזרתן אני מקבל נתונים לאובייקט רוקט
        Rocket[] rockets;// מערך מסוג רוקט
        int[] vArrx, vArry;// מערכים של ערכים ל x ו y בשביל האובייקטים רוקט
        Random rnd;
        int counter = 0,counter2 = 0;
        PictureBox target;
        List<Rocket> matingpool;
        public Form1()
        {
            InitializeComponent();
            //השמה של כל הנתונים והאובייקטים הנדרשים
            this.Width = 1920;
            this.Height = 1080;
            #region obstacle
            obstacle = new PictureBox();
            obstacle.Width = 300;
            obstacle.Height = 20;
            obstacle.BackColor = Color.Black;
            obstacle.Location = new Point(this.Width / 2 - obstacle.Width / 2, this.Height / 2 - obstacle.Height);
            this.Controls.Add(obstacle);
            #endregion
            #region rockets
            r = new PictureBox[len];
            rockets = new Rocket[len];
            vArrx = new int[len];
            vArry = new int[len];
            rnd = new Random();
            for (int i = 0; i < r.Length; i++)
            {
                r[i] = new PictureBox();
                r[i].Width = 10;
                r[i].Height = 10;
                r[i].BackColor = Color.Red;
                r[i].Left = this.obstacle.Left + 100;
                r[i].Top = this.obstacle.Top + 100 ;
                for (int j = 0; j < vArrx.Length; j++)
                {
                    // נתינת ערכים אקראיים לשני המערכים
                    
                    vArrx[j] = rnd.Next(-10, 11);

                    vArry[j] = rnd.Next(-10,11);
                }
                rockets[i] = new Rocket(r[i].Left, r[i].Top, vArrx, vArry);
                rockets[i].setid(i);//השמת מספר הזהות של כל רוקט
                this.Controls.Add(r[i]);
            }
            #endregion
            #region target
            target = new PictureBox();
            target.Width = 50;
            target.Height = 50;
            target.Left = this.Width / 2 - target.Width/2;
            target.Top = 0;
            target.BackColor = Color.Black;
            this.Controls.Add(target);
            #endregion


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //טיימר שאחראי לתזוזה בציר ה x 
            for (int i = 0; i < vArrx.Length; i++)
            {
                for (int j = 0; j < Math.Abs(rockets[i].getvArrX(i)); j++)
                {
                    if (/*!r[i].Bounds.IntersectsWith(obstacle.Bounds)*/ true && rockets[i].getMove())
                    {
                        if (rockets[i].getvArrX(i) > 0)
                        {
                            r[i].Left++;
                        }
                        else if (rockets[i].getvArrX(i) < 0)
                        {
                            r[i].Left--;
                        }
                    }
                    else if(rockets[i].getMove())
                    {
                        if (rockets[i].getvArrX(i) > 0)
                        {
                            r[i].Left--;
                        }
                        else if (rockets[i].getvArrX(i) < 0)
                        {
                            r[i].Left++;
                        }
                    }
                    if (counter2 == rockets.Length)
                    {
                        this.counter2 = 0;
                    }
                    if (r[counter2].Bounds.IntersectsWith(target.Bounds))
                    {
                        this.rockets[counter2].setFitness(rockets[counter2].getFitness() * 10);
                        this.rockets[counter2].setMove(false);
                    }
                    this.counter2++;
                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //טיימר שאחראי על תזוזה בציר ה y
            for (int i = 0; i < vArry.Length; i++)
            {
                for (int j = 0; j < Math.Abs(rockets[i].getvArrY(i)); j++)
                {
                    if (/*!r[i].Bounds.IntersectsWith(obstacle.Bounds)*/ true && rockets[i].getMove())
                    {
                        if (rockets[i].getvArrY(i) > 0)
                        {
                            r[i].Top++;
                        }
                        else if (rockets[i].getvArrY(i) < 0)
                        {
                            r[i].Top--;
                        }
                    }
                   else if (rockets[i].getMove())
                    {
                        if (rockets[i].getvArrY(i) > 0)
                        {
                            r[i].Top--;
                        }
                        else if (rockets[i].getvArrY(i) < 0)
                        {
                            r[i].Top++;
                        }
                    }
                    if (counter2 == rockets.Length)
                    {
                        this.counter2 = 0;
                    }
                    if (r[counter2].Bounds.IntersectsWith(target.Bounds))
                    {
                        this.rockets[counter2].setFitness(rockets[counter2].getFitness() * 10);
                        this.rockets[counter2].setMove(false);
                        this.rockets[counter2].setFitness(this.rockets[counter2].getFitness() * 10);
                     }
                    this.counter2++;
                }
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label1.Text = "time: " + counter.ToString();
            //טיימר שקורא לפעולה ריסט כל 20 שניות
            if (counter == 20)
            {
                reset();
                counter = 0;
            }
            this.counter++;
        }
        public void reset()
        {
            
            //פעולה שמאפסת את כל הנתונים בתכנית למצב ההתחלתי שלהם
            this.timer1.Enabled = false;
            this.timer2.Enabled = false;
            //Blabla

            Calcfitness();
            //v2
            //v3
            Evaluate();
            Matingpool();
            for (int i = 0; i < rockets.Length; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1["Column1", i].Value = i.ToString();
                dataGridView1["Column2", i].Value = rockets[i].getFitness();
            }

            dataGridView1.Sort(Column2, ListSortDirection.Descending);
            Rocket[] newrockets = Selection();
            for (int i = 0; i < rockets.Length; i++)
            {
                rockets[i] = newrockets[i];
                rockets[i].setid(i);
            }
            for (int i = 0; i < rockets.Length; i++)
            {
                r[i].Left = rockets[i].getx();
                r[i].Top = rockets[i].gety();
            }
            this.timer1.Enabled = true;
            this.timer2.Enabled = true;
        }
        public void Calcfitness()
        {
            double x, y;
            for (int i = 0; i < r.Length; i++)
            {
                x = (this.target.Left + this.target.Width / 2) - this.r[i].Left;
                y = (this.target.Height + this.target.Height / 2) - this.r[i].Top;

                this.rockets[i].setFitness(Math.Floor(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2))));
            }
        }
        public void Evaluate()
        {
            double maxfit = 0;
            for (int i = 0; i < rockets.Length; i++)
            {
                maxfit = Math.Max(maxfit, rockets[i].getFitness());
            }
            for (int i = 0; i < rockets.Length; i++)
            {
                this.rockets[i].setFitness(rockets[i].getFitness() / maxfit);
            }
        }

        public void Matingpool()
        {
            this.matingpool = new List<Rocket>();
            for (int i = 0; i < rockets.Length; i++)
            {
                double n = this.rockets[i].getFitness() * 100;
                for (int j = 0; j < n; j++)
                {
                    this.matingpool.Add(this.rockets[i]);
                }
            }
        }


        public Rocket[] Selection()
        {
            Rocket[] newrockets = new Rocket[this.rockets.Length];
            rnd = new Random();
            for (int i = 0; i < this.rockets.Length; i++)
            {
                Rocket parentA = this.matingpool[rnd.Next(this.matingpool.Count)];
                Rocket parentB = this.matingpool[rnd.Next(this.matingpool.Count)];
                newrockets[i] = CrossOver(parentA, parentB);
            }
            return newrockets;

        }
        public Rocket CrossOver(Rocket parentA, Rocket parentB)
        {
            int midpoint = this.rnd.Next(parentA.getArrx().Length);
            int[] arrx = new int[parentA.getArrx().Length];
            int[] arry = new int[parentA.getArrx().Length];
            for (int i = 0; i < parentA.getArrx().Length; i++)
            {
                if (i > midpoint)
                {
                    arrx[i] = parentA.getvArrX(i);
                }
                else
                {
                    arrx[i] = parentB.getvArrX(i);
                }
            }
            for (int i = 0; i < parentA.getArry().Length; i++)
            {
                if (i > midpoint)
                {
                    arry[i] = parentA.getvArrY(i);
                }
                else
                {
                    arry[i] = parentB.getvArrY(i);
                }
            }
            return new Rocket(this.obstacle.Left + 100, this.obstacle.Top + 100, arrx, arry);
        }
    }
}