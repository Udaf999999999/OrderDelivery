namespace OrderDeliveryClassesConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Order<HomeDelivery,int>.maxProductCount);
            Console.WriteLine("Hello, World!");
        }
    }
}
