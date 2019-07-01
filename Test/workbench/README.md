This folder contains a copy of Smart contracts in Solidity and Workflow policies in JSON for the samples 
in Azure Blockchain Workbench
https://github.com/Azure-Samples/blockchain/tree/master/blockchain-workbench/application-and-smart-contract-samples

The folder structure is usually:
<Sample-Name>
   Sample.sol        // Solidity file
   Sample.json       // original JSON
   WrapperFiles\
      A__Sample_Workbench.sol // input file to VeriSol
      Sample.sol //a copy
      A__Sample_Workbench_exp.bpl //manually added specifications in Boogie file (only for PingPongGame)
   FixedVersion\    // if a bug was fixed (e.g. AssetTransfer)
      A__Sample_Workbench.sol // input file to VeriSol
      Sample.sol //fixed copy

Command to VeriSol:



   