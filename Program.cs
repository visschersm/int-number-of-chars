// https://stackoverflow.com/a/51099524/2372288
// Extended with switch expression

using System.Diagnostics;

Console.WriteLine("\r\n.NET Core");

RunTests_Int32();
RunTests_Int64();

// Int32 Performance Tests:
void RunTests_Int32()
{
    Console.WriteLine("\r\nInt32");

    const int size = 100000000;
    int[] samples = new int[size];
    Random random = new Random((int)DateTime.Now.Ticks);
    for (int i = 0; i < size; ++i)
        samples[i] = random.Next(int.MinValue, int.MaxValue);

    Stopwatch sw1 = new Stopwatch();
    sw1.Start();
    for (int i = 0; i < size; ++i) DigitsInt_IfChain(samples[i]);
    sw1.Stop();
    Console.WriteLine($"IfChain: {sw1.ElapsedMilliseconds} ms");

    Stopwatch sw2 = new Stopwatch();
    sw2.Start();
    for (int i = 0; i < size; ++i) DigitsInt_Log10(samples[i]);
    sw2.Stop();
    Console.WriteLine($"Log10: {sw2.ElapsedMilliseconds} ms");

    Stopwatch sw3 = new Stopwatch();
    sw3.Start();
    for (int i = 0; i < size; ++i) DigitsInt_While(samples[i]);
    sw3.Stop();
    Console.WriteLine($"While: {sw3.ElapsedMilliseconds} ms");

    Stopwatch sw4 = new Stopwatch();
    sw4.Start();
    for (int i = 0; i < size; ++i) DigitsInt_String(samples[i]);
    sw4.Stop();
    Console.WriteLine($"String: {sw4.ElapsedMilliseconds} ms");

    Stopwatch sw5 = new Stopwatch();
    sw5.Start();
    for (int i = 0; i < size; ++i) DigitsInt_ComplexSwitch(samples[i]);
    sw5.Stop();
    Console.WriteLine($"Switch: {sw5.ElapsedMilliseconds} ms");


    // Start of consistency tests:
    Console.WriteLine("Running consistency tests...");
    bool isConsistent = true;

    // Consistency test on random set:
    for (int i = 0; i < samples.Length; ++i)
    {
        int s = samples[i];
        int a = DigitsInt_IfChain(s);
        int b = DigitsInt_Log10(s);
        int c = DigitsInt_While(s);
        int d = DigitsInt_String(s);
        int e = DigitsInt_ComplexSwitch(s);
        if (a != b || c != d || a != c || d != e || b != e)
        {
            Console.WriteLine($"Digits({s}): IfChain={a} Log10={b} While={c} String={d} Switch={e}");
            isConsistent = false;
            break;
        }
    }

    // Consistency test of special values:
    samples = new int[]
    {
                0,
                int.MinValue, -1000000000, -999999999, -100000000, -99999999, -10000000, -9999999, -1000000, -999999, -100000, -99999, -10000, -9999, -1000, -999, -100, -99, -10, -9, - 1,
                int.MaxValue, 1000000000, 999999999, 100000000, 99999999, 10000000, 9999999, 1000000, 999999, 100000, 99999, 10000, 9999, 1000, 999, 100, 99, 10, 9,  1,
    };
    for (int i = 0; i < samples.Length; ++i)
    {
        int s = samples[i];
        int a = DigitsInt_IfChain(s);
        int b = DigitsInt_Log10(s);
        int c = DigitsInt_While(s);
        int d = DigitsInt_String(s);
        int e = DigitsInt_ComplexSwitch(s);
        if (a != b || c != d || a != c || b != e || d != e)
        {
            Console.WriteLine($"Digits({s}): IfChain={a} Log10={b} While={c} String={d} Switch={e}");
            isConsistent = false;
            break;
        }
    }

    // Consistency test result:
    if (isConsistent)
        Console.WriteLine("Consistency tests are OK");
}

// Int64 Performance Tests:
void RunTests_Int64()
{
    Console.WriteLine("\r\nInt64");

    const int size = 100000000;
    long[] samples = new long[size];
    Random random = new Random((int)DateTime.Now.Ticks);
    for (int i = 0; i < size; ++i)
        samples[i] = Math.Sign(random.Next(-1, 1)) * (long)(random.NextDouble() * long.MaxValue);

    Stopwatch sw1 = new Stopwatch();
    sw1.Start();
    for (int i = 0; i < size; ++i) DigitsLong_IfChain(samples[i]);
    sw1.Stop();
    Console.WriteLine($"IfChain: {sw1.ElapsedMilliseconds} ms");

    Stopwatch sw2 = new Stopwatch();
    sw2.Start();
    for (int i = 0; i < size; ++i) DigitsLong_Log10(samples[i]);
    sw2.Stop();
    Console.WriteLine($"Log10: {sw2.ElapsedMilliseconds} ms");

    Stopwatch sw3 = new Stopwatch();
    sw3.Start();
    for (int i = 0; i < size; ++i) DigitsLong_While(samples[i]);
    sw3.Stop();
    Console.WriteLine($"While: {sw3.ElapsedMilliseconds} ms");

    Stopwatch sw4 = new Stopwatch();
    sw4.Start();
    for (int i = 0; i < size; ++i) DigitsLong_String(samples[i]);
    sw4.Stop();
    Console.WriteLine($"String: {sw4.ElapsedMilliseconds} ms");

    Stopwatch sw5 = new Stopwatch();
    sw5.Start();
    for (int i = 0; i < size; ++i) DigitsLong_ComplexSwitch(samples[i]);
    sw5.Stop();
    Console.WriteLine($"Switch: {sw5.ElapsedMilliseconds} ms");

    // Start of consistency tests:
    Console.WriteLine("Running consistency tests...");
    bool isConsistent = true;

    // Consistency test on random set:
    for (int i = 0; i < samples.Length; ++i)
    {
        long s = samples[i];
        int a = DigitsLong_IfChain(s);
        int b = DigitsLong_Log10(s);
        int c = DigitsLong_While(s);
        int d = DigitsLong_String(s);
        int e = DigitsLong_ComplexSwitch(s);
        if (a != b || c != d || a != c || e != b || e != d)
        {
            Console.WriteLine($"Digits({s}): IfChain={a} Log10={b} While={c} String={d} Switch={e}");
            isConsistent = false;
            break;
        }
    }

    // Consistency test of special values:
    samples = new long[]
    {
                0,
                long.MinValue, -1000000000000000000, -999999999999999999, -100000000000000000, -99999999999999999, -10000000000000000, -9999999999999999, -1000000000000000, -999999999999999, -100000000000000, -99999999999999, -10000000000000, -9999999999999, -1000000000000, -999999999999, -100000000000, -99999999999, -10000000000, -9999999999, -1000000000, -999999999, -100000000, -99999999, -10000000, -9999999, -1000000, -999999, -100000, -99999, -10000, -9999, -1000, -999, -100, -99, -10, -9, - 1,
                long.MaxValue, 1000000000000000000, 999999999999999999, 100000000000000000, 99999999999999999, 10000000000000000, 9999999999999999, 1000000000000000, 999999999999999, 100000000000000, 99999999999999, 10000000000000, 9999999999999, 1000000000000, 999999999999, 100000000000, 99999999999, 10000000000, 9999999999, 1000000000, 999999999, 100000000, 99999999, 10000000, 9999999, 1000000, 999999, 100000, 99999, 10000, 9999, 1000, 999, 100, 99, 10, 9,  1,
    };
    for (int i = 0; i < samples.Length; ++i)
    {
        long s = samples[i];
        int a = DigitsLong_IfChain(s);
        int b = DigitsLong_Log10(s);
        int c = DigitsLong_While(s);
        int d = DigitsLong_String(s);
        int e = DigitsLong_ComplexSwitch(s);
        if (a != b || c != d || a != c || e != b || e != d)
        {
            Console.WriteLine($"Digits({s}): IfChain={a} Log10={b} While={c} String={d} Switch={e}");
            isConsistent = false;
            break;
        }
    }

    // Consistency test result:
    if (isConsistent)
        Console.WriteLine("Consistency tests are OK");
}

int DigitsInt_ComplexSwitch(int n)
{
    return n switch
    {
        ((>= 0) and (< 10)) => 1,
        ((< 0) and (> -10)) or ((>= 10) and (< 100)) => 2,
        ((<= -10) and (> -100)) or ((>= 100) and (< 1000)) => 3,
        ((<= -100) and (> -1000)) or ((>= 1000) and (< 10000)) => 4,
        ((<= -1000) and (> -10000)) or ((>= 10000) and (< 100000)) => 5,
        ((<= -10000) and (> -100000)) or ((>= 100000) and (< 1000000)) => 6,
        ((<= -100000) and (> -1000000)) or ((>= 1000000) and (< 10000000)) => 7,
        ((<= -1000000) and (> -10000000)) or ((>= 10000000) and (< 100000000)) => 8,
        ((<= -10000000) and (> -100000000)) or ((>= 100000000) and (< 1000000000)) => 9,
        ((<= -100000000) and (> -1000000000)) or ((>= 1000000000)) => 10,
        _ => 11
    };
}

int DigitsLong_ComplexSwitch(long n)
{
    return n switch
    {
        ((>= 0) and (< 10)) => 1,
        ((< 0) and (> -10)) or ((>= 10) and (< 100)) => 2,
        ((<= -10) and (> -100)) or ((>= 100) and (< 1000)) => 3,
        ((<= -100) and (> -1000)) or ((>= 1000) and (< 10000)) => 4,
        ((<= -1000) and (> -10000)) or ((>= 10000) and (< 100000)) => 5,
        ((<= -10000) and (> -100000)) or ((>= 100000) and (< 1000000)) => 6,
        ((<= -100000) and (> -1000000)) or ((>= 1000000) and (< 10000000)) => 7,
        ((<= -1000000) and (> -10000000)) or ((>= 10000000) and (< 100000000)) => 8,
        ((<= -10000000) and (> -100000000)) or ((>= 100000000) and (< 1000000000)) => 9,
        ((<= -100000000) and (> -1000000000)) or ((>= 1000000000 and (< 10000000000))) => 10,
        ((<= -1000000000) and (> -10000000000)) or ((>= 10000000000 and (< 100000000000))) => 11,
        ((<= -10000000000) and (> -100000000000)) or ((>= 100000000000 and (< 1000000000000))) => 12,
        ((<= -100000000000) and (> -1000000000000)) or ((>= 1000000000000 and (< 10000000000000))) => 13,
        ((<= -1000000000000) and (> -10000000000000)) or ((>= 10000000000000 and (< 100000000000000))) => 14,
        ((<= -10000000000000) and (> -100000000000000)) or ((>= 100000000000000 and (< 1000000000000000))) => 15,
        ((<= -100000000000000) and (> -1000000000000000)) or ((>= 1000000000000000 and (< 10000000000000000))) => 16,
        ((<= -1000000000000000) and (> -10000000000000000)) or ((>= 10000000000000000 and (< 100000000000000000))) => 17,
        ((<= -10000000000000000) and (> -100000000000000000)) or ((>= 100000000000000000 and (< 1000000000000000000))) => 18,
        ((<= -100000000000000000) and (> -1000000000000000000)) or ((>= 1000000000000000000)) => 19,
        _ => 20
    };
}

int DigitsInt_IfChain(int n)
{
    if (n >= 0)
    {
        if (n < 10) return 1;
        if (n < 100) return 2;
        if (n < 1000) return 3;
        if (n < 10000) return 4;
        if (n < 100000) return 5;
        if (n < 1000000) return 6;
        if (n < 10000000) return 7;
        if (n < 100000000) return 8;
        if (n < 1000000000) return 9;
        return 10;
    }
    else
    {
        if (n > -10) return 2;
        if (n > -100) return 3;
        if (n > -1000) return 4;
        if (n > -10000) return 5;
        if (n > -100000) return 6;
        if (n > -1000000) return 7;
        if (n > -10000000) return 8;
        if (n > -100000000) return 9;
        if (n > -1000000000) return 10;
        return 11;
    }
}

int DigitsLong_IfChain(long n)
{
    if (n >= 0)
    {
        if (n < 10L) return 1;
        if (n < 100L) return 2;
        if (n < 1000L) return 3;
        if (n < 10000L) return 4;
        if (n < 100000L) return 5;
        if (n < 1000000L) return 6;
        if (n < 10000000L) return 7;
        if (n < 100000000L) return 8;
        if (n < 1000000000L) return 9;
        if (n < 10000000000L) return 10;
        if (n < 100000000000L) return 11;
        if (n < 1000000000000L) return 12;
        if (n < 10000000000000L) return 13;
        if (n < 100000000000000L) return 14;
        if (n < 1000000000000000L) return 15;
        if (n < 10000000000000000L) return 16;
        if (n < 100000000000000000L) return 17;
        if (n < 1000000000000000000L) return 18;
        return 19;
    }
    else
    {
        if (n > -10L) return 2;
        if (n > -100L) return 3;
        if (n > -1000L) return 4;
        if (n > -10000L) return 5;
        if (n > -100000L) return 6;
        if (n > -1000000L) return 7;
        if (n > -10000000L) return 8;
        if (n > -100000000L) return 9;
        if (n > -1000000000L) return 10;
        if (n > -10000000000L) return 11;
        if (n > -100000000000L) return 12;
        if (n > -1000000000000L) return 13;
        if (n > -10000000000000L) return 14;
        if (n > -100000000000000L) return 15;
        if (n > -1000000000000000L) return 16;
        if (n > -10000000000000000L) return 17;
        if (n > -100000000000000000L) return 18;
        if (n > -1000000000000000000L) return 19;
        return 20;
    }
}

// USING LOG10:
int DigitsInt_Log10(int n) =>
    n == 0 ? 1 : (n > 0 ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));

// WHILE LOOP:
int DigitsInt_While(int n)
{
    int digits = n < 0 ? 2 : 1;
    while ((n /= 10) != 0) ++digits;
    return digits;
}

// STRING CONVERSION:
int DigitsInt_String(int n) =>
    n.ToString().Length;

// USING LOG10:
int DigitsLong_Log10(long n) =>
    n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));

// WHILE LOOP:
int DigitsLong_While(long n)
{
    int digits = n < 0 ? 2 : 1;
    while ((n /= 10L) != 0L) ++digits;
    return digits;
}

// STRING CONVERSION:
int DigitsLong_String(long n) =>
    n.ToString().Length;