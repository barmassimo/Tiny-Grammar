TinyGrammar
===========

A minimalistic implementation of Chomsky's [generative grammar](https://en.wikipedia.org/wiki/Generative_grammar)

The generator can be used as a library (no external dependencies) or via command line.

Grammar rules can be added programmaticaly or stored on a text file in a very intuitive format, for example:

```
# This is a simple grammar definition example.
# 
# Each row describes a grammar rule, in the form 
# <SYMBOL>:<EXPRESSION>
#
# On this file:
# 1."{SUBJECT}", "{ANIMAL}" and "{PERSON}" are grammar symbols (surrounded by curly brackets).
# 2. "a dog", "a cat", "John" and "{PERSON}'s dog" are expressions: an expression can be:
# - a symbol e.g. {ANIMAL}"
# - a constant e.g. "a cat"
# - a constant with symbols e.g. "{PERSON}'s dog"
#
# You can use some special characters in expressions:
# - "{{" and "}}" represents the characters "{" and "}"
# - "\n" represents a newline character
#
# Lines starting with "#" are comments and are ignored.
#
# The symbol at the left of the first rule is considered the starting symbol ({SUBJECT} in the example below).
#
# See the test project for library usage.
#
{SUBJECT}:{ANIMAL}
{SUBJECT}:{PERSON}
{ANIMAL}:a dog
{ANIMAL}:{PERSON}'s dog
{ANIMAL}:a cat
{PERSON}:John
#
```

## Live example

See [my site](http://massimobarbieri.it/it/Tecnichese) for a funny nonsense-tecnical sentence generator (Italian, I'm working on an English example too)

Copyright (C) [Massimo Barbieri](http://www.massimobarbieri.it) 

## Environment

* .NET Framework / C#
* XUnit for testing

## License

GNU GENERAL PUBLIC LICENSE V 3

