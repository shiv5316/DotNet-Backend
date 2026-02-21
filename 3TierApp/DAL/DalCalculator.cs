namespace DAL
{
    public class DalCalculator
    {
        //public int Add(int a, int b)
        //{
        //    return a + b;
        //}


        //String Reverse
        public string ReverseString(string input)
        {
            char[] arr=input.ToCharArray();
            System.Array.Reverse(arr);
            return new string(arr);
        }
    }
}
