# Topic: Lexer & Scanner
**Course:** Formal Languages & Finite Automata  
**Author:** Romașenco Elena

---

## 📚 Theory

Lexical analysis is the first phase of a compiler or interpreter, where the source code is converted from a sequence of characters into a sequence of tokens that can be more easily processed by later phases. The component that performs this task is known as a **lexer**, **scanner**, or **tokenizer**.

---

## 🔑 Core Concepts

- **Lexemes:**  
  The actual character sequences in the source code that match a pattern for a token type.  
  _Example:_ In `count = 7`, the lexemes are `count`, `=`, and `7`.

- **Tokens:**  
  The categorized and classified lexemes. A token typically contains:
  - A token type/category (e.g., `<IDENTIFIER>`, `<OPERATOR>`, `<NUMBER>`)
  - The actual lexeme value (e.g., `"count"`, `"="`, `"7"`)
  - Optional metadata (e.g., line number, position)

- **Regular Expressions:**  
  Most lexers use regular expressions or finite automata to define and recognize token patterns.

---

## ⚙️ The Scanning Process

1. **Input Buffering:**  
   The lexer reads the input source code, often using buffer techniques for efficiency.

2. **Pattern Matching:**  
   Rules are applied to identify the next token, typically using the **longest match** (maximal munch) strategy.

3. **Token Generation:**  
   Once matched, the lexer creates a token with the appropriate type and value.

4. **Error Handling:**  
   Unexpected characters trigger error messages or recovery attempts.

5. **Whitespace and Comments:**  
   Usually discarded unless the language requires them to be meaningful.

---

## 🧱 Lexer Types

- **Hand-written Lexers:**  
  Written manually for fine control and performance.

- **Generated Lexers:**  
  Built with tools like Lex, Flex, or ANTLR from token specs.

- **DFA-Based Lexers:**  
  Use Deterministic Finite Automata for fast and predictable behavior.

---

## ⚠️ Challenges in Lexical Analysis

- **Ambiguity:**  
  A character sequence may match multiple patterns — solved by priority or longest match.

- **Context Sensitivity:**  
  Sometimes meaning depends on context (e.g., keyword vs identifier).

- **Lookahead:**  
  Some tokens require looking ahead in the input stream.

- **Error Recovery:**  
  Skipping or correcting invalid tokens can be complex.

---

Lexical analysis, while conceptually simple, forms the critical foundation for all subsequent phases of compilation or interpretation. By breaking down source code into meaningful tokens, it significantly simplifies the parsing stage that follows.

---

## 🎯 Objectives

- Understand what lexical analysis is.
- Get familiar with the inner workings of a lexer/scanner/tokenizer.
- Implement a sample lexer and show how it works.




## 🛠️ Implementation Description

The implementation of the lexer was tailored specifically for a regular grammar with terminal symbols `{a, b, c, d}`. It consists of three core components:

---

### 1. **Tokenizer.cs** – Lexical Analyzer Logic

This class performs the actual tokenization by iterating over each character in the input string.

#### ✅ Key Elements:

* **State Tracking**
  The lexer keeps track of:

  * `_position` – current index in the input
  * `_line` and `_column` – used for tracking token positions for error messages

* **Whitespace Handling**

  ```csharp
  if (char.IsWhiteSpace(current))
  ```

  All whitespace characters are skipped. If a newline is encountered (`\n`), it increments the line counter.

* **Token Matching**

  ```csharp
  Token? token = MatchToken(current);
  ```

  The lexer tries to match each character against known terminal symbols. If none is matched, an exception is thrown to indicate an unexpected character.

* **End-of-File Token**

  ```csharp
  tokens.Add(new Token(TokenType.EOF, "", _line, _column));
  ```

  A special `EOF` token is added at the end to mark the end of input — this is a common practice in compiler design.

---

### 2. **MatchToken(char ch)** – Token Type Dispatcher

This function maps a character to a specific `TokenType` using a C# `switch` expression:

```csharp
return ch switch
{
    'a' => new Token(TokenType.A, "a", _line, _column),
    'b' => new Token(TokenType.B, "b", _line, _column),
    'c' => new Token(TokenType.C, "c", _line, _column),
    'd' => new Token(TokenType.D, "d", _line, _column),
    _ => null
};
```

#### 🧠 Explanation:

Only characters `'a'`, `'b'`, `'c'`, and `'d'` are valid tokens. Any other character causes the lexer to throw an error. This simple structure is appropriate given the regular grammar's limited vocabulary.

---

### 3. **Token.cs** – Token Data Structure

The `Token` class holds the following metadata for each recognized token: Type - TokenType enum (e.g., `A`, `B`, `C`, `D`, `EOF`); Value - the actual character matched and the line and column which is the position in the input (used for debugging and error reporting)

#### 🔍 Sample Token:

```csharp
new Token(TokenType.A, "a", 1, 2)
```

This means that the character `'a'` was recognized as a token of type `A` at line 1, column 2.

---

### 4. **TokenType.cs** – Enum for Classifying Tokens

Defines a finite set of possible token types:

```csharp
public enum TokenType
{
    A,
    B,
    C,
    D,
    EOF
}
```
In such way it is possible to keep the lexer organized and allows other components to easily recognize what kind of token they are dealing with. The entire lexer is structured to be Deterministic, because each character has a clearly defined outcome; minimal as it only supports terminals required by the grammar and error-awareness - unexpected characters are immediately flagged with informative messages. This clean and purpose-built approach makes the implementation both efficient and easy to extend in the future.

### 5. **Conclusion**


In conclusion, this laboratory work successfully demonstrates the foundational step of developing a domain-specific language (DSL) by implementing a lexical analyzer. Although the example here is based on a simple regular grammar, the same principles are essential in more complex language processing scenarios.

Targeted Grammar Support - the lexer correctly identifies and categorizes all terminal characters defined in the grammar

Structured Error Handling - it detects and reports unexpected input, providing line and column for debugging

Accurate Position Tracking - maintains line and column tracking across various input formats, essential for diagnostics

Clear Token Differentiation - cleanly distinguishes between supported token types using a well-defined enum

Simple Pattern Recognition - efficiently recognizes single-character tokens with a minimalistic approach

Extendable Design - the modular structure allows easy expansion to support more token types or grammar rules

The implementation paves the way for future development, such as building a parser for syntax verification, implementing semantic checks, and potentially developing an interpreter or compiler targeting real-world applications. This lexer sets the stage for a complete and functional language toolchain.

