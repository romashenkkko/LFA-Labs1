using System;
using System.Collections.Generic;
using System.Linq;

class Grammar
{
    private HashSet<char> VN;
    private HashSet<char> VT;
    private Dictionary<char, List<string>> P;
    private char S;
    private Random random = new Random();

    public Grammar()
    {
        VN = new HashSet<char> { 'S', 'A', 'B', 'C' };
        VT = new HashSet<char> { 'a', 'b', 'c', 'd' };
        S = 'S';
        P = new Dictionary<char, List<string>>
        {
            { 'S', new List<string> { "dA" } },
            { 'A', new List<string> { "aB", "b" } },
            { 'B', new List<string> { "bC", "d" } },
            { 'C', new List<string> { "cB", "aA" } }
        };
    }

    public string GenerateString()
    {
        List<char> sequence = new List<char> { S };
        while (sequence.Any(c => VN.Contains(c)))
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                char current = sequence[i];
                if (VN.Contains(current))
                {
                    var productions = P[current];
                    string chosenProduction = productions[random.Next(productions.Count)];

                    sequence.RemoveAt(i);
                    sequence.InsertRange(i, chosenProduction);
                    break;
                }
            }
        }
        return string.Concat(sequence);
    }

    public int GetChomskyType()
    {
        if (IsType3()) return 3;
        if (IsType2()) return 2;
        if (IsType1()) return 1;
        return 0;
    }

    private bool IsType3()
    {
        foreach (var rule in P)
        {
            foreach (var production in rule.Value)
            {
                bool singleTerminal = production.Length == 1 && VT.Contains(production[0]);
                bool terminalNonTerminal = production.Length == 2 && VT.Contains(production[0]) && VN.Contains(production[1]);
                if (!(singleTerminal || terminalNonTerminal)) return false;
            }
        }
        return true;
    }

    private bool IsType2()
    {
        return P.Keys.All(nonTerminal => VN.Contains(nonTerminal));
    }

    private bool IsType1()
    {
        foreach (var rule in P)
        {
            foreach (var production in rule.Value)
            {
                if (production.Length < rule.Key.ToString().Length)
                    return false;
            }
        }
        return true;
    }

    public string GetChomskyTypeDescription()
    {
        return GetChomskyType() switch
        {
            0 => "Type 0: Unrestricted Grammar",
            1 => "Type 1: Context-Sensitive Grammar",
            2 => "Type 2: Context-Free Grammar",
            3 => "Type 3: Regular Grammar",
            _ => "Unknown"
        };
    }

    public void TestClassification()
    {
        Console.WriteLine("Generated Strings:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(GenerateString());
        }
        Console.WriteLine("\nGrammar Type: " + GetChomskyTypeDescription());
    }
}


