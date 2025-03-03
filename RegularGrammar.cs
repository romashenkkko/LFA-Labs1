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
        string result = "";
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
                    sequence.InsertRange(i, chosenProduction.ToCharArray());
                    break; 
                }
            }
        }

        return string.Concat(sequence);
    }


    public FiniteAutomaton ToFiniteAutomaton()
    {
        return new FiniteAutomaton(VN, VT, P, S);
    }
}

class FiniteAutomaton
{
    private HashSet<char> Q;
    private HashSet<char> Sigma;
    private Dictionary<(char, char), char> Delta;
    private char q0;
    private HashSet<char> F;

    public FiniteAutomaton(HashSet<char> vn, HashSet<char> vt, Dictionary<char, List<string>> p, char start)
    {
        Q = new HashSet<char>(vn);
        Sigma = new HashSet<char>(vt);
        q0 = start;
        F = new HashSet<char> { 'B', 'C', 'A' };
        Delta = new Dictionary<(char, char), char>();

        foreach (var rule in p)
        {
            foreach (var production in rule.Value)
            {
                if (production.Length == 2)
                {
                    Delta[(rule.Key, production[0])] = production[1];
                }
                else
                {
                    Delta[(rule.Key, production[0])] = production[0];
                }
            }
        }
    }

    public bool StringBelongToLanguage(string input)
    {
        char currentState = q0;
        foreach (char symbol in input)
        {
            if (!Sigma.Contains(symbol) || !Delta.ContainsKey((currentState, symbol)))
            {
                return false;
            }
            currentState = Delta[(currentState, symbol)];
        }
        return F.Contains(currentState);
    }
}

