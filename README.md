# Language.Ext for new developers

An initial tutorial showing how to use ideas such as pipelineing, delcarative style coding and walks through the fundamentals behind Select() SelectMany() and Bind() and Map() while covering implementation via Linq's Fluent and Linq Expression syntax.
The tutorials go through the followng concepts

* Tutorial01 - Construction of a Type with its own Select() function
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
* Tutorial14 - Shows the basics of Either<L,R> using Bind()
* Tutorial15 - Shows the basics of Either<L,R>, using BiBind()
* Tutorial16 - Shows the basics of Either<L,R>, using BiExists()
* Tutorial17 - Shows the basics of Either<L,R>, using Fold() to change an initial state over time based on the contents of the Either
* Tutorial18 - Shows the basics of Either<L,R>, using iter() to run an arbitary function on the either if its value is right type or BiIter() to specify a function to run on both types 
* Tutorial19 - Shows the basics of Either<L,R>, using BiMap() to make provision for a transform function for both the left and right types of the either. The transform is automatically lifted.
* Tutorial20 - Shows the basics of Either<L,R>, using BindLeft() to make provision for a transform function forthe left types of the either. The transform is NOT automatically lifted(this is a bind() after all).
* Tutorial21 - Shows the basics of Either<L,R>, using BindLeft() using Match to extract the contents of an Either<> but and not put it back into and either types (as map() and Bind() would do)
* Tutorial22 - Shows the basics of transforming a list of Eithers using BiMapT and MapT
* Tutorial23 - Shows the basics of transforming a list of Eithers using BindT
* Tutorial24 - Shows the basics of transforming a list of Eithers using IterT
* Tutorial25 - Shows the basics of transforming a list of Eithers using Apply both on a simple Either<> and a List of Eithers to demonstate its simplicity
* Tutorial26 - Shows the basics of transforming a list of Eithers using Partition to easily get both the lefts() and the Rights() in one call - as a tuple of (lefts,rights)
* Tutorial27 - Shows the basics of transforming a list of Eithers using match to understand whats in them (both left and right values) and then transform them based on their values into a single type that represents either in one way (a string)
* Tutorialv - Partial Functions - Allowing multiple arguments to be 'baked' in and still appear as Math like functions
* Tutorialw - Immutability - Smart constructors, Immutable data-types
* Tutorialx - Caching
* Tutorialy - Threading and parallelism benfits
* Tutorialz - Guidelines for writing immutable code, starting with IO on the fringes (bycicle spoke design)

