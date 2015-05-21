# Heavy-Engine 2
Heavy-Engine is a open-source and multi-platform game engine developed in C#  . It's main aim is to reduce the task of game making . The game engine is licensed under GNU GPL v2.0 . Please see the LICENSE file for more information .
We give you the source code with a strict aggrement that you will remove the name of the original author of the program . 

# Features

List of features.

1. Level Editor.
2. Multi - Platform.
3. Free and open - source.
4. Friendly UI.
5. Quick Designing.

# Platforms

List of current and up-coming platforms.

1. Windows PC.
2. Java Desktop.
3. Java Mobiles. 	
4. Java Web.      	(Under Development)
5. HTML5.  			(Under Confirmation)

From the above , the platforms which are marked 'Under Development' are being developed and the one which are marked 'Under Confirmation' are yet not confirmed.

# Building the Engine

Building the engine requires .NET Framework , lastest version is preferred.
Note : Read all the steps before building the engine is recommended.

Compile BosCompiler
===================
1. Open Makefile in BosCompiler and change the .../ paths with the compiler default paths.
2. Run make in command line.

Compile C# Runtime
==================
1. Create a visual studio project and add Runtime.cs file . Create a dll project.
2. Compile and get the dll file.

Compile Java Runtime
====================
1. Create a java class library project and add the folder / package jruntime to your project for proper compile.
2. Compile and get the jar file.

Compile Java ME Runtime
==================
1. Create a java mobile class library project and the folder / package jruntime to your project for proper compile.
2. Compile an get the jar file.

Compile Engine
==============
1. Setup the PATH environment variable with the path of the .net framework directory.
2. Setup the PATH environment variable with the path of the JDK directory.
3. Setup the PATH environment variable with the path of the JavaME SDK directory.
4. Create a visual studio C# project and add all the files and folders in Engine directory.
5. Build the exe file.
6. Copy all the binaries you compile before to the directory of the exe file.
7. Copy also midp_2.0.jar and cldc_1.0.jar and all other jar files form the pre-compiled directory.
6. Run the Engine.

Now we also provide pre-compiled binaries , if you don't want to re-build it , you can use the pre-compiled binaries .

Release Version : 1.4 (Late Release).