Introduction
------------

This is a tutorial that aims to demonstrate how to use LanguageExt in
C\# programs with an emphasis on the practical side, demonstrating
through code examples and providing narrative throughout the process.

### Introduction to the Box type (a Monad)

A Monad is a just type, much like any user-defined type that is created
when designing a class. It has its own properties that represent its
state, and methods that give the Monad behaviour and, in many cases,
manipulates its state through a public interface it provides to the
programmers to use.

A simple example of a Monad might be a Box type:

![](./myMediaFolder/media/image2.png){width="7.270138888888889in"
height="0.36180555555555555in"}

A *Box\<T\>,* can hold any type but in this case, it holds and makes
provision for an integer type, i.e. *Box\<int\>*.

Look at the implementation of the *Box\<T\>*, notice it's just a C\#
Class that's been created:

![](./myMediaFolder/media/image4.png){width="7.270138888888889in"
height="4.56875in"}

So far this looks like a normal C\# class for the Box type. You can
create a *Box\<T\>* by specifying the type of contents it can hold, and
internally to the class, it is stored as a member called *\_item*.
Additionally, you can set the contents of the *Box\<T\>* through its
*Item* property. Thus is it possible to extract and set the contents of
the *Box\<T\>*:

![](./myMediaFolder/media/image6.png){width="7.270138888888889in"
height="0.26319444444444445in"}

Monads are types that allow you to transform the contents of the Monad,
in this case the contents of the *Box\<T\>* which is currently set to
99. A series of defined functions need to exist on a Monad which will
define the kinds of transformations that can occur -- they are expected
to exist. If a type has a *Map, Select* and a *Bind* function, it can
*generally* be considered a Monad.

#### Transforming the contents of a Monad

The important thing to understand is that whatever the transformation
will be, it needs to be provided by the programmer. Calling one of these
functions, will then read the contents of the Monad and change the
contents somehow -- the programmer will provide a *Higher-Ordered
Function* (HOF), which will define the transformation. The
transformation function, such as *Select* or *Bind* acts an execution
environment, passing its internal contents into the HOF, where
additional checks can be done before or after the transformation is
started.

The *Select* function does exactly this, it passes the contents of the
*Box\<T\>* monad into the user-defined transformation function that was
passed into *Select* function and executes that function and the results
thereof i.e. the *transformation* become the result of the *Select*
function call:

![](./myMediaFolder/media/image8.png){width="7.270138888888889in"
height="2.1590277777777778in"}

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

#### Using the Select function on a Monad

*Box\<T\>*'s *Select* function is called by the programmer like this:

![](./myMediaFolder/media/image10.png){width="7.270138888888889in"
height="0.17083333333333334in"}

The *x =\> + 1* syntax is a definition of a lambda function that takes
in one argument and the result of the expression after the *=\>* is the
result, and as such it is a suitable transformation function to pass
into the *Select* function, provided it matches the *map* parameter's
types as defined by *Func\<TA, TB\>.*

Incidentally, this notation of explicitly calling the *Select* function,
as defined on the *Box\<T\>* type is called the *Linq Fluent Syntax*. An
alternative is to use the *Linq Query Expression Syntax*:

![](./myMediaFolder/media/image12.png){width="7.270138888888889in"
height="0.5534722222222223in"}

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

![](./myMediaFolder/media/image14.png){width="7.270138888888889in"
height="2.28125in"}

<https://github.com/stumathews/UnderstandingLanguageExt/blob/master/Tutorial01_monad/Program.cs>

Many Monads in Language-Ext are utilized by taking advantage of C\#'s
*Query Expression Syntax* notation for accessing the *Select* function
in this way. This is also largely due to being able to define an
extension method for any type this way.

When working with Language-Ext in general, the already defined monad
types such as *Option\<T\>*, *Either\<L,R\>* and *Try\<T\>* already have
suitable *Select*, *Map* and *Bind* functions defined and as such its
not needed to define them, only to pass in our transformation function,
that is unless you want to create our own Monad like our *Box\<T\>*
monad!

Next, we'll put the Select function aside, and discuss its relatives
*Map* and *Bind*.

### Map and Bind (also the construction of a Monad Type)

### Pipelining

### Performing operations on a Box using Map() and Bind(), Select() and introducing SelectMany()

### More examples of performing operations on a Box

### Using Linq Fluent and Expression syntax

### More on Map() and Bind()

### The transition from Imperative style coding to Declarative style coding with an example

### Pipelines include automatic validation

### The Bind transformation function always return a Monad

### Monad built-invalidation and short-circuiting

### Composition of functions

### Pure Functions

Either Monad. 
--------------

### Using BiBind()

### Using BiExists()

### Using Fold() to change an initial state over time based on the contents of the Either

### Using Iter()

### Using BiMap()

### Using BindLeft()

### Using Match()

### Operating on Lists of Either\<left, Right\>

### Using BiMapT and MapT

### Using BindT

### Using IterT

### Using Apply

### Using Partition

### Using Match

Option Monad
------------

### Introduction to Option\<T\>

### Basic use-case of Option\<T\>

### Using Option\<T\> in functions (passing in and returning)

### using IfSome() and IfNone()

### Creates an entire application of just functions via pipelineing which returning and receive Option\<T\>

### Using ToEither\<\>

### Using BiMap()

Custom/Specific 
----------------

Using custom extension method ThrowIfFailed() and introducing
Either\<IAmFailure, Option\<T\>\> as a standard return type for all
functions

Using custom extension method FailureToNone()

Using pure functions to cache things, ensuring that you need not call
expensive calls if they\'ve been done once already. (Bonus: FP concepts)

Changing state of an object over time (Fold) including concept of apply
events over time to change an object (Bonus: FP concepts)

Immutability - Designing your objects with immutability in mind: Smart
constructors and Immutable data-types (Bonus: FP concepts)

Try - Supressing Exceptions
