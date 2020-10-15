# Celestial Compiler

Prequisites:
* `python3`
* `antlr4`
* `prettytable`
* `argparse`

Instructions to compile a Celestial source:
```
$ python3 ./main.py <path to celestial source code> [--v="FStar|VeriSol"] [--fstDir="path_to_output_directory_for_FStar_code"]
[--solDir="path_to_output_directory_for_Solidity_code"] 
```

The command line argument ```--v``` specifies the verification mode. If not specified, it defaults to FStar, meaning compilation will generate both F\* code with specifications and Solidity code without specifications. If verification mode specifies VeriSol as intended verification backend, then Solidity code is generated with specifications that are supported by VeriSol. 

The F\* code will be generated in the current directory (if fstDir is not specified) with the same name as the contract.

The Solidity file will be named `contract.sol`.