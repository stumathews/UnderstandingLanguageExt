# Understanding Language.Ext

This is a tutorial that aims to demonstrate the fundamentals behind using LanguageExt in a practical fashion though step-by-step tutorials which introduce and then build up on concepts.

Furthermore, the tutorial shows how to use ideas such as pipelineing, delcarative style coding and walks through the fundamentals behind Select() SelectMany() and Bind() and Map() while covering implementation via Linq's Fluent and Linq Expression syntax.

The general tutorial is structured like this:

* Monads
* Map/Bind
* Fluent/Expression Linq
* Pipelining
* Declarative Style
* Monad Validation
* Function Composition
* Pure Functions
* Basics of Either<L,R>
* Operations with Lists Eithers<,>
* Basics of Option<T>
* ThrowIfFailed()
* FailureToNone()

Note: To run any specific tutorial, right-click on the project in the solution explorer and 'Set as start-up project'

# The Basics
* Tutorial01 - Introduction to the Box type (a Monad)
* Tutorial02 - Shows you how to use Map and Bind (also construction of a Mondad Type)
* Tutorial03 - Shows how Bind is used to create a pipeline of function calls
* Tutorial04 - Performing operations on a Box using Map() and Bind(), Select() and introducing SelectMany()
* Tutorial05 - More examples of performing operations on a Box
* Tutorial06 - This tutorial shows you how pipelining is used to call funtions using Linq Fluent and Expression syntax
* Tutorial07 - Shows you when to use Map() and Bind()
* Tutorial08 - Shows you how to transition from Imperative style coding to Declarative style coding with an example
* Tutorial09 - Shows you that pipelines include automatic validation
* Tutorial10 - Expands on Tutorial09 to show that transformation function always return a Monad
* Tutorial11 - Shows how monad built in validation, affords short-cuircuiting functionality.
* Tutorial12 - Composition of functions
* Tutorial13 - Pure Functions - immutable functions with now side effects ie mathematically correct
# Language.Ext
## Either<Left,Right> Basics
* Tutorial14 - Shows the basics of Either<L,R> using Bind()
* Tutorial15 - Using BiBind()
* Tutorial16 - Using BiExists()
* Tutorial17 - Using Fold() to change an initial state over time based on the contents of the Either
* Tutorial18 - Using Iter()
* Tutorial19 - Using BiMap()
* Tutorial20 - Using BindLeft()
* Tutorial21 - Using Match()
## Operations on Lists of Either<left, Right>
* Tutorial22 - Using BiMapT and MapT
* Tutorial23 - Using BindT
* Tutorial24 - Using IterT
* Tutorial25 - Using Apply
* Tutorial26 - Using Partition
* Tutorial27 - Using Match
## Option <T> Basics
* Tutorial28 - Introduction to Option<T>
* Tutorial29 - Basic use-case of Option<T>
* Tutorial30 - Using Option<T> in functions (passing in and returning)
* Tutorial31 - using IfSome() and IfNone()
* Tutorial32 - Creates an entire application of just functions via pipelineing which returning and receive Option<T>
* Tutorial33 - Using ToEither<>
* Tutorial34 - Using BiMap() - see tutorial 19
# Custom Specific 
* Tutorial35 - Using custom extension method ThrowIfFailed() and introducing Either<IAmFailure, Option<T>> as a standard return type for all functions
* Tutorial36 - Using custom extension method FailureToNone()
# Misc
* Tutorial37 - Using pure functions to cache things, ensuring that you need not call expensive calls if they've been done once already.
# Todo
* TutorialA - Partial Functions - Allowing multiple arguments to be 'baked' in and still appear as Math like functions (pure functions)
* TutorialB - Immutability - Smart constructors, Immutable data-types
* TutorialD - Threading and parallelism benfits
* TutorialE - Guidelines for writing immutable code, starting with IO on the fringes (bycicle spoke design)
* TutorialF - Immutable Collection types in Language Ext
* TutorialG - Spoke and wheel model
* TutorialH - Map, HashSt, Set LanguageExt collections
* TutorialI - Changing state over time (Fold)
* TutorialJ - Custom useful Monad Extensions



References:

* https://stackoverflow.com/questions/28139259/why-do-we-need-monads Why do we need monads? 
* https://github.com/louthy/language-ext/wiki LanguageExt Wiki
* http://www.stuartmathews.com/index.php/component/tags/tag/functional-prograqmming My articles on Functional programming(old)
