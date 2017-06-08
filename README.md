This is the demo code from the Code Conversations episode: C#7 Features with Mads Torgersen

# CodeConversations

C# 7.0 features script

## Start

```csharp
using static System.Console;

classProgram
{
    staticvoid Main(string[] args)

    {
    }
}
```

## Tuples

A Fibonacci number is the sum of the two previous Fibonacci numbers.

```csharp
    static int Fibonacci(int n)
    {
    }
```

An efficient implementation uses a recursive helper that returns the _two_ latest numbers. We do that with tuples:

```csharp
    static(int, int) Fib(int i)
    {
    }
```

This is a _tuple_ type, denoting two ints. For clarity we can give the elements descriptive names:

```csharp
    static (intcurr, intprev) Fib(int i)
```

We encode a base case with a tuple literal:

```csharp
        if (i == 0) return(1, 0);
```

Otherwise we need a recursive call:

```csharp
        var t = Fib(i - 1);
```

We can use the descriptive names to dot into the returned tuple, in order to create the values of our own return tuple:

```csharp
        return (t.curr + t.prev, t.curr);
```

## Deconstruction

Another approach is to _deconstruct_ the tuple returned from the recursive call directly into new variables:

```csharp
        var (c, p) = Fib(i - 1);

        return (c + p, c);
```

_Full Fib method:_

```csharp
    static (int curr, int prev) Fib(int i)

    {

        if (i == 0) return (1, 0);

        var (c, p) = Fib(i - 1);

        return (c + p, c);

    }
```

We can now call the helper from the main Fibonacci method:

```csharp
        return Fib(n).curr;
```

But we can also deconstruct our way to the result, using a wildcard:

```csharp
        (var r, \_) = Fib(n);

        return r;
```

## Local functions

Fib is a helper method, that is only useful within the Fibonacci method itself. It can be declared as a _local function_ inside the body of the Fibonacci method:

```csharp
    static int Fibonacci(int n)
    {
        (var r, \_) = Fib(n);
        return r;
        
        (int curr, int prev) Fib(int i)
        {
            if (i == 0) return (1, 0);
            var (c, p) = Fib(i - 1);
            return (c + p, c);
        }
    }
```

Local variables from the enclosing method, such as n and r are in scope in the local function. Though we don&#39;t make use of that here, that is often useful.

## Pattern matching

Let's change it to a &quot;TryFibonacci&quot; method, that takes any object and tries to make an int out of it.

```csharp
    static bool TryFibonacci(object o, outint r)

    {

        (r, \_) = Fib(n);

        return true;
```

Our job now is to see if we can get an n from the o that was passed in. _Pattern matching_ can help with that:

```csharp
        if (o is int n)
        {
            (r, \_) = Fib(n);
            returntrue;
        }
```

This is a so-called type pattern. Apart from is-expressions it can also be used in cases of switch statements to switch on the type of an incoming object.

The pattern introduces a variable in the middle of an expression. Such a variable is called an _expression variable_, and is like any other local variable. For instance you can assign to it. Let&#39;s augment this to work on strings that parse to ints:

        if (o isint n || o isstring s &amp;&amp; int.TryParse(s, out n))

Adding fallback behavior, here is the full TryFibonacci method:

```csharp
    static bool TryFibonacci(object o, outint r)
    {
        if (o isint n || o isstring s &amp;&amp; int.TryParse(s, out n))
        {
            (r, \_) = Fib(n);
            return true;
        }

        r = 0;

        return false;

        (int curr, int prev) Fib(int i)
        {
            if (i == 0) return (1, 0);
            var (c, p) = Fib(i - 1);
            return (c + p, c);
        }
    }
```

## Out variables

Let's end by calling TryFibonacci from the Main method. As another form of expression variable, we can use freshly declared _out variables_ directly as an out parameter:

```csharp
    static void Main(string[] args)
    {
        if (TryFibonacci("11", outint r)) WriteLine(r);
    }
```

You can even declare out variables with &quot;var&quot;, since the type can be inferred from the method signature:

```csharp
        if (TryFibonacci("11", outvar r)) WriteLine(r);
```

## Full listing

```csharp
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
```
