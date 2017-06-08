using static System.Console;

class Program
{
    static bool TryFibonacci(object o, out int r)
    {
        if (o is int n || o is string s && int.TryParse(s, out n))
        {
            (r, _) = Fib(n);
            return true;
        }
        r = 0;
        return false;

        (int curr, int prev) Fib(int i)
        {
            if (i == 0) return (1, 0);
            var (c, p) = Fib(i - 1);
            return (curr: c + p, prev: c);
        }
    }
    static void Main(string[] args)
    {
        if (TryFibonacci("11", out var r)) WriteLine(r);
    }
}