# Celestial Compiler

Prequisites:
* `python3`
* `antlr4`
* `prettytable`
* `argparse`

Instructions to compile a Celestial source:
```
$ python3 ./main.py [--outputDir="path_to_output_directory"] <path to celestial source code>
```

The F* will be generated in the current directory (if output directory is not specified) with the same name as the contract.

The Solidity file will be named `contract.sol`