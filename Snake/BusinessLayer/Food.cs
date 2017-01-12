﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Food
    {
        public const int BUFFER = 1;
        public int yLocation { get; set; }
        public int xLocation { get; set; }
        private enum foodType
        {
            basic
        }
        foodType type = foodType.basic;

        public Food()
        {
            xLocation = -1;
            yLocation = -1;
            type = foodType.basic;
        }

        public bool generateFood(int xBorder, int yBorder)
        {
            if(xBorder < 0 || yBorder < 0)
            {
                return false; // Negative number input
            }
            int xLocation = randomGenerate(xBorder);
            int yLocation = randomGenerate(yBorder);

            return true;
        }

        private int randomGenerate(int numberLimit)
        {
            Random randomNumber = new Random();
            return randomNumber.Next(numberLimit + BUFFER);
        }


    }
}
