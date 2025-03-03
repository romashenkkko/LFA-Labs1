class Program
{
    static void Main()
    {
        Grammar grammar = new Grammar();
        Console.WriteLine("Generated Strings:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(grammar.GenerateString());
        }

        FiniteAutomaton automaton = grammar.ToFiniteAutomaton();
        Console.WriteLine("Checking language membership:");
        Console.WriteLine("dab belongs to language: " + automaton.StringBelongToLanguage("dab"));
        Console.WriteLine("dabc belongs to language: " + automaton.StringBelongToLanguage("dabc"));
        Console.WriteLine("dcac belongs to language: " + automaton.StringBelongToLanguage("dcac"));
        Console.WriteLine("dbac belongs to language: " + automaton.StringBelongToLanguage("dbac"));
        Console.WriteLine("dca belongs to language: " + automaton.StringBelongToLanguage("dca"));


    }
}
