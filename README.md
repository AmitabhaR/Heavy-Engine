# Heavy-Engine 2

This is the official site of Heavy-Engine 2 .

# Contents
1. Source Code
2. Downloadable Binaries
3. Help Centre
4. Documentation

The project is licensed under GNU GPL v2.0 . Please see the LICENSE file for more information .
We give you the source code with a strict aggrement that you will remove the name of the original author of the program .
For more information , please contact us at reo.studio@yahoo.com . Documentation on the way . 

# Platforms
	1. Windows PC (Managed)
	2. Windows PC Native (x86)
	3. Java Desktop
	4. Java Mobiles
    5. Linux Native. 	

# Building the Engine

#### Requirements
	 1. MSBuild 2013 + Visual Studio 2013.
	 2. Java SDK.
	 3. Java ME SDK.
	
### Setup Environment Variables
	1. Open Environment Variables Editor.
	2. Select PATH variable and add MSBuild bin folder path , add Java SDK bin folder path , add Java ME SDK bin folder path.
	3. Restart your system.		
		
### Compile BosCompiler
	1. For compiling BosCompiler , download Orwell Dev-Cpp (http://sourceforge.net/projects/orwelldevcpp/files/latest/download) or download CodeBlocks (http://sourceforge.net/projects/codeblocks/files/Binaries/13.12/Windows/codeblocks-13.12mingw-setup.exe/download).
	2. Create a new project and add all the source files.
	3. Build the EXE and compiling is finished.

### Compile PackageManager
	1. Open the .csproj file in the folder PackageManager
	2. Run Build Solution that makes a dll file.
	
### Compile C# Runtime
	1. Create a visual studio class library project .
	2. Add Runtime.cs file .
	3. Create a dll project.
	4. Compile and get the dll file.

### Compile Java Runtime
	1. Create a java class library project.
	2. Add the folder / package jruntime to your project for proper compile.
	3. Compile and get the jar file.

### Compile Java ME Runtime
	1. Create a java mobile class library project 
	2. Add the folder / package jruntime to your project for proper compile.
	3. Compile an get the jar file.

### Compile Windows Native Runtime
	1. Open VS2013 and create a new static library project for c++.
	2. Add all the files present in the Windows Native Runtime folder.
	3. Hit Build button to compile to the final .lib file.

### Compile Linux Native Runtime
	1. Download all the required SDL libraries (SDL2,SDL2_image,SDL2_mixer,SDL2_ttf,SDL2_net) using sudo if using linux
	   or include all the .a SDL library files and all the header files and make sure all of them are stored inside a SDL2 folder.
	2. Open the code-blocks project in linux or if using a cross-compiler , in windows.
	3. Hit build and compiling should begin.
	
### Compile Engine
	1. Open the Heavy-Engine.sln in the visual studio editor.
	2. Click re-build to re-make the exe file.

### Finally
 	1.Open the folder pre-compiled.
	2.Copy all the files and folders like that pattern and run the engine.
	
# Other Options
In the pre-compiled folder , we have compiled and given you the latest build . You can download that if you don't want to re-compile the engine.

# Protection
For protection from decompilation , you can use Esiriz .NET Reactor for encryting / encoding the source code . This is only for official purposes only.

# Notice
1.We are not providing the source code of FileZip and GameBuilder because according to the terms and conditions of the FileZip software , the software is free but not be shared it's source code . GameBuilder also contains the same license.
2.We are providing the bos compiler with x64 Optimized executable , if you need to make one , please prefer microsoft compilers.

Release Version : 2.0 Beta 1.3

#### 															Reo Studio 2015 - 2016
