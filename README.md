
# VeriSol

VeriSol (Verifier for Solidity) is a prototype formal verification system for smart contracts developed
in the popular [Solidity] (https://solidity.readthedocs.io/en/) programming language. It is based on translating
programs in Solidity language to programs in [Boogie](https://github.com/boogie-org/boogie) intermediate 
verification language, and then leveraging the verification toolchain for Boogie programs. 

The following paper describes the design of VeriSol and applications in [Azure Blockchain](https://azure.microsoft.com/en-us/solutions/blockchain/).

> __Formal Specification and Verification of Smart Contracts for Azure Blockchain__, Shuvendu K. Lahiri, Shuo Chen, Yuepeng Wang, Isil Dillig, https://arxiv.org/abs/1812.08829

## Dependencies

The following dependecies are needed to run VeriSol on a Solidity program. There are two categories of dependencies:
   - Translating Solidity to Boogie program
   - Run the verifier

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

## License
VeriSol is licensed under the MIT license. 

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
