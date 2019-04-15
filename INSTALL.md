# Installing and running VeriSol

## Dependencies

> NOTE: We currently provide instructions for Windows. Instructions for Linux coming soon!

Install .NET Core 2.2 from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106) (for Windows)

The following dependecies are needed to run VeriSol on a Solidity program. There are two categories of dependencies:
   - Translating Solidity to Boogie program
   - Run the verifier
   - Viewing the Corral defect trace in Solidity source 

### Depedencies for translating Solidity to Boogie 
   - __Solidity compiler__. Download the Solc binary for Windows or Linux from [here](https://github.com/ethereum/solidity/releases/tag/v0.4.24). We have currently tested with version __0.4.24__. Place the executable (solc.exe for Windows, or solc-static-linux for Linux) in the **Tool** folder.
   
### Dependencies for running verifier

We use **Corral** as a git submodule. Corral in turn uses **Boogie** as a submodule. 
Run a recursive git submodule update command from the root folder

> git submodule update --recursive --init

Make sure that Corral and Corral\Boogie folders are populated. 
Let us denote %CORRAL_DIR% as corral\bin\debug\ and %BOOGIE_DIR% as corral\boogie\binaries\ folders.

   - __Z3 theorem prover__. Download **z3.exe** from [here](https://github.com/Z3Prover/z3), and place it in both Boogie and Corral binaries directories. We have only tested versions **4.8.0** or below.
   
### Dependencies for viewing Corral defect traces in source code
   - __Concurrency explorer__. There is a version of [ConcurrencyExplorer](https://github.com/LeeSanderson/Chess) in Corral\Tool\. Denote this as _%CONCURRENCY_EXPLORER_DIR%_.
%Download the sources and build the sources of **ConcurencyExplorer** from [here](https://github.com/LeeSanderson/Chess), and denote _%CONCURRENCY_EXPLORER_DIR%_ as the path containing **ConcurrencyExplorer.exe**.

## Build VeriSol

Open the __Sources\SolToBoogie.sln__ file in Visual Studio (2017) and perform __Build Solution__. 

## Running VeriSol

### Translate Solidity to Boogie
Assuming the root folder of this repository is *VERISOL_PATH*, run 

`dotnet %VERISOL_PATH%\Sources\SolToBoogie\bin\Debug\netcoreapp2.2\SolToBoogie.dll a.sol %VERISOL_PATH% out.bpl`

For pretty print viewing, run

 `%BOOGIE_DIR%\boogie.exe out.bpl /noVerify /doModSetAnalysis /print:pretty.bpl`


### Run verifier
See the paper [here](https://arxiv.org/abs/1812.08829) for details of what these verification terms mean.

*Sound verification* of the Boogie program (unbounded verification using invariant inference)

`%BOOGIE_DIR%\Boogie.exe -doModSetAnalysis -inline:assert -noinfer -contractInfer -proc:BoogieEntry_* out.bpl`

*Transaction-bounded verification* of the Boogie program (using Corral), with unrolling depth (replace **4** with desired depth) for a top-level contract (replace **Foo** with the contract name):

`%CORRAL_DIR%\corral.exe /recursionBound:4 /k:1 /main:CorralEntry_Foo /tryCTrace out.bpl /printDataValues:1`

If Corral throws an exception, try adding "/trackAllVars " to the list of parameters above.  

### View traces from Corral
If Corral generates a defect (look at output of Corral and **corral_out_trace.txt** file in the same folder), view it using ConcurrencyExplorer: 

`%CONCURRENCY_EXPLORER_DIR%\ConcurrencyExplorer.exe corral_out_trace.txt`

## Regression script

To run the regressions, build *SolToBoogie.sln*, install Corral, go to **%VERISOL_PATH%\test\regressions**, and run

`dotnet %VERISOL_PATH%\Sources\SolToBoogieTest\bin\Debug\netcoreapp2.2\SolToBoogieTest.dll <Path to Corral.exe> %VERISOL_PATH%`

All regressions are expected to pass. 

To run a subset of examples during testing, add an optional parameter to limit the above run to a subset of tests that match a prefix string *<prefix>*

`dotnet %VERISOL_PATH%\Sources\SolToBoogieTest\bin\Debug\netcoreapp2.2\SolToBoogieTest.dll <Path to Corral.exe> %VERISOL_PATH% [<prefix>]`

> Instructions to run the regressions on Linux can be found [here](https://github.com/Microsoft/verisol/wiki/How-to-run-regressions-in-Linux). 

