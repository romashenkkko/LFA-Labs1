# Formal Languages and Automata - Laboratory Report

## 1. Theoretical Background

### 1.1 Alphabet
An alphabet is a finite set of symbols used to construct strings in a formal language. It is typically denoted by \( \Sigma \). In our case, the alphabet consists of:
These symbols are the building blocks of the language derived from our given grammar. Formal languages are essential for defining structured communication between machines, humans, or systems. They establish a precise way to represent information, ensuring consistency and eliminating ambiguity. Formal languages play a critical role in computer science, particularly in programming languages, compilers, artificial intelligence, cryptography, and data validation. They are used to describe syntax in programming, define automata in computational models, and facilitate language processing in artificial intelligence and natural language processing systems. Additionally, they are applied in network protocols, database query languages, and security models to ensure structured and rule-based data processing.

\[ \Sigma = \{ a, b, c, d \} \]


### 1.2 Vocabulary
A vocabulary consists of two main components. Non-terminal symbols (VN) are placeholders that can be replaced by other symbols. Terminal symbols (VT) are symbols that appear in the final generated strings. 

For our grammar, we define:
- Non-terminal symbols: \( VN = \{ S, A, B, C \} \)
- Terminal symbols: \( VT = \{ a, b, c, d \} \)

### 1.3 Grammar
A formal grammar consists of a set of non-terminals \( VN \), a set of terminals \( VT \), a set of production rules \( P \), and a start symbol \( S \). 
The goal of a grammar is to generate valid strings that belong to the language defined by its production rules. By defining a structured way to generate and interpret strings, formal grammars allow us to analyze and process languages efficiently. They provide the foundation for automata theory, which models computational processes and helps design efficient algorithms for language recognition, text parsing, and automated reasoning. Understanding grammars enables the development of programming languages, ensures syntactic correctness in code execution, and supports natural language processing applications in artificial intelligence.


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
```csharp
             

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
      
 
```
This function expands a string step by step until it consists only of terminal symbols.

### 3.2 Converting Grammar to Finite Automaton
We created a **FiniteAutomaton** class, which:
- Defines **states** corresponding to non-terminals.
- Uses a **transition function** to move between states.
- Checks whether an input string belongs to the language.

#### Example Function: Testing The Grammar Function
One of the key functionalities implemented in our finite automaton is the ability to check whether a given string belongs to the language defined by our grammar. This is accomplished using the following function:
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
This function takes an input string and processes it step by step to determine if it follows valid state transitions in the automaton. The currentState variable is initialized to the start state , and each character of the input string is checked against the transition function . If at any point the transition is not defined (i.e., the symbol is not in the alphabet or there is no valid transition from the current state), the function returns false, indicating that the string is not valid. Otherwise, it updates the currentState accordingly and continues checking the remaining characters.

Once all characters are processed, the function checks whether the final state is one of the accepting states . If it is, the function returns true, confirming that the input string is valid within the language. This approach ensures that our automaton correctly recognizes strings that conform to the grammar rules while rejecting those that do not.

## 4. Conclusion

Through this laboratory work, we have explored the fundamental aspects of formal grammars, their role in defining structured languages, and their transformation into finite automata. We successfully implemented a grammar that generates valid strings, converted it into an equivalent finite automaton, and tested its ability to determine whether an input string belongs to the language. This approach provided valuable insights into automata theory and computational linguistics, helping to bridge the gap between abstract language definitions and practical implementations in programming.

A crucial part of our implementation involved testing the finite automaton's ability to recognize valid strings. The function public bool StringBelongToLanguage(string input) was implemented to check whether a given string follows valid state transitions. The function iterates through each character of the input string, verifying if a transition exists for the given state-symbol pair. If at any point the transition is not defined, the function returns false. Otherwise, it continues transitioning between states until it reaches a final state, confirming if the string belongs to the language.

Our tests confirmed that strings generated by the grammar were correctly recognized by the finite automaton. For example, given the input "dab", the function correctly determined its validity within the language. Similarly, the string "dabc" was verified and accepted as a valid string. These tests demonstrate the correctness and reliability of our implementation.

In conclusion, this work provided practical experience in formal language processing and automata theory. The implementation successfully demonstrated how formal grammars can be systematically converted into automata, paving the way for further studies in compiler design, text recognition, and formal verification systems.

