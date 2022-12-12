using System.Reflection;
using AdventOfCode.Utils;

namespace AdventOfCode.Calendar;

public sealed class Manager
{
    private readonly Dictionary<int, Solution> _solutions = new();

    public Manager()
    {
        var assembly = Assembly.GetAssembly(typeof(Program));
        if (assembly is null)
            throw new Exception("Could not find assembly of this project.");

        // Using Reflection search for every class with PuzzleSolution attribute
        // and add it to _solutions.
        foreach (var type in GetTypesAttribute(assembly, typeof(PuzzleSolutionAttribute)))
        {
            if (Activator.CreateInstance(type) is Solution solution)
                _solutions.Add(solution.Day, solution);
        }
    }

    public void Execute(int day)
    {
        if (day > _solutions.Count)
            throw new ArgumentOutOfRangeException(nameof(day), $"Day should be within range of 1-{_solutions.Count}");

        _solutions[day].Execute();
    }

    public void ExecuteAll()
    {
        foreach (var (_, solution) in _solutions)
            solution.Execute();
    }

    public void Benchmark(int day)
    {
        if (day > _solutions.Count)
            throw new ArgumentOutOfRangeException(nameof(day), $"Day should be within range of 1-{_solutions.Count}");

        _solutions[day].Benchmark();
    }

    public void BenchmarkAll()
    {
        foreach (var (_, solution) in _solutions)
            solution.Benchmark();
    }

    private static IEnumerable<Type> GetTypesAttribute(Assembly assembly, Type attribute) =>
        assembly.GetTypes().Where(type => type.GetCustomAttributes(attribute, true).Length > 0);
}
