public class MyClass
{
    public static void Main(string[] args)
    {

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine($"count of even numbers: {evenNumbers.Count()}");
        if (evenNumbers.Count() > 0)
        {
            Console.WriteLine("Even numbers are:");
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No even numbers found");
        }
    }
}