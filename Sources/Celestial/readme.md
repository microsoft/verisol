# Celestial
***
Celestial is a Microsoft Research project for prototyping a framework for formally verifying smart contracts written in the Solidity language for the Ethereum blockchain. CELESTIAL allows programmers to write expressive functional specifications for their contracts. It translates the contracts and the specifications to F\* to formally verify, against an F\* model of the blockchain semantics, that the contracts meet their specifications. Once the verification succeeds, CELESTIAL performs an erasure of the specifications to generate Solidity code for execution on the Ethereum blockchain. 
Alternatively, one can choose to employ VeriSol as the verification backend. In this case, CELESTIAL generates a VeriSol-compatible Solidity source code to formally verify that the contract meets its specifications. 

We use CELESTIAL to verify several real-world smart contracts from different application domains such as tokens, digital wallets, and governance. 

## Installation
See [this](https://github.com/FStarLang/FStar/blob/master/INSTALL.md) for installing FStar.

See [this](https://github.com/microsoft/verisol/blob/master/INSTALL.md) for installing VeriSol.

### Prequisites for using Celestial:
* `python3`
* `antlr4`
* `prettytable`
* `argparse`

---
### Instructions to compile a Celestial source:
```
$ python3 ./main.py <path to celestial source code> [--v="FStar|VeriSol"] [--fstDir="path_to_output_directory_for_FStar_code"]
[--solDir="path_to_output_directory_for_Solidity_code"] 
```

The command line argument ```--v``` specifies the verification mode. If not specified, it defaults to FStar, meaning compilation will generate both F\* code with specifications and Solidity code without specifications. If verification mode specifies VeriSol as intended verification backend, then Solidity code is generated with specifications that are supported by VeriSol. 

The F\* code will be generated in the current directory (if fstDir is not specified) with the same name as the contract.

The Solidity file will be named `contract.sol`.

---
### Instructions to verify the generated code using F\*
```
fstar.exe --include ../Compiler/lib [--z3rlimit 50] <path_to_generated_FStar_code>
```
One can increase ```z3rlimit``` if required. More details about FStar can be found [here](https://github.com/FStarLang/FStar).

---
### Instructions to verify the generated code using VeriSol
```
VeriSol <path_to_top_level_Solidity_file_with_spec> <name_of_the_top_level_contract_to_analyze>
```
More details about VeriSol can be found [here](https://github.com/microsoft/verisol/blob/master/INSTALL.md).

---