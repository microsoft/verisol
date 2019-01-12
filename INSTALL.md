# Installing and running VeriSol

## Dependencies

The following dependecies are needed to run VeriSol on a Solidity program. There are two categories of dependencies:
   - Translating Solidity to Boogie program
   - Run the verifier

> NOTE: We currently provide instructions for Windows. Instructions for Linux coming soon!

### Depedencies for translating Solidity to Boogie 
   - __Solidity compiler__. Download the Solc binary for Windows or Linux from [here](https://github.com/ethereum/solidity/releases/tag/v0.4.24). We have currently tested with version __0.4.24__. Place the executable (solc.exe for Windows, or solc-static-linux for Linux) in the **Tool** folder.
   
### Dependencies for running verifier
   - __Boogie verifier__.
   - __Corral verifier__.
   - __Z3 theorem prover__.
   - __Concurrency explorer__.

## Build VeriSol

Open the __Sources\SolToBoogie.sln__ file in Visual Studio (2017) and perform __Build Solution__. 

## Running VeriSol

### Translate Solidity to Boogie
Assuming the root folder is *VERISOL_PATH*, run 

> %VERISOL_PATH%\Sources\SolToBoogie\bin\Debug\netstandard2.0\SolToBoogie.dll a.sol %VERISOL_PATH% out.bpl

### Run verifier
Coming soon!

## Regression script

Coming soon!
