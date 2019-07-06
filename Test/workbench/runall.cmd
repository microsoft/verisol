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
REM DefectiveComponentCounter: ill-formed-bpl due to corral_print
REM DigitalLocker: Bug
REM RoomThermostat: start state is first element of a enum type (default, but good warning)
FOR %%G in (AssetTransfer, BazaarItemListing, DefectiveComponentCounter, DigitalLocker, RoomThermostat) DO (
   echo ****************************
   echo **** %%G  ****
   echo ****************************
   pushd %%G\WrapperFiles\
   dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
   popd
)

REM Finds a proof without changes
FOR %%G in (BasicProvenance, FrequentFlyerRewardsCalculator, HelloBlockchain, RefrigeratedTransportation, SimpleMarketplace) DO (
   echo ****************************
   echo **** %%G  ****
   echo ****************************
   pushd %%G\WrapperFiles\
   dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
   popd
)

REM Finds a proof for bug-fixed versions
REM Missing fixed examples for  BazaarItemListing, DigitalLocker, RoomThermostat
FOR %%G in (AssetTransfer) DO (
  echo ****************************
  echo **** %%G-fixed ****
  echo ****************************
  pushd %%G\FixedVersion\
  dotnet %VERISOL_ROOT_DIR%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll A__%%G_Workbench.sol %%G_AzureBlockchainWorkBench /tryProof /tryRefutation:6
  popd
)


