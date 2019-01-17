# Installing and running VeriSol

## Dependencies

The following dependecies are needed to run VeriSol on a Solidity program. There are two categories of dependencies:
   - Translating Solidity to Boogie program
   - Run the verifier
   - Viewing the Corral defect trace in Solidity source 

> NOTE: We currently provide instructions for Windows. Instructions for Linux coming soon!

### Depedencies for translating Solidity to Boogie 
   - __Solidity compiler__. Download the Solc binary for Windows or Linux from [here](https://github.com/ethereum/solidity/releases/tag/v0.4.24). We have currently tested with version __0.4.24__. Place the executable (solc.exe for Windows, or solc-static-linux for Linux) in the **Tool** folder.
   
### Dependencies for running verifier

   - __Boogie verifier__. Download and build the Boogie sources from [here](https://github.com/boogie-org/boogie
), and denote _%BOOGIE_DIR%_ as the path to the folder containing **Boogie.exe**(and other dlls).
   - __Corral verifier__. Download and build the Corral sources from [here](https://github.com/boogie-org/corral
), and denote _%CORRAL_DIR%_ as the path to the folder containing **Corral.exe** (and other dlls).
   - __Z3 theorem prover__. Unless you already have **z3.exe** installed as part of Boogie/Corral download, 
   download **z3.exe** from [here](https://github.com/Z3Prover/z3), and place it in both _%BOOGIE_DIR%_ and _%CORRAL_DIR%_. We have only tested versions **4.8.0** or below.
   
### Dependencies for viewing Corral defect traces in source code
   - __Concurrency explorer__. Downlaod the sources and build the sources of **ConcurencyExplorer** from [here](https://github.com/LeeSanderson/Chess), and denote _%CONCURRENCY_EXPLORER_DIR%_ as the path containing **ConcurrencyExplorer.exe**.

## Build VeriSol

Open the __Sources\SolToBoogie.sln__ file in Visual Studio (2017) and perform __Build Solution__. 

## Running VeriSol

### Translate Solidity to Boogie
Assuming the root folder of this repository is *VERISOL_PATH*, run 

`dotnet %VERISOL_PATH%\Sources\SolToBoogie\bin\Debug\netcoreapp2.0\SolToBoogie.dll a.sol %VERISOL_PATH% out.bpl`

For pretty print viewing, run

 `%BOOGIE_DIR%\boogie.exe out.bpl /noVerify /doModSetAnalysis /print:pretty.bpl`


### Run verifier
See the paper [here](https://arxiv.org/abs/1812.08829) for details of what these verification terms mean.

*Sound verification* of the Boogie program (unbounded verification using invariant inference)

`%BOOGIE_DIR%\Boogie.exe -doModSetAnalysis -inline:assert -noinfer -contractInfer -proc:BoogieEntry_* out.bpl`

*Transaction-bounded verification* of the Boogie program (using Corral), with unrolling depth (say 4) for a top-level contract Foo 

`%CORRAL_DIR%\corral.exe /recursionBound:4 /k:1 /main:CorralEntry_Foo /tryCTrace out.bpl /printDataValues:1`

If Corral throws an exception, try adding "/trackAllVars " to the list of parameters above.  

### View traces from Corral
If Corral generates a defect (look at output of Corral and **corral_out_trace.txt** file in the same folder), view it using ConcurrencyExplorer: 

`%CONCURRENCY_EXPLORER_DIR%\ConcurrencyExplorer.exe corral_out_trace.txt`

## Regression script

Coming soon!
