# Topic: Lexer & Scanner
**Course:** Formal Languages & Finite Automata  
**Author:** Romașenco Elena

---

## 📚 Theory

Lexical analysis is the first phase of a compiler or interpreter, where the source code is converted from a sequence of characters into a sequence of tokens that can be more easily processed by later phases. The component that performs this task is known as a **lexer**, **scanner**, or **tokenizer**.


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


   The lexer reads the input source code, often using buffer techniques for efficiency. Rules are applied to identify the next token, typically using the **longest match** (maximal munch) strategy. Once matched, the lexer creates a token with the appropriate type and value. Unexpected characters trigger error messages or recovery attempts.Usually discarded unless the language requires them to be meaningful.

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

 Lexical analysis presents several non-trivial challenges that must be addressed with care to ensure robust language processing. One of the most fundamental issues is **ambiguity**, where a given character sequence may match multiple token definitions. To resolve such situations, lexers commonly employ a strategy based on **priority rules** or follow the **longest match principle**, also known as **maximal munch**, to select the most appropriate token type.

Another complex aspect arises from **context sensitivity**. In some languages, a word might be treated as a keyword in one context and as an identifier in another. This requires the lexer or parser to be aware of the surrounding context, which can significantly complicate the design.

In addition, certain token types may necessitate **lookahead**, where the lexer needs to inspect additional characters beyond the current one to determine the correct classification. For instance, distinguishing between `=` and `==` requires peeking at the next character without prematurely finalizing the token.

**Error recovery** is also a critical consideration. When the lexer encounters unexpected or invalid characters, it must provide meaningful feedback — such as line and column position — and, ideally, attempt to recover gracefully without halting the entire compilation process. Designing such mechanisms can be quite intricate, especially in large or complex languages.

Despite these challenges, lexical analysis remains a cornerstone of language processing. Though it might seem conceptually simple, its correct implementation is essential. By decomposing raw source code into a structured stream of tokens, the lexer lays down a clear and reliable foundation for the next phase: parsing. Without a precise and well-designed scanner, no parser or compiler can function effectively.


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
 The Tokenize() method’s core loop processes input text character by character, emphasizing whitespace handling to ensure accurate position tracking. It distinguishes newline characters (\n), which increment the line counter and reset the column counter, from other whitespace like spaces and tabs, which only increment the column counter, while also managing Windows-style line endings (\r\n) by skipping redundant characters for cross-platform support.

Whitespace is isolated from token matching with a continue statement, preventing it from being treated as a token. This separation enhances code clarity and maintainability by distinctly handling position updates and token identification. All whitespace characters are skipped. If a newline is encountered (`\n`), it increments the line counter.

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
The Tokenize() method contains a critical section where token matching occurs. Once the lexer has advanced past any whitespace, it attempts to identify a token at the current position by calling the MatchToken() method. This method performs all the pattern matching logic and returns either a valid token or null if no match is found.

    When a token is successfully identified (not null), it's immediately added to the running list of tokens that will eventually represent the complete tokenized source code. This approach follows a sequential tokenization pattern where the input is processed from left to right, with each token being recognized and captured in order. The continuous accumulation of tokens builds up the lexical structure of the program, preserving both the semantic meaning and syntactic organization of the original source code.

    The simplicity of this design belies its power - by delegating the complex pattern matching to a separate method, the main tokenization loop remains clean and focused solely on the process of token collection and whitespace handling. This separation of concerns ensures that the tokenizer is both maintainable and extensible, allowing for new token patterns to be added without disrupting the core tokenization process.

Only characters `'a'`, `'b'`, `'c'`, and `'d'` are valid tokens. Any other character causes the lexer to throw an error. This simple structure is appropriate given the regular grammar's limited vocabulary.

---

### 3. **Token.cs** – Token Data Structure

The `Token` class holds the following metadata for each recognized token: Type - TokenType enum (e.g., `A`, `B`, `C`, `D`, `EOF`); Value - the actual character matched and the line and column which is the position in the input (used for debugging and error reporting)

#### 🔍 Sample Token:

```csharp
new Token(TokenType.A, "a", 1, 2)
```

 The MatchToken() method serves as the heart of the lexer, implementing a comprehensive pattern matching system for identifying tokens in the source code. It employs a hierarchical approach to token recognition, working from simple to complex patterns.

    Initially, the method checks for single-character symbols like parentheses, commas, and basic operators, which can be identified with a simple character comparison. For multi-character operators such as ==, >=, and <=, the method uses the Peek() helper function to examine the next character without advancing the position. This lookahead capability is essential for correctly distinguishing between operators like = (assignment) and == (equality comparison).

    After handling symbols and operators, the method proceeds to more complex token types. For alphabetic characters or specific identifier symbols ($ or #), it reads the entire word and passes it to CreateWordToken() for classification as either a reserved keyword or an identifier. For numeric characters, it invokes MatchNumber() to parse various numeric formats. String literals are handled by ReadString(), which extracts the content between quotation marks.

    This cascading approach to token matching creates a priority system where more specific patterns are checked before more general ones. If no pattern matches the current character, the method returns null, signaling to the caller that an unrecognized character has been encountered. This design provides excellent flexibility while maintaining clean, readable code structure, making it easy to understand how each token type is identified and processed during the lexical analysis phase.
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

