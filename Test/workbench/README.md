This folder contains a copy of Smart contracts in Solidity and Workflow policies in JSON for the samples 
in Azure Blockchain Workbench
https://github.com/Azure-Samples/blockchain/tree/master/blockchain-workbench/application-and-smart-contract-samples
 * The version of files reflect the state of the files around August 2018 in the above repository

These files form the experiments described in the arXiv technical report:

> Formal Specification and Verification of Smart Contracts for Azure Blockchain, Yuepeng Wang, Shuvendu K. Lahiri, Shuo Chen, Rong Pan, Isil Dillig, Cody Born, Immad Naseer, https://arxiv.org/abs/1812.08829

The folder structure is usually:
- Sample-Name
   - Sample.sol        // Solidity file
   - Sample.json       // original JSON
   - WrapperFiles\
      - A__Sample_Workbench.sol // instrumented file that is input file to VeriSol
      - Sample.sol //a copy
      - A__Sample_Workbench_exp.bpl //manually added specifications in Boogie file (only for PingPongGame)
   - FixedVersion\    // if a bug was fixed (e.g. AssetTransfer)
      - A__Sample_Workbench.sol // input file to VeriSol
      - Sample.sol //fixed copy

Command to VeriSol: 

runall.cmd




   
   
