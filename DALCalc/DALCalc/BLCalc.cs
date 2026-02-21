using System;

namespace DALCalc
{
    public class BLCalculator
    {
        public int AddNumbers(int a, int b)
        {
            DALCalculator dal = new DALCalculator();

            if (a < 0 || b < 0)
            {
                throw new Exception("Negative numbers not allowed");
            }

            return dal.Add(a, b);
        }
    }
}
