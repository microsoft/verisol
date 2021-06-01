# Install VeriSol

- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106)

The following are dynamically installed at VeriSol runtime (the first time only) [Solidity compiler](https://github.com/ethereum/solidity/releases/tag/v0.5.10) (for Windows and Linux only), [Z3 theorem prover](https://github.com/Z3Prover/z3/releases), [Corral](https://github.com/boogie-org/corral) and [Boogie](https://github.com/boogie-org/boogie) verifiers, the latter two are installed as dotnet cli tools.

- Follow either **Nuget** package installation directly, or after building the **sources**.

### Install from sources

Perform the following set of commands:
```
git clone https://github.com/utopia-group/verisol.git
```

From %VERISOL_PATH%  (the root folder of the repository), perform

```
dotnet build Sources\VeriSol.sln
```

Install to the **global** dotnet CLI tools cache so that you can run command  `VeriSol` from anywhere:
```
dotnet tool install VeriSol --version 0.1.1-alpha --global --add-source %VERISOL_PATH%/nupkg/
```
You may need to uninstall a previous version first. Use `dotnet tool uninstall --global VeriSol` first then. 

**For VeriSol Developers** One can use VeriSol without performing the install steps directly as a dotnet Core dll. Run the following command instead `dotnet %VERISOL_PATH%/bin/Debug/VeriSol.dll`.

### Special instruction for MacOS/OSx (for both installation)
- Follow the instructions [here](https://solidity.readthedocs.io/en/v0.5.11/installing-solidity.html). Perform the following commands using [HomeBrew](http://brew.sh/) installer system: `brew update & brew upgrade & brew tap ethereum/ethereum & brew install https://raw.githubusercontent.com/ethereum/homebrew-ethereum/7fa7027f20cca27f76c679d0c5b35ee3c565f284/solidity.rb`. Homebrew will install `solc` binaries in the folder `/usr/local/Cellar/solidity/<version>/bin`.

# Running VeriSol

Assuming VeriSol is in the path, run

`VeriSol`

to view options and their meanings.

A common usage:

`VeriSol foo.sol Bar`

where
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze

  > For Windows, the tool output prints instructions to step through the trace using [**ConcurrencyExplorer.exe**](https://github.com/boogie-org/corral/tree/master/tools) binary, whose sources are [here](https://github.com/LeeSanderson/Chess)

For the examples below, change directory to Test\regressions\ folder.

### Example with refutation ###
`VeriSol Error.sol AssertFalse`

### Example with verification ###
`VeriSol Mapping.sol Mapping`

### Example with Loop Invariants ###
`VeriSol LoopInvUsageExample.sol LoopFor`

### Example with Contract Invariants ###
`VeriSol ContractInvUsageExample.sol LoopFor`

### Example of higly simplified ERC20 ###
`VeriSol ERC20-simplified.sol ERC20`

### Example of higly simplified TheDAO attack ###
`VeriSol DAO-Sim-Buggy.sol Mallory`

`VeriSol DAO-Sim-Fixed.sol Mallory`

### Other examples
A few more examples with non-default options can be found [here](https://github.com/microsoft/verisol/wiki/Experimental--options-in-VeriSol).  

Here is a recommended [blog post from OpenZeppelin](https://forum.openzeppelin.com/t/formal-verification-of-erc20-implementations-with-verisol/1824) describing the use of VeriSol for formalizing functional correctness of ERC20. 

# Regression script
First, follow the installation instructions from **sources**

To run the regressions test, first install SolToBoogieTest
```
dotnet tool install --global SolToBoogieTest --version 0.1.1-alpha --add-source %VERISOL_PATH%/nupkg/
```

Then run command `VeriSolRegressionRunner`
```
VeriSolRegressionRunner %VERISOL_PATH%/Test/
```

All regressions are expected to pass.

To run a subset of examples during testing, add an optional parameter to limit the above run to a subset of tests that match a prefix string *<prefix>* (e.g. using Array will only run regresssions with Array in their prefix)

```
VeriSolRegressionRunner %VERISOL_PATH%/Test/ [<prefix>]
```




