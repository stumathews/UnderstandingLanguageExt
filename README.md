---
generator: pandoc
title: The Language Ext Tutorial
viewport: 'width=device-width, initial-scale=1.0, user-scalable=yes'
---

Understanding Language Ext

by Stuart Mathews

-   [Part I: Monads](#part-i-monads)
    -   [Introduction](#introduction)
    -   [Acknowledgements](#acknowledgements)
-   [Scope](#scope)
    -   [Monad Basics](#monad-basics)
    -   [Language.Ext](#language.ext)
        -   [Either\<Left,Right\> Basics](#eitherleftright-basics)
        -   [Operations on Lists of Either\<left,
            Right\>](#operations-on-lists-of-eitherleft-right)
        -   [Option Basics](#option-basics)
    -   [Custom Specific](#custom-specific)
        -   [Todo](#todo)
-   [Basics](#basics)
    -   [Introduction to the Box type (a
        Monad)](#introduction-to-the-box-type-a-monad)
    -   [Transforming the contents of a
        Monad](#transforming-the-contents-of-a-monad)
    -   [Using the Select function on a
        Monad](#using-the-select-function-on-a-monad)
    -   [Understanding Map and Bind](#understanding-map-and-bind)
    -   [Pipelining transformation
        workloads (3)](#pipelining-transformation-workloads-3)
    -   [Monadic functions: Complete
        example (4)](#monadic-functions-complete-example-4)
    -   [Using LINQ Fluent and Expression
        syntax (6)](#using-linq-fluent-and-expression-syntax-6)
    -   [When to use *Map()* or
        *Bind()* (7)](#when-to-use-map-or-bind-7)
    -   [Transitioning from Imperative to Declarative
        style (8)](#transitioning-from-imperative-to-declarative-style-8)
    -   [Monad validation (9)](#monad-validation-9)
    -   [Returning Monads: transformations always return a
        Monad (10)](#returning-monads-transformations-always-return-a-monad-10)
    -   [Built-in validation and short-circuiting in
        Monads (11)](#built-in-validation-and-short-circuiting-in-monads-11)
    -   [A diversion: composition of
        functions (12)](#a-diversion-composition-of-functions-12)
    -   [A diversion: Pure
        Functions (13)](#a-diversion-pure-functions-13)
-   [Part II: Language-Ext](#part-ii-language-ext)
    -   [The Either\<L,R\> Monad](#the-eitherlr-monad)
        -   [Introducing the Either
            monad (14)](#introducing-the-either-monad-14)
        -   [Operations on Either\<L,R\>](#operations-on-eitherlr)
        -   [Operating on Lists of Either\<left,
            Right\>](#operating-on-lists-of-eitherleft-right)
    -   [The Option\<T\> Monad](#the-optiont-monad)
        -   [Introduction to
            Option\<T\> (28)](#introduction-to-optiont-28)
        -   [Basic use-case of
            Option\<T\> (29)](#basic-use-case-of-optiont-29)
        -   [Using Option\<T\> in functions (passing in and
            returning)](#using-optiont-in-functions-passing-in-and-returning)
        -   [using IfSome() and IfNone()](#using-ifsome-and-ifnone)
        -   [Pipelining with
            Options\<T\> (32)](#pipelining-with-optionst-32)
        -   [Using ToEither\<\>](#using-toeither)
        -   [Using BiMap() (34)](#using-bimap-34)
    -   [The Try\<T\> Monad](#the-tryt-monad)
        -   [Supressing Exceptions](#supressing-exceptions)
-   [Part III: Everything else](#part-iii-everything-else)
    -   [Bonus](#bonus)
        -   [ThrowIfFailed() and introducing Either\<IAmFailure,
            Option\<T\>\>](#throwiffailed-and-introducing-eitheriamfailure-optiont)
        -   [Using custom extension method
            FailureToNone()](#using-custom-extension-method-failuretonone)
        -   [Memoization](#memoization)
        -   [Apply events over time to change an
            object](#apply-events-over-time-to-change-an-object)
        -   [Smart constructors and Immutable
            data-types](#smart-constructors-and-immutable-data-types)

Part I: Monads
==============

Introduction
------------

This is a tutorial that aims to demonstrate how to use LanguageExt in
C\# programs with an emphasis on the practical side, demonstrating
through code examples and providing narrative throughout the process.

This is a tutorial that aims to demonstrate the fundamentals behind
using LanguageExt in a practical fashion though step-by-step tutorials
which introduce and then build up on concepts.

Furthermore, the tutorial shows how to use ideas such as pipelineing,
delcarative style coding and walks through the fundamentals behind
Select() SelectMany() and Bind() and Map() while covering implementation
via Linq\'s Fluent and Linq Expression syntax.

The general tutorial is structured like this:

-   Monads

-   Map/Bind

-   Fluent/Expression Linq

-   Pipelining

-   Declarative Style

-   Monad Validation

-   Function Composition

-   Pure Functions

-   Basics of Either\<L,R\>

-   Operations with Lists Eithers\<,\>

-   Basics of Option

-   ThrowIfFailed()

-   FailureToNone()

Bonus: Functional programming concepts

-   Caching - Using pure functions to cache things, ensuring that you
    need not call expensive calls if they\'ve been done once already.

-   Changing object state over time

-   Immutability

**Note: To run any specific tutorial, right-click on the project in the
solution explorer and \'Set as start-up project\'**

Acknowledgements
----------------

Language.Ext library was created by Paul Louthy. See
<https://github.com/louthy/language-ext>

References:

-   <https://stackoverflow.com/questions/28139259/why-do-we-need-monads> Why
    do we need monads?

-   <https://github.com/louthy/language-ext/wiki> LanguageExt Wiki

-   <http://www.stuartmathews.com/index.php/component/tags/tag/functional-prograqmming> My
    articles on Functional programming(old)

Scope
=====

Monad Basics
------------

-   Tutorial01 - Introduction to the Box type (a Monad)

-   Tutorial02 - Shows you how to use Map and Bind (also construction of
    a Mondad Type)

-   Tutorial03 - Shows how Bind is used to create a pipeline of function
    calls

-   Tutorial04 - Performing operations on a Box using Map() and Bind(),
    Select() and introducing SelectMany()

-   Tutorial05 - More examples of performing operations on a Box

-   Tutorial06 - This tutorial shows you how pipelining is used to call
    funtions using Linq Fluent and Expression syntax

-   Tutorial07 - Shows you when to use Map() and Bind()

-   Tutorial08 - Shows you how to transition from Imperative style
    coding to Declarative style coding with an example

-   Tutorial09 - Shows you that pipelines include automatic validation

-   Tutorial10 - Expands on Tutorial09 to show that transformation
    function always return a Monad

-   Tutorial11 - Shows how monad built in validation, affords
    short-cuircuiting functionality.

-   Tutorial12 - Composition of functions

-   Tutorial13 - Pure Functions - immutable functions with now side
    effects ie mathematically correct

Language.Ext
------------

### Either\<Left,Right\> Basics

-   Tutorial14 - Shows the basics of Either\<L,R\> using Bind()

-   Tutorial15 - Using BiBind()

-   Tutorial16 - Using BiExists()

-   Tutorial17 - Using Fold() to change an initial state over time based
    on the contents of the Either

-   Tutorial18 - Using Iter()

-   Tutorial19 - Using BiMap()

-   Tutorial20 - Using BindLeft()

-   Tutorial21 - Using Match()

### Operations on Lists of Either\<left, Right\>

-   Tutorial22 - Using BiMapT and MapT

-   Tutorial23 - Using BindT

-   Tutorial24 - Using IterT

-   Tutorial25 - Using Apply

-   Tutorial26 - Using Partition

-   Tutorial27 - Using Match

### Option Basics

-   Tutorial28 - Introduction to Option

-   Tutorial29 - Basic use-case of Option

-   Tutorial30 - Using Option in functions (passing in and returning)

-   Tutorial31 - using IfSome() and IfNone()

-   Tutorial32 - Creates an entire application of just functions via
    pipelineing which returning and receive Option

-   Tutorial33 - Using ToEither\<\>

-   Tutorial34 - Using BiMap() - see tutorial 19

Custom Specific
---------------

-   Tutorial35 - Using custom extension method ThrowIfFailed() and
    introducing Either\<IAmFailure, Option\> as a standard return type
    for all functions

-   Tutorial36 - Using custom extension method FailureToNone()

-   Tutorial37 - Using pure functions to cache things, ensuring that you
    need not call expensive calls if they\'ve been done once already.
    (Bonus: FP concepts)

-   Tutorial38 - Changing state of an object over time (Fold) including
    concept of apply events over time to change an object (Bonus: FP
    concepts)

-   Tutorial39 - Immutability - Designing your objects with immutability
    in mind: Smart constructors and Immutable data-types (Bonus: FP
    concepts)

-   Tutorial40 - Try - Supressing Exceptions

### Todo

-   TutorialA - Partial Functions - Allowing multiple arguments to be
    \'baked\' in and still appear as Math like functions (pure
    functions)

-   TutorialB - Threading and parallelism benfits

-   TutorialC - Guidelines for writing immutable code, starting with IO
    on the fringes (bycicle spoke design)

-   TutorialD - Custom useful Monad Extensions

Basics
======

Introduction to the Box type (a Monad)
--------------------------------------

A Monad is a just type, much like any user-defined type that is created
when designing a class. It has its own properties that represent its
state, and methods that give the Monad behaviour and, in many cases,
manipulates its state through a public interface it provides to the
programmers to use.

A simple example of a Monad might be a Box type:

![](./myMediaFolder/media/image2.png)

A *Box\<T\>,* can hold any type but in this case, it holds and makes
provision for an integer type, i.e. *Box\<int\>*.

Look at the implementation of the *Box\<T\>*, notice it's just a C\#
Class that's been created:

![](./myMediaFolder/media/image4.png)

So far this looks like a normal C\# class for the Box type. You can
create a *Box\<T\>* by specifying the type of contents it can hold, and
internally to the class, it is stored as a member called *\_item*.
Additionally, you can set the contents of the *Box\<T\>* through its
*Item* property. Thus, is it possible to extract and set the contents of
the *Box\<T\>*:

![](./myMediaFolder/media/image6.png)

Monads are types that allow you to transform the contents of the Monad,
in this case the contents of the *Box\<T\>* which is currently set to
99. A series of defined functions need to exist on a Monad which will
define the kinds of transformations that can occur -- they are expected
to exist. If a type has a *Map, Select* and a *Bind* function, it can
*generally* be considered a Monad.

Transforming the contents of a Monad
------------------------------------

The important thing to understand is that whatever the transformation
will be, it needs to be provided by the programmer. Calling one of these
functions, will then read the contents of the Monad and change the
contents somehow -- the programmer will provide a *Higher-Ordered
Function* (HOF), which will define the transformation. The monadic
functions, such as *Select/Map* or *Bind* acts an execution environment,
passing its internal contents into the HOF, where additional checks can
be done before or after the transformation is started. The HOF is known
as the transformation function and is provided by the programmer using
the Monad.

The *Select* function does exactly this, when called on the *Box\<T\>,*
it passes the contents of the *Box\<T\>* monad into the user-defined
transformation function and executes that function, and the results
thereof i.e. the *transformation* become part of the result of the
*Select* function call. Lets see how it does this:

![](./myMediaFolder/media/image8.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial01\_monad/Box.cs

*TA* is the type of the content of the Box i.e. *Box\<TA\>,* *TB* is the
type that *TA* will be transformed into, *box* is the *Box\<T\>* type
where conceptually *T* is replaced with *TA*. The last parameter *map*
is the user-provided function aka *the transformation function*.

In this example, the transformation function is called *map*, which will
be provided by the programmer defined as *Func\<TA, TB\>* which is any
function accepting a type of *TA* (which also happens to be the type
which Box holds), and which returns a *TB* type as its transformation
result.

You'll notice how the *Box\<T\>*'s *Select* function wraps the supplied
transformation function, *map()* by first checking if the *Box\<T\>* is
empty, and if so returns an empty *Box\<TB\>,* otherwise executes the
map function, producing a transformation of the content. The
transformation is represented by the return type of *TB*, as the
original type of the content of the box is *TA*.

An important distinction is that the transformation result is placed
into a *new* *Box\<T\>* but specifically as a transformed
representation, *Box\<TB\>* and the original *Box\<T\>*'s content is not
modified during the *Select* function's operation. This is a preview the
concept of immutability, and of how other Monad functions that accept a
user-defined transformation function - like *Bind,* will work.

Using the Select function on a Monad
------------------------------------

*Box\<T\>*'s *Select* function is called by the programmer like this:

![](./myMediaFolder/media/image10.png)

The *x =\> + 1* syntax is a definition of a lambda function that takes
in one argument and the result of the expression after the *=\>* is the
result, and as such it is a suitable transformation function to pass
into the *Select* function, provided it matches the *map* parameter's
types as defined by *Func\<TA, TB\>.*

Incidentally, this notation of explicitly calling the *Select* function,
as defined on the *Box\<T\>* type is called the *Linq Fluent Syntax*. An
alternative is to use the *Linq Query Expression Syntax*:

![](./myMediaFolder/media/image12.png)

This form, will fetch or *select* a value from the box by using the
*Box\<T\>*\'s *Select* function (this is done implicitly) and passing
the expression immediately after the *select* statement i.e. *select
number1 +1* as the mapping function for the *Select* function on the
type, in this case a *Box\<T\>.*

As noted previously, the *\'Linq Expression Syntax*\' and requires
*Box\<T\>* to have a *Select* function for it to work in this way.

Any type you define that has a *Select* function defined on it as an
extension method can be used in both Linq Fluent and Expression syntax
forms.

The resulting program illustrates the concepts discussed thus far:

![](./myMediaFolder/media/image14.png)

<https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial01_monad/Program.cs>

Many Monads in Language-Ext are utilized by taking advantage of C\#'s
*Query Expression Syntax* notation for accessing the *Select* function
in this way. This is also largely due to being able to define an
extension method for any type this way.

When working with Language-Ext in general, the already defined monad
types such as *Option\<T\>*, *Either\<L,R\>* and *Try\<T\>* already have
suitable *Select*, *Map* and *Bind* functions defined and as such its
not needed to define them, and only need to pass in the transformation
function, that is unless you want to create our own Monad like our
*Box\<T\>* monad, and in which case, you now know how.

Why do we need to perform transformations? Traditional programming
revolves around having functions. Almost every programming language
allows you to define functions which take input and process it and then
produce and output. This is what transformation is. So instead of
defining explicit hard-coded functions in your source code, you can now
pass around functions -- transformation functions. Monads are datatypes
that allow you to work on their contents but providing the
transformation function, where the input of the transformation function
will be the contents of the Monad. Monads to a little bit more, they can
run checks to validate if that function should run or not, and how it
will behave. In this was Monads provide monadic functions like Select,
Map and Bind to house the incoming transformation function that the user
will provide, and acts as an execution vehicle for running that function
and passing to it, its own contents.

Next, we'll put the Select function aside, and discuss its relatives
*Map* and *Bind*.

Understanding Map and Bind
--------------------------

Now we're going to look into the other monadic functions, map and bind.
Like select, the both allow the programmer to provide a customer
transformation function. The difference between the two is how that
transformation is returned to the programmer.

As suggested previously, like Select, both Map and Bind serve as an
execution environment for the transformation function provided by the
user, and they follow a certain interesting pattern before performing
the transformation function. This can be described using the acronym
VETL or Validate, Extract, Transform and Lift/Leave. These phases all
apply to the how the *Select*, *map* and *bind* functions work and
dictate how they ultimately call the user-defined transformation
function.

Let's have a look at *Map* and *Bind* for the *Box\<T\>* Monad. As show
previously with *Select*, these are just two extension methods on the
Box\<T\> type. Also shown is the previously talked about, *Select*
method which shows how it too features the same VETL convention.

![](./myMediaFolder/media/image15.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial02\_transformations\_1/Box.cs

First, there is no difference between *Map* and *Select*. They are
identical functions, and function as previously described i.e. it
*validates* if the *Box\<T\>* is empty, it *Extracts* the content of the
Monad, our *Box\<T\>*'s *\_Item* member in this case, runs the
user-provided transformation function on that content and *Lifts* the
result of the transformation into a new Box object. In this way, it
follows the above pattern of *VETL*.

*Map* and *Bind* do much the same, however they differ only in how they
return the transformed result i.e. the Lift/leave phase. In all cases
the transformation function is run, however the requirements of the
transformation's functions arguments and return type have changed:

*Bind* requires the programmer to define and provide a transformation
function that will take as input the item provided to it by the *Bind*
function, but crucially it needs to transform it in such a way that the
result is a new *Box\<TB\>* while *Map*, only requires the
transformation function to return the transformation from TA to TB
without needing to place it into a new *Box\<TB\>*.

Fundamentally, what this means is that both Map and Bind do the same
transformation, but package up the result in different ways.

Let have a look at this in an example

![](./myMediaFolder/media/image17.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial02\_transformations\_1/Program.cs

Now the Boxes contains another kind of thing, a list of integers. So
effectively the Boxes are boxes containing numbers. Two *Box\<int\[\]\>*
variables are defined that contain a list of numbers. As the Box*\<T\>*
type has *Map* and *Bind* defined for it, we can call them by providing
a transformation functions i.e. *MyFunction()* and *MyFunction2()*.

Notice how *MyFunction* takes in a list of numbers, i.e *int\[\]* which
is the *T* in the *Box\<T\>* definition when *Box\<T\>* is defined as
*Box\<int\[\]\>.* This is the same for *MyFunction2*. The difference is
what they *return*, and as a consequence, limits each one to being used
in either *Map* i.e *MyFunction2* or *Bind* i.e *MyFunction*. They can't
switched around because *Map* and *Bind* have different type
requirements for the transformation functions.

Here the contents of the Boxes are fed into the transformation functions
as lambda expressions:

![](./myMediaFolder/media/image18.png)

Ultimately the return type of the enclosing Map and Bind functions, i.e.
the vehicles for the user-defined function need to return a Monad or a
*Box\<int\[\]\>* in this case. So, depending on if we're calling *Map*
or *Bind*, internally these two functions need to know if the
transformation function will lift the transformation result into the
Monad type or not, hence why *Map* and *Bind* function *lift* or *leave*
the result of transformed content respectively.

How *Map* or *Bind* does this is by expecting a certain return-type of
the user-provided transformation function and either automatically
*lifting* the result into a *Box\<T\>* where the transformation function
does not do that, as is the case with the *Map* function, or *leaving*
the user-provided transformation function to do so, as is the case with
the *Bind* function. This is why they require different forms of the
user-provided transformation function, but ultimately the transformation
is the same for both map and bind. In many cases you can use them
interchangeably with the exception that the supplied lambda function or
transformation function meets the requirements of either returning a
Monad or not.

The take away is that *Map*() and *Bind*() do the same thing but
*Bind*() requires you to put your transformation result back in the Box,
while *Map* doesn\'t.

Both methods \'manage\' your user defined transform function by running
it only if it deems it should (if validation passes) and then depending
on the specific function, it will either lift the result of the
transformation(*Map*) or require that your transformation function\'s
signature explicitly says it will do it itself (B*ind*)

This means, with a you don't have to return a Box (like you do when you
with *Bind*), when transforming with the *Map*() function\...it
automatically does this for you

But here is something sneaky but very useful to know: The user-provided
transformation function by definition can change the return type of the
content it is transforming:

![](./myMediaFolder/media/image20.png)

Look, we\'ve been able to change the type of the Box from a *int\[\]* to
a *string*!

This is by virtue of the fact that it's what your transformation
function returned, and it thus transformed the return type also, and put
it back into a box(because that's what map does). It's still in a
*Box\<T\>* but is a box of a *string* now instead of a box of
numbers\....

*Map*() and *Bind*() can do this, and this is what makes these function
really useful for chaining transformations together. His behaviour
allows use to instead of designing hard coded functions like other
programming languages like C or Pascal does, we can instead pass our
functions into our Monads when designing logic that works with them,
instead of passing out Monads into function, which is the old way of
thinking.

We'll explore this next but before working further on out Monad Box, why
do we need Monads? Why do we have to have Map() and Bind()..

Here is why:
<https://stackoverflow.com/questions/28139259/why-do-we-need-monads>

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial02\_transformations\_1/Box.cs

Pipelining transformation workloads (3)
---------------------------------------

We will continue to define and explain our Monad type, *Box\<T\>* and
showing how transformations are use in real time as well as previewing
an implicit behaviour of Monads: short-circuiting.

Short-circuiting works when chaining or cascading multiple *Bind*() or
*Map*() transformations together as part of a pipeline. Lets look at an
example, which we'll go through shortly:

![](./myMediaFolder/media/image23.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial03\_pipelines/Program.cs

As can be seen in the example, we're creating a pipeline of *Bind*
operations, each one doing its defined transformation as provided by the
user, and each time the *Bind* returns a Monad, which is a valid type
for the next *Bind* extension method to work on. These cascade into a
series of transformations, but this has hidden and useful feature:
short-circuiting.

Put differently, this code transform the contents of the box by passing
it down a series of *Bind*() operations,

so there are multiple associated VETL-\>VETL-\>VETL steps that
represents the *Binds*()

effectively representing a pipeline of data going in and out of
*Bind*(VETL) functions

Remember that the \'validate\' step is implicit and is actually
hard-coded directly into the *Bind*() or *Map*() functions that we
previously looked at.

The validity of a box in this case, is if it is empty or not (remember
that at the Bind functions\' code that explicitly checks this),

If it is empty(invalid) it will not run the user provided transformation
function, otherwise it will.

This is an example of *\'short-circuiting\'* out of the pipeline of
stacked transformations early on. So, if the first *Bind*
short-circuits, i.e. the box is empty (the validation phase fails), the
next *Bind* or *Map* will do the same and so they will not run the
user-provided transformation function, as the validation phase failed.

When the validation phase fails, it returns a 'bad' result which is what
other bind function will detect during their validation phase, in this
case whenever an empty Box is encountered, skipping the transformation
execute phase and returning the 'bad' or empty box to the next bind in
the pipeline -- which too will return early. In this way, the pipeline
has short-circuited and none of the other remining transformation
functions are run.

Many Language.Ext monads such as Option\<T\>, Either\<L,R\> and Try\<T\>
work like this.

Again, you will notice, that during each transformation, it is possible
to change the content's type in the monad being passed into the next
bind, as previously discussed.

Next we'll discuss the SelectMany() function which like Map/Select and
Bind is used to transform the contents of monads, and like the those, it
has a new requirement on the user-provided transformation function.

Monadic functions: Complete example (4)
---------------------------------------

Now let's look at a complete example showing all the operations on a
*Box\<T\>* using *Map*() and *Bind*() and introducing a new
*SelectMany*() extension method on *Box\<T\>:*

![](./myMediaFolder/media/image25.png)

<https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial04_methods_1/Program.cs>

This example starts by showing the passing of Monads into functions.
Each function *DoubleBox* n..4 will demonstrate a different variant to
help you get familiar with their usages and occurances.

*DoubleBox1* calls *DoubleNumbers* which accepts a Box\<int\[\]\> Monad
and transforms it using *Bind*, thus *DoubleNumbers* needs to return a
*Box\<T\>* type.

*DoubleBox2* instead using *Map* which uses a necessary function
*DoubleNumbersNoLift* to show that that function does not return a
*Box\<T\>* and as such the *Map* function can be used with it, which
will lift it into a Monad automatically.

These are familiar based on what we've learnt so far and it puts this
into practise, in a practical fashion.

*DoubleBox3* however uses Box\<T\>'s *SelectMany* function implicitly
through its use in the *Linq Query Expression Syntax*.

This is new, we will discuss this now. Lets look at how SelectMany() is
defined, as we've already seen how Select/Map and Bind look.

![](./myMediaFolder/media/image27.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial04\_methods\_1/Box.cs

You'll notice that this again, looks remarkably like the other monadic
function, Select/Map and Bind in as much as they follow the Validate,
Extract, Transform, and Lift/Load process. However, with SelectMany
there is an additional step: Once the transformation function is run,
bind in this case, a 2^nd^ new function can be provided by the user,
namely project.

Project will take the newly transformed and lifted Monad, along with the
original content and allow that to be transformed into a 3^rd^ Type, TC.

Its not clear yet why this additional bootstrapping is required until
you look at how the Linq Query Expression Syntax is used and how
powerful it is: it allows this function's internals (as read above) to
be exposed and manipulated dynamically in LINQ. Here is an example:

![](./myMediaFolder/media/image29.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial04\_methods\_1/Program.cs

*DoubleBox3* and *DoubleBox11* are equivalent with *DoubleBox3* being
the function used in the previous example. *DoubleBox11* is used here to
highlight how it actually works:

The first *from* statement calls *Select* on boxOfIntegers and exposes
the *Extract* phase of the call, effectively extracting the contents of
*boxOfIntegers* into *start*. The transformation function has not been
run yet - the transformation function is only run in the 2^nd^ *from*,
see the invocation of the user-provided transform function,
*DoTransformToAnyBox* which now using the item extracted from the first
*from, start* as its input(as one would expect for a transformation
function compatible with the bind function). This is effectively the
user-provided transform function used for passing into Bind. This
results in the transformed result, *startTransformed.*

So far, this has shown how the Linq Query Syntax can open up both the
*Select*() and *Bind*() functions and expose it as a Linq Query
Expression. The next part is is the select statement which is
effectively the *project* function called implicitly. The *project*
function has access to the start variable as well as the
*startTransformed* variable (its in scope) and as such it can use it to
perform a transformation expression using the two, as per the project
function definition:

![](./myMediaFolder/media/image31.png)

Note too, that the result of the *project* transformation function is
put into a *Box\<T\>* and so just like *Bind* and *Map*, need to return
a Monad.

This has shown how defining a *SelectMany*() function on the *Box\<T\>*
monad, allows LINQ access to specifying the *bind* transformation
function inline as well as the *projection* transformation function in
one LINQ query expression. The benefit of doing transformation this way
is clearly that you can define transformation functions inline and have
access to previous expression results, as they remain in scope!

This becomes more useful when you see it in an example with multiple
from statements, simulating chaining of the transformations:

![](./myMediaFolder/media/image33.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial07\_transformations\_2/Program.cs

We will investigate examples like this later, but for now its useful to
see how the LINQ query expression syntax can reach deep into *Select*(),
*Bind*() and *SelectMany*() and provides an alternative to specifying
transformation and chaining the results.

Next, we'll bring what we've learnt thus far into a cohesive example:

![](./myMediaFolder/media/image35.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial05\_methods\_2/Program.cs

Notice how transformation functions passed to Bind return Monads
explicitly, while those passed to Map do not. Also Notice we can use
either Map or Bind for transformation within a pipeline, but it becomes
necessary to choose/use a specific one depending on if or not the
provided transformation function returns a box or not (lifts or
doesn\'t), i.e. is transformed in a call to Bind() or Map().

Next, we'll spend more time concentrating on using the LINQ fluent vs
Expression syntax styles when working with monads.

Using LINQ Fluent and Expression syntax (6)
-------------------------------------------

The following shows the two approaches, which yield the same result but
look syntactically different. Remember, the LINQ expression syntax allow
access to previous transformation results, while the fluent notation,
only provides you access to the last transformation:

![](./myMediaFolder/media/image37.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial06\_Linq/Program.cs

Shown: Using the select many way, i.e. the LINQ expression syntax as
shown below allows an extracted item from the box, then to be passed
down a series of transforms by way of those transform functions being
compatible with the bind() phase of the *SelectMany*() function.

Each can see the prior transformation and can act on it subsequently.

And as each transformation function is run as part of the
*SelectMany*()'s implementation in Box, it will also be subject to the
VETL phases, which means if the input is not valid, it will return an
invalid value and subsequent transforms upon receiving that invalid
input will also return an invalid input and in all those cases, the
underlying transform is not run (short-circuiting). This is also an
illustration of Lazy-evaluation, where a function does not need to be
run unless is necessary (i.e. valid in this case).

I need to use *Map* in the transformation pipeline of *doubled2* because
*DoSomethingWith*() does not return a Box and the result of a *Map*(will
always do that).

The *doSomethingWith* function, represents a valid *Map* function to use
with a *Map*, as map will automatically lift this and so this function
does not have to lift its result. Note we don't have to return a
*Box\<T\>* because as we\'\'ll be running within a *SelectMany*()
expression - it automatically lifts the result, in this case an *object*
type. Note that this function will be acting as the bind()
transformation function within the *SelectMany*() function defined for
the Box class (see *SelectMany*())

The *DoubleNumbers* function is a valid bind function to be used in a
*Bind*() because it lifts the result.

This function does something with the numbers we extracted from the box
and then put them back in the box again because this function will be
run in the bind() phase of the *SelectMany*() function (see the
selectMany function implementation) and that function requires a
signature of : *int\[\] =\> Box\<int\[\]\>*

When to use *Map()* or *Bind()* (7)
-----------------------------------

As an extension of the previous example, this shows you how to structure
and deal with situations when choosing what function will be used in the
bind(), map() functions and how choices you make impact the subsequent
invocations of those functions when pipe-lining or chaining these
functions together

![](./myMediaFolder/media/image39.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial07\_transformations\_2/Program.cs

This shows that you can use either *Map* or *Bind* (they do the same
thing i.e. both transform their input) but map will lift automatically
and *Bind*() needs its transform function to explicitly do that.

This choice of using *Map* or *Bind* will only impact on how the
associated transformation functions require to produce either a lifted
(*Box\<T\>)* or a non-lifted result.

Next, we'll look into defining the imperative and declarative style.
Imperative refers the procedural view of having functions, and passing
data into them, while declarative refers to having data and passing
functions into them, *a-la* Monads!

Transitioning from Imperative to Declarative style (8)
------------------------------------------------------

Moving from traditional programming approaches which centre around
defining imperative construction of logic within programs, we well look
at what is required to convert from a Procedural way of thinking to
declarative and as a consequence a Pipeline way of thinking.

As previously discussed, pipelining allows the movement of data from one
Monad to the next, through a series of transformations that occurs
through the pipelines. This simulates what procedural code does by
calling function upon function to establish a set of logic for a
program. We can do this using declarative style also:

![](./myMediaFolder/media/image41.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial08\_declarative\_style/Program.cs

![](./myMediaFolder/media/image43.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial08\_declarative\_style/Program.cs

As can be seen from the above example, using either LINQ fluent or query
syntax as the base of defining a declarative transformations using
Monads to express logic.

Monad validation (9)
--------------------

When using a Monad like *Box\<T\>* in a *SelectMany* statement like LINQ
expression style used above, remember the implementation of the Monads
*SelectMany*(), i.e for each bind() phase of the *SelectMany*(), that
phase requires a function such PopulatePortfolioHoldings1() and any
following ones to transform the extracted item and then put it back into
a Box, so thats why each function must return a Box\<\> in the pipeline.

Also note how the logical way of planning the steps can be replicated in
both the procedural and pipeline ways (you don\'t have to think that
differently)

But wait, I can make a pipeline too without chaining Bind() or Map()
statements too? Why can\'t I just do this:

![](./myMediaFolder/media/image45.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial09\_automatic\_validation/Program.cs

The problem with this is you don't get automatic validation i.e. (V)ET
it's not short-circuit-able.

You could do it but it would mean every function would need to have
internal validation and check for invalid data whereas in a Monad like
Box, that validation is built into the Select and SelectMany()
implementation as thus us automatic each usage of those function on the
monad!

This is another key aspect of using monads.

Returning Monads: transformations always return a Monad (10)
------------------------------------------------------------

Let's have a bit of a recap:

*Map* and *Bind* both extract and validate the item within the box (that
it\'s not empty) i.e. do VETL and then proceeds to run the transform
function on it if it's not empty, otherwise returns empty box.

Both are equivalent in as much as they perform VETL but differ in what
form they require their transform function to either lift or not lift
the transformation (the function prototype must match what a *Map* or
*Bind*() function requires)

In the LINQ Query Expression Syntax method, Box\'s *SelectMany*() is
used to transform successive transformations, and you have access to
each of the transformed results, as well as much earlier
transformations. The final select statement is run via the *Box\<T\>\'s*
*Select*() function (technically this is the *projection*() function)
and therefor it will automatically be lifted and you don't need to do
it.

The Fluent mechanism also uses *Box\<T\>*\'s Map and Bind functions.

Each fluent style *Map* and *Bind* has access to the last transformation
before it, and unlike the LINQ expression syntax cannot see before the
last transformation (as that is the only input it gets).

Transformations from a call to *Bind* and *Map* must result in a
*Box\<T\>* either explicitly via *Bind*() or automatically via *Map*()

Your logical planning or thinking of logical programming tasks in your
design can equally be represented procedurally and using pipelining.

Another key aspect when using monads, particularly when they are
involved in successive chained calls such as within a pipeline, is that
each returns a monad that the other uses as input.

Extending the last example by removing the transformation function and
exposing them as inline lambda expressions, you can see how each must
conform the function prototypes for transformation functions within the
Map() or Bind() functions that exist on Box\<T\>:

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial10\_returning\_monads/Program.cs

Built-in validation and short-circuiting in Monads (11)
-------------------------------------------------------

We now turn to a specific example of short-circuiting behaviour of
monads with special emphasis in seeing it in action when using LINQ
query expression syntax:

![](./myMediaFolder/media/image48.png)

![](./myMediaFolder/media/image49.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial11\_short\_circuiting/Program.cs

An empty box, i.e aBoxOfNumbers2 will cause the entire pipeline to
return an Empty Box\<T\> instead of the ultimate result of transforming
all the boxes contents. This is by design and later when this is not
desired, you can use LanguageExt's *match*() function to determine how
to deal with invalid data and how the pipeline should proceed. For now
we'll leave this ideas until later, when we cover *match*.

A diversion: composition of functions (12)
------------------------------------------

Now we turn our attention to function composition, see:
<https://github.com/louthy/language-ext/wiki/Thinking-Functionally:-Function-composition>
for more details.

Scenario:

You have a program that has existing functions. You change one of those
functions to now return a Monad. You need to ensure that you program
still works i.e. existing functions can use your Monad returning
function, even though it does not expect a Monad!

So, we\'ll compose a new function that will take the monad, and adapt it
to the interface of the original function.

![](./myMediaFolder/media/image51.png)

https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial12\_function\_compositionial12/Program.cs

A diversion: Pure Functions (13)
--------------------------------

A pure function\'s return value is a product of its arguments i.e. only
its arguments are used to determine the return value.

The expectation is that if it does this, then the same input to the
function will yield the same output too.

To ensure this, you need to further restrict the function to not
use/depend on anything that might jeopardise this, i.e. fetch/use a
source that today might be one thing and tomorrow might be something
else.

For instance, if you get a value from the DB today, tomorrow it might be
removed and then the guarantee you made that the function will return
the same value breaks.

So, you can\'t use Input/output or anything that is a source of
changeable circumstances.

Side note: Pure functions are immediately parallelizable and can be used
for Memoization (storing the result and input of the function requires
that the function never needs to run again)

![](./myMediaFolder/media/image53.png)

<https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial13_pure_functions/Program.cs>

The last three sections don't fit well with the narrative of the
proceeding sections. Should better integrate them with relatable use
cases.There might be some better examples in Mazer.

Part II: Language-Ext
=====================

The proceeding section will have suitably prepared you for your fist
encounter with LanguageExt monads, how they work and what you can come
to expect from them. Essentially, they are not very different to the
Box\<T\> monad.

We'll cover Either, Option and Try Monads and everything you've learnt
thus far about Box\<T\> applies to them also, including calling there
Map, Bind and using them in LINQ expressions in a declarative way.

The Either\<L,R\> Monad
-----------------------

### Introducing the Either monad (14)

This tutorial shows you what a what an Either\<\> type is and how to use
it generally

![](./myMediaFolder/media/image55.png)

### Operations on Either\<L,R\>

#### Using BiBind (15)

This tutorial shows you what a what an Either\<\>\'s BiBind()
functionality

![](./myMediaFolder/media/image57.png)

#### Using BiExists

This tutorial shows you what a what an Either\<\>\'s BiExists()

![](./myMediaFolder/media/image59.png)

#### Using Fold() to change an initial state over time based on the contents of the Either

This tutorial shows you what fold() does

![](./myMediaFolder/media/image61.png)

#### Using Iter

Iter: run an arbitary function on the Either\<\> if its value is right
type or chose BiIter() to specify a function to run on both types

![](./myMediaFolder/media/image63.png)

#### Using BiMap (19)

Using BiMap() to make provision for a transform function for both the
left and right types of the either. The transform is automatically
lifted.

![](./myMediaFolder/media/image65.png)

#### Using BindLeft (20)

Shows the basics of Either\<L,R\>, using BindLeft() to make provision
for a transform function for the left types of the either (which is
unusual for the default Bind() function).

The transform is NOT automatically lifted(this is a bind() after all).

![](./myMediaFolder/media/image67.png)

#### Using Match (21)

Using Match to extract the contents of an Either\<\> but not put it back
into and either types (as map() and Bind() would do)

![](./myMediaFolder/media/image69.png)

### Operating on Lists of Either\<left, Right\>

#### Using BiMapT and MapT

This tutorial shows you how you can transform a List of Eithers,
effectively doing a Map on each either in the list, and this Bi variety
allows you to specify how make provision to map/transform both types

When a Bind/Map function is called on a list of monads, it is BindT or
BiMapT, otherwise operating on a single monad, use Bind() or Map() alone

![](./myMediaFolder/media/image71.png)

#### Using BindT

This tutorial shows you how you can transform a List of Eithers,
effectively doing a Bind on each either in the list and a Bi variety
allows you to specify how make provision to map/transform both types

![](./myMediaFolder/media/image73.png)

#### Using IterT

This tutorial shows you how you can call a function on each right value
for the list of monads in the list, using IterT() (Either is a monad)

![](./myMediaFolder/media/image75.png)

#### Using Apply

Using Apply both on a simple Either\<\> and a List of Eithers to
demonstrates its simplicity

![](./myMediaFolder/media/image77.png)

#### Using Partition

Using Partition to easily get both the lefts() and the Rights() in one
call - as a tuple of (lefts,rights)

![](./myMediaFolder/media/image79.png)

#### Using Match

Shows the basics of transforming a list of Eithers using match to
understand whats in them (both left and right values) and

then transform them based on their values into a single type that
represents either in one way (a string)

Along with Map() and Bind() this extracts the value from the Either and
provides transformation functions for both Left and Right sides of the
Either

![](./myMediaFolder/media/image81.png)

The Option\<T\> Monad
---------------------

### Introduction to Option\<T\> (28)

Option\<T\> type effectively removes the need to use NULL in your code.
Nulls can produce unexpected behavior and as such have no place in pure
functions where unexpected behavior would

render them otherwise impure

![](./myMediaFolder/media/image83.png)

### Basic use-case of Option\<T\> (29)

![](./myMediaFolder/media/image85.png)

### Using Option\<T\> in functions (passing in and returning)

Contrived example of passing around Option\<T\> arguments

![](./myMediaFolder/media/image87.png)

### using IfSome() and IfNone()

Demonstrates the usage of IfNone and IfSome which runs a user defined
function provided the option is None or Some respectively

![](./myMediaFolder/media/image89.png)

### Pipelining with Options\<T\> (32)

Rosetta code! Procedural -\> Fluent -\> Query Syntax

![](./myMediaFolder/media/image91.png)

### Using ToEither\<\>

ToEither extension method to convert a value to a right sided
Either\<L,R\>

![](./myMediaFolder/media/image93.png)

### Using BiMap() (34)

Bimap

![](./myMediaFolder/media/image95.png)

The Try\<T\> Monad
------------------

### Supressing Exceptions

This tutorial demonstrates the use of the Try\<\> Monad.

![](./myMediaFolder/media/image97.png)

Part III: Everything else
=========================

Bonus
-----

### ThrowIfFailed() and introducing Either\<IAmFailure, Option\<T\>\>

ThrowIfFailed and a way to make functions return a standard return type
of an Either of Right(T) or a failure(Left).

T can be any type your function deals with, as you\'d use in any normal
function you create, only you make it an Option.

You also bundle with your return type a failure if there is one, by
returning an all encompassing return value of Either\<IAmFailure,
Option\<T\>\>

Program.cs

![](./myMediaFolder/media/image99.png)

CustomExtensions:

![](./myMediaFolder/media/image101.png)

IamFailure:

![](./myMediaFolder/media/image103.png)

### Using custom extension method FailureToNone()

We can convert a \'failed\' standard wrapped function to a None. This is
helpful if you want to turn a failure into a \'valid\' standard function
either but with None Right value

![](./myMediaFolder/media/image105.png)

### Memoization

This tutorial exposes how functional programming, particularly caching
results from pure functions, aids memoization, as they always return the
same output for same input.

![](./myMediaFolder/media/image107.png)

Now because we\'ve cached decrypted results for phrases, when we see
that encrypted phrase we can use the cached decrypted result for that
encrypted phrase to get without having to run the SimpleDecrypt()
function again.

This is the same with all caching mechanism, which make the obvious seem
transparent, however with a pure function, you know for certain that
there is no chance that our cached decrypted result could be different
from running a SimpleDecrypt() on the encrypted string we have - so we
have double certainty that we dont have to run the SimpleDecrypt()
function.

If SimpleEncrypt() or SimpleDecrypt() could sometimes return different
outputs for the same input, then we\'d have to cal SimpleDecrypt() to
return what the decrypted result is that/this time.

Note using Monads, Select, Bind() within your functions you\'re making
it unlikely that you functions will

never throw exceptions becasue you\'re catering for both the expected
and unexpected data by virtue of using Monads (which ensure that you
need to ie. they contain both failure and success logic such as
Some/None or Either left or right emedded into themselves so you can and
indeed have to cater for them)

In catering for them by extracting their values using Match() or
transforming via Select()/Bind()/Map().

Now use cache to decrypt known inputs against outputs produced by pure
function SimpleEncrypt():

### Apply events over time to change an object

This tutorial shows how you can use the Fold() function in languageExt
to change the state of an object over time

![](./myMediaFolder/media/image109.png)

![](./myMediaFolder/media/image111.png)

![](./myMediaFolder/media/image113.png)

![](./myMediaFolder/media/image115.png)

![](./myMediaFolder/media/image117.png)

![](./myMediaFolder/media/image119.png)

![](./myMediaFolder/media/image121.png)

### Smart constructors and Immutable data-types

This tutorial demonstrates creating immutable objects using Smart
Constructors, and using them as you would use any other OOP objects.

However, this object is immutable in as much as its specifically
designed not to have its state changed by operations

The operations that it does have, create a new object with the
modification and leaves the original object untouched.

![](./myMediaFolder/media/image123.png)

![](./myMediaFolder/media/image125.png)

![](./myMediaFolder/media/image127.png)
