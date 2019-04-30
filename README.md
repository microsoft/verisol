
# VeriSol

VeriSol (Verifier for Solidity) is a prototype formal verification and analysis system for smart contracts developed
in the popular [Solidity](https://solidity.readthedocs.io/en/) programming language. It is based on translating
programs in Solidity language to programs in [Boogie](https://github.com/boogie-org/boogie) intermediate 
verification language, and then leveraging the verification toolchain for Boogie programs. 

The following paper describes the design of VeriSol and application of smart contract verification for [Azure Blockchain](https://azure.microsoft.com/en-us/solutions/blockchain/).

> __Formal Specification and Verification of Smart Contracts for Azure Blockchain__,  Yuepeng Wang, Shuvendu K. Lahiri, Shuo Chen, Rong Pan, Isil Dillig, Cody Born, Immad Naseer, https://arxiv.org/abs/1812.08829

## INSTALL

Instructions for installing and running VeriSol can be found [here](https://github.com/Microsoft/verisol/blob/master/INSTALL.md
).

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
