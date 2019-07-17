@echo off

REM
REM A windows CMD script just to show the syntax of commands for Workbench examples
REM

if [%VERISOL_ROOT_DIR%] EQU [] (
    echo "Error!! VERISOL_ROOT_DIR not set! Please set it to the root of the repository (e.g. d:\verisol\)"
    goto :exit
)

REM Finds a trace or has ill-formed bpls
REM AssetTransfer: Bug
REM BazaarItemListing: constructor bug with 2 contracts
REM DigitalLocker: Bug
REM RoomThermostat: start state is first element of a enum type (default, but good warning)
FOR %%G in (AssetTransfer, BazaarItemListing,  DigitalLocker, RoomThermostat, PingPongGame) DO (
   echo ****************************
   echo **** %%G:Buggy  ****
   echo ****************************
   pushd %%G\WrapperFiles\
   dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
   popd
)

REM Finds a proof without changes
FOR %%G in (BasicProvenance, FrequentFlyerRewardsCalculator, DefectiveComponentCounter, HelloBlockchain, RefrigeratedTransportation, SimpleMarketplace) DO (
   echo ****************************
   echo **** %%G:Correct  ****
   echo ****************************
   pushd %%G\WrapperFiles\
   dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
   popd
)

REM Finds a proof for bug-fixed versions
REM TODO: Add fixed examples for  BazaarItemListing, DigitalLocker, RoomThermostat
FOR %%G in (AssetTransfer) DO (
  echo ****************************
  echo **** %%G:Fixed ****
  echo ****************************
  pushd %%G\FixedVersion\
  dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
  popd
)

REM Manual proofs using Boogie directly
REM Search for "//manually added" in BPL file
FOR %%G in (PingPongGame) DO (
  echo ****************************
  echo **** %%G:Manual proof ****
  echo ****************************
  pushd %%G\WrapperFiles\
  %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\Boogie\Boogie.exe -doModSetAnalysis -inline:spec -noinfer -proc:BoogieEntry_Starter_Azure* A__PingPongGame_Workbench_exp.bpl  
  REM Don't use contractInfer
  REM Smoke test: to see a counterexample
  REM 1. replace -inline:spec with -inline:assert //fails as there is a recursive call > depth 1
  REM 2. comment the ensures 
  popd
)


