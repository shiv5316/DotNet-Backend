using DAL;

namespace BL
{
    public class BLCalc
    {
        //public int AddNumbers(int a, int b)
        //{
        //    DalCalculator dal = new DalCalculator();
        //    return dal.Add(a, b);
        //}


        //String Reverse
        public string Reverse(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                throw new Exception("String cannot be empty");
            }
            DalCalculator dal=new DalCalculator();
            return dal.ReverseString(text);
        }
    }
}
