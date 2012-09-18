MiniLight.NET
=============
> *MiniLight is a minimal global illumination renderer. It is primarily an exercise in simplicity.
> But that makes it a good base and benchmark (in some sense) for development and experimentation.
> And it just might be the neatest renderer around (on average, about 650 lines).
> There are translations into several programming languages.*

From the [MiniLight home page](http://www.hxa.name/minilight/)

Motivation
----------
As the C# version of MiniLight [linked](http://www.lomont.org/Software/#GraphicsDemos)
on the [MiniLight page](http://www.hxa.name/minilight/) throws an StackOverflowException
after a while (sometimes as soon as on the third iteration), I thought I'd
give it a try and rewrite the code from scratch. I will be using the C/C++ code as a reference.

Features
--------
Right now I'm aiming at the same feature set as the C/C++ version of MiniLight but I'm going to
add a GUI that will at least show the current iteration of the render in progress.

Tools of the trade
------------------
* I am using Visual Studio 2012 (thanks to MSDN AA) as my IDE
* Unit testing will be done in Visual Studio 2012 as well
* For the documentation I will use [Natural Docs](http://www.naturaldocs.org) or [Sandcastle](http://sandcastle.codeplex.com)