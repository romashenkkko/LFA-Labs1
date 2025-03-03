# Formal Languages and Automata - Laboratory Report

## 1. Theoretical Background

### 1.1 Alphabet
An **alphabet** is a finite set of symbols used to construct strings in a formal language. It is typically denoted by \( \Sigma \). In our case, the alphabet consists of:

\[ \Sigma = \{ a, b, c, d \} \]

These symbols are the building blocks of the language derived from our given grammar.

### 1.2 Vocabulary
A **vocabulary** consists of two main components:
- **Non-terminal symbols (VN)**: These are placeholders that can be replaced by other symbols. 
- **Terminal symbols (VT)**: These are symbols that appear in the final generated strings.

For our grammar, we define:
- Non-terminal symbols: \( VN = \{ S, A, B, C \} \)
- Terminal symbols: \( VT = \{ a, b, c, d \} \)

### 1.3 Grammar
A **formal grammar** consists of:
- **A set of non-terminals** \( VN \)
- **A set of terminals** \( VT \)
- **A set of production rules** \( P \)
- **A start symbol** \( S \)

For our specific grammar:

\[
P = \{
S \to dA,\
A \to aB,\
B \to bC,\
C \to cB,\
B \to d,\
C \to aA,\
A \to b
\}
\]

The goal of a grammar is to generate valid strings that belong to the language defined by its production rules.

## 2. Task

The task requires us to:
1. **Implement a Grammar class** that represents the given grammar structure.
2. **Generate 5 valid strings** based on the grammar.
3. **Convert the grammar into a Finite Automaton**.
4. **Implement a function to check if a string belongs to the language** defined by the automaton.
5. **Write a report to document our solution**.

## 3. Solution

### 3.1 Grammar Implementation
We implemented a **Grammar** class with:
- A **set of non-terminals** and **terminals**.
- A **dictionary for production rules**.
- A **method to generate valid strings**.

#### Example Function: Generating Strings
```
               ....

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
          ....
 
```
This function expands a string step by step until it consists only of terminal symbols.

### 3.2 Converting Grammar to Finite Automaton
We created a **FiniteAutomaton** class, which:
- Defines **states** corresponding to non-terminals.
- Uses a **transition function** to move between states.
- Checks whether an input string belongs to the language.

#### Example Function: Checking Language Membership
```csharp
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
```
This function verifies if a given string follows valid transitions in the automaton.

## 4. Conclusion
This laboratory work helped us:
- Understand the structure of **formal grammars**.
- Implement **string generation** using production rules.
- Convert a **grammar into a finite automaton**.
- Validate whether a string **belongs to the language**.

The implementation correctly generates different strings and verifies input strings against the finite automaton, successfully completing all requirements of the task.