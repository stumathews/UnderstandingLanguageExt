# Language.Ext for new developers

An initial tutorial showing how to use ideas such as pipelineing, delcarative style coding and walks through the fundamentals behind Select() SelectMany() and Bind() and Map() while covering implementation via Linq's Fluent and Linq Expression syntax.
The tutorials go through the followng concepts

# The Basics
* Tutorial01 - Construction of a Monad
* Tutorial02 - Shows you how to use Map and Bind (also construction of a Mondad Type)
* Tutorial03 - Shows how Bind is used to create a pipeline of function calls
* Tutorial04 - Performing operations on a Box using Map() and Bind(), Select and SelectMany()
* Tutorial05 - More examples of performing operations on a Box
* Tutorial06 - This tutorial shows you how pipelining is used to call funtions using Linq Fluent and Expression syntax
* Tutorial07 - Shows you when to use Map() and Bind()
* Tutorial08 - Shows you how to transition from Imperative style coding to Declarative style coding with an example
* Tutorial09 - Shows you that pipelines include automatic validation
* Tutorial10 - Expands on Tutorial09 to show that transofmration function always return a Monad
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
## Option<T>
* Tutorial28 - Introduction to Option<T>
* Tutorial29 - Basic use-case of Option<T>
* Tutorial30 - Using Option<T> in functions (passing in and returning)
* Tutorial31 - using IfSome() and IfNone()
* Tutorial32 - Creates an entire application of just functions returning and receiving Option<T>
# Work in progress
* TutorialA - Partial Functions - Allowing multiple arguments to be 'baked' in and still appear as Math like functions (pure functions)
* TutorialB - Immutability - Smart constructors, Immutable data-types
* TutorialC - Caching
* TutorialD - Threading and parallelism benfits
* TutorialE - Guidelines for writing immutable code, starting with IO on the fringes (bycicle spoke design)
* TutorialF - Immutable Collection types in Language Ext
* TutorialG - Spoke and wheel model
* TutorialH - Map, HashSt, Set LanguageExt collections
* TutorialI - Changing state over time (Fold)
* TutorialJ - Custom useful Monad Extensions

References:

https://stackoverflow.com/questions/28139259/why-do-we-need-monads Why do we need monads? 
