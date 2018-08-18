using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_rockets
{
    public class Rocket
    {
        
        private int[] vArrx = new int[100];// מערך ה איקס ו ה y של האובייקט
        private int[] vArry = new int[100];
        private int x, y;
        private int id;// מספר זהות
        private double fitness;// כמה האובייקט קרוב למטרה
        private bool canMove,reachedGoal;
        public Rocket(int x, int y, int[] vArrx, int[] vArry)
        {
            for (int i = 0; i < vArrx.Length; i++)
            {
                this.vArrx[i] = vArrx[i];
                this.vArry[i] = vArry[i];
            }
            this.x = x;
            this.y = y;
            this.canMove = true;
            this.reachedGoal = false;
        }
        public void setFitness(double d)
        {
            this.fitness = 1 / d;
        }
        public double getFitness()
        {
            return this.fitness;
        }
        public void setid(int id)
        {
            this.id = id;
        }
        public int getid()
        {
            return this.id;
        }
        public int getvArrX(int i)
        {
            return this.vArrx[i];
        }
        public int getvArrY(int i)
        {
            return this.vArry[i];
        }
        public int[] getArrx()
        {
            return this.vArrx;
        }
        public int[] getArry()
        {
            return this.vArry;
        }
        public int getx()
        {
            return this.x;
        }
        public int gety()
        {
            return this.x;
        }

        public void setMove(bool Move)
        {
            this.canMove = Move;
        }
        public bool getMove()
        {
            return this.canMove;
        }
        public void setgoal(bool g)
        {
            this.reachedGoal = g;
        }
        public bool getGoal()
        {
            return this.reachedGoal;
        }

    }
}
