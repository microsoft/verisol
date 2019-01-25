@echo off

REM ************ README starts ***********************************
REM A helper script to run verisol end to end on Windows
REM Usage: 
REM    Example: script.cmd poa AdminValidatorSet 8 bounded
REM    Example: script.cmd poa AdminValidatorSet 8 proof
REM    Example: script.cmd poa removeAdmin_AdminSet 8 audit
 
REM Set these variables in command line (NOT IN THIS FILE) before running 
REM set VERISOL_ROOT_DIR=d:\WestLakeContractsVeriSol\
REM ************ README ends *************************************
REM ************
REM ************ DO NOT EDIT ANY LINE BEFORE THIS LINE, IT IS JUST COMMENTS

if [%VERISOL_ROOT_DIR%] EQU [] (
    echo "Error!! VERISOL_ROOT_DIR not set! Please set it to the root of the repository (e.g. d:\WestLakeContractsVerisol\)"
    goto :exit
)

if [%4] EQU [] (
     echo "Usage!! run_verisol.cmd <Name of file without .sol> <contractname | procedure_contractname> <recursionBound> <bounded|audit|proof>"
     goto :exit
)

if EXIST out.bpl (
  copy out.bpl out-old.bpl
  del out.bpl
  del pretty.bpl
)

set BINARYDEPENDENCIES=%VERISOL_ROOT_DIR%\verisol_dependencies\

if NOT EXIST %BINARYDEPENDENCIES% (
    echo "Could not find the verisol_dependencies folder"
    goto :exit
)

@echo off
REM Generate BPL 
echo Compiling sol files and generating boogie files ....
@echo on
dotnet %VERISOL_ROOT_DIR%\Sources\SolToBoogie\bin\Debug\netcoreapp2.0\SolToBoogie.dll %1.sol %VERISOL_ROOT_DIR% out.bpl
@echo off

REM Check for syntax issues and pretty print
echo Pretty printing boogie files....
@echo on
%BINARYDEPENDENCIES%\boogie\boogie.exe  out.bpl /noVerify /doModSetAnalysis /print:pretty.bpl
@echo off

del corral_out_trace.txt

set methodprefix=
if [%4] EQU [bounded] (
    echo Running the transaction-depth-bounded verification...
    set methodprefix=CorralEntry_
    )
if [%4] EQU [audit] (
    echo Running the audit mode verification...
    )


if [%4] EQU [proof] (
    REM Run Boogie/Houdini to fully verify it
    echo Running Boogie with invariant inference...
    %BINARYDEPENDENCIES%\boogie\Boogie.exe -doModSetAnalysis -inline:assert -noinfer -contractInfer -proc:BoogieEntry_* out.bpl 
) else (
    REM Run Corral on various entrypoints
    echo Running Corral ...
    %BINARYDEPENDENCIES%\Corral\corral.exe /recursionBound:%3 /k:1 /main:%methodprefix%%2 /tryCTrace out.bpl /trackAllVars /printDataValues:1 > corral.txt
    REM %BINARYDEPENDENCIES%\Corral\corral.exe /recursionBound:%3 /k:1 /main:CorralEntry_%2 /tryCTrace out.bpl  /printDataValues:1 /track:owners_Admin /track:alreadyVotedToAdd_Admin /track:alreadyVotedToRemove_Admin /track:adminMap_AdminSet /track:isValidator_SimpleValidatorSet /track:latestChange_adminOwner_SimpleValidatorSet /track:votesFor_Admin /track:votesAgainst_Admin 
    if EXIST corral_out_trace.txt (
        REM Check the trace
        echo Running trace viewer for defect found...
        %BINARYDEPENDENCIES%\ConcurrencyExplorer\ConcurrencyExplorer.exe corral_out_trace.txt
    ) ELSE (
         echo Did not find any defect traces....
    )
)

:exit

echo Done!