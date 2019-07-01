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
      A__Sample_Workbench_exp.sol //manually added specifications
   FixedVersion\    // if a bug was fixed
      A__Sample_Workbench.sol // input file to VeriSol
      Sample.sol //fixed copy

Command to VeriSol:



   