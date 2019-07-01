Instructions to run demo:
-------------------------
https://github.com/Azure-Samples/blockchain/tree/master/blockchain-workbench/application-and-smart-contract-samples/asset-transfer
- pushd %verisol_root%\scripts
- setup_windows.cmd
- popd
- pushd WrapperFiles
- %verisol_root_dir%\scripts\run_verisol_win.cmd A__AssetTransfer_Workbench AssetTransfer_AzureBlockchainWorkBench 6 bounded
- popd
- push FixedVesion
- %verisol_root_dir%\scripts\run_verisol_win.cmd A__AssetTransfer_Workbench AssetTransfer_AzureBlockchainWorkBench 6 bounded
- %verisol_root_dir%\scripts\run_verisol_win.cmd A__AssetTransfer_Workbench AssetTransfer_AzureBlockchainWorkBench 0 proof
- popd
