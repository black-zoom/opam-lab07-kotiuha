using System;
using System.Collections.Generic;

class Student
{
    public string Name;
    public string Group;
    public int Score;

    public Student(string name, string group, int score)
    {
        Name = name;
        Group = group;
        Score = score;
    }

    public override string ToString()
    {
        return $"{Name} ({Group}, {Score})";
    }
}

class Program
{
    const int N = 14;
    const int K = 1;

    static List<int> numbers = new List<int>();

    static List<string> words = new List<string>
    {
        "кіт", "програмування", "сонце", "будь-який",
        "алгоритм", "синьо-жовтий", "код", "делегат",
        "навчання", "рядок", "об'єкт", "узагальнення"
    };

    static List<Student> students = new List<Student>
    {
        new Student("Коваленко", "КН-3/1", 88),
        new Student("Мельник", "КН-3/2", 64),
        new Student("Іщенко", "КН-3/1", 93),
        new Student("Бондар", "КН-3/1", 71),
        new Student("Савченко", "КН-3/2", 55),
        new Student("Ткачук", "КН-3/2", 90),
        new Student("Кравець", "КН-3/1", 76),
        new Student("Іванів", "КН-3/1", 48),
        new Student("Мороз", "КН-3/2", 82),
        new Student("Шевчук", "КН-3/1", 67),
        new Student("Бойко", "КН-3/2", 95),
        new Student("Кушнір", "КН-3/2", 73)
    };

    static void Task1()
    {
        List<int> selectedNumbers =
            Algorithms.Filter(numbers, x => x <= 0);

        List<string> selectedWords =
            Algorithms.Filter(words, x => x.Contains("-"));

        List<Student> selectedStudents =
            Algorithms.Filter(students, x => x.Score >= 60 && x.Score <= 89);

        PrintList("Числа", selectedNumbers);
        PrintList("Рядки", selectedWords);
        PrintList("Студенти", selectedStudents);
    }

    static void Task2()
    {
        List<int> signs =
            Algorithms.Map(numbers, x => Math.Sign(x));

        List<char> firstLetters =
            Algorithms.Map(words, x => x.Length > 0 ? x[0] : '\0');

        List<int> scores =
            Algorithms.Map(students, x => x.Score);

        PrintList("Знаки чисел", signs);
        PrintList("Перші літери рядків", firstLetters);
        PrintList("Бали студентів", scores);
    }

    static void Task3()
    {
        int sum = Algorithms.Aggregate(
            numbers, 0, (state, x) => state + x);

        int product = Algorithms.Aggregate(
            numbers, 1, (state, x) => state * x);

        int? maximum = numbers.Count == 0
            ? null
            : Algorithms.Aggregate(
                numbers,
                int.MinValue,
                (state, x) => x > state ? x : state);

        (int Sum, int Count) averageState = Algorithms.Aggregate(
            numbers,
            (Sum: 0, Count: 0),
            (state, x) => (state.Sum + x, state.Count + 1));

        double average = averageState.Count == 0
            ? double.NaN
            : (double)averageState.Sum / averageState.Count;

        Console.WriteLine($"Сума: {sum}");
        Console.WriteLine($"Добуток: {product}");
        Console.WriteLine($"Максимум: {(maximum.HasValue ? maximum.Value.ToString() : "null")}");
        Console.WriteLine($"Середнє: {average}");
    }

    static void Task4()
    {
        Dictionary<string, List<Student>> byGroup =
            Algorithms.GroupBy(students, x => x.Group);

        Dictionary<char, List<Student>> bySurnameInitial =
            Algorithms.GroupBy(students, x => x.Name[0]);

        Dictionary<string, List<Student>> byScoreRange =
            Algorithms.GroupBy(students, x => GetScoreRange(x.Score));

        Dictionary<int, List<string>> wordsByLength =
            Algorithms.GroupBy(words, x => x.Length);

        PrintGroups("Студенти за групами", byGroup);
        PrintGroups("Студенти за першою літерою прізвища", bySurnameInitial);
        PrintGroups("Студенти за діапазоном балів", byScoreRange);
        PrintGroups("Рядки за довжиною", wordsByLength);
    }

    static void Task5()
    {
        int threshold = K;
        Func<int, bool> isGreaterThanThreshold = x => x > threshold;
        threshold = 1000;

        Console.WriteLine($"Змінений поріг: {isGreaterThanThreshold(2)}");

        List<Func<int>> forFunctions = new List<Func<int>>();

        for (int i = 0; i < 3; i++)
            forFunctions.Add(() => i);

        List<int> forResults = Algorithms.Map(
            forFunctions, function => function());

        PrintList("for", forResults);

        List<Func<int>> foreachFunctions = new List<Func<int>>();

        foreach (int i in new int[] { 0, 1, 2 })
            foreachFunctions.Add(() => i);

        List<int> foreachResults = Algorithms.Map(
            foreachFunctions, function => function());

        PrintList("foreach", foreachResults);

        List<Func<int>> fixedFunctions = new List<Func<int>>();

        for (int i = 0; i < 3; i++)
        {
            int copy = i;
            fixedFunctions.Add(() => copy);
        }

        List<int> fixedResults = Algorithms.Map(
            fixedFunctions, function => function());

        PrintList("Виправлений for", fixedResults);
    }

    static string GetScoreRange(int score)
    {
        if (score <= 59) return "0–59";
        if (score <= 74) return "60–74";
        if (score <= 89) return "75–89";
        return "90–100";
    }

    static void PrintList<T>(string label, List<T> items)
    {
        Console.WriteLine($"{label} ({items.Count}): {string.Join(", ", items)}");
    }

    static void PrintGroups<T, TKey>(
        string label,
        Dictionary<TKey, List<T>> groups) where TKey : notnull
    {
        Console.WriteLine(label);

        foreach (KeyValuePair<TKey, List<T>> group in groups)
            Console.WriteLine($"{group.Key}: {group.Value.Count}");
    }

    static void RunTests()
    {
        List<int> empty = new List<int>();

        bool filterEmpty = Algorithms.Filter(empty, x => x > 0).Count == 0;
        bool mapEmpty = Algorithms.Map(empty, x => x.ToString()).Count == 0;
        bool emptySum = Algorithms.Aggregate(empty, 0, (s, x) => s + x) == 0;
        bool emptyProduct = Algorithms.Aggregate(empty, 1, (s, x) => s * x) == 1;

        int? emptyMaximum = empty.Count == 0
            ? null
            : Algorithms.Aggregate(empty, int.MinValue, (s, x) => x > s ? x : s);

        bool maximumEmpty = emptyMaximum == null;

        (int Sum, int Count) emptyState = Algorithms.Aggregate(
            empty, (Sum: 0, Count: 0),
            (s, x) => (s.Sum + x, s.Count + 1));

        double emptyAverage = emptyState.Count == 0
            ? double.NaN
            : (double)emptyState.Sum / emptyState.Count;

        bool averageEmpty = double.IsNaN(emptyAverage);
        bool noMatches = Algorithms.Filter(new List<int> { 1, 2, 3 }, x => x < 0).Count == 0;

        Dictionary<int, List<int>> oneGroup =
            Algorithms.GroupBy(new List<int> { 1, 1, 1 }, x => x);

        bool groupOne = oneGroup.Count == 1 && oneGroup[1].Count == 3;

        List<int> single = new List<int> { 7 };

        bool singleElement =
            Algorithms.Aggregate(single, 0, (s, x) => s + x) == 7 &&
            Algorithms.Aggregate(single, 1, (s, x) => s * x) == 7 &&
            Algorithms.Aggregate(single, int.MinValue, (s, x) => x > s ? x : s) == 7;

        Console.WriteLine($"1. Filter empty: {filterEmpty}");
        Console.WriteLine($"2. Map empty: {mapEmpty}");
        Console.WriteLine($"3. Empty sum: {emptySum}");
        Console.WriteLine($"4. Empty product: {emptyProduct}");
        Console.WriteLine($"5. Empty maximum: {maximumEmpty}");
        Console.WriteLine($"6. Empty average: {averageEmpty}");
        Console.WriteLine($"7. Filter no matches: {noMatches}");
        Console.WriteLine($"8. GroupBy one group: {groupOne}");
        Console.WriteLine($"9. Single element: {singleElement}");
    }

    static void Main()
    {
        Random random = new Random(N);

        for (int i = 0; i < 20; i++)
            numbers.Add(random.Next(-K, K + 1));

        Task1();
        Task2();
        Task3();
        Task4();
        Task5();
        RunTests();
    }
}
