## Instructions to run the experiments

* `make compile experiment=<experiment_name>`

    Compiles the Celestial source for the specified experiment and generates the Solidity and FStar versions.

* `make verify experiment=<experiment_name>`

    Verifies the generated FStar for the specified experiment.

* `make perf experiment=<experiment_name>`

    Generates a `.csv` file with gas comparisions for the specified experiment.

* `make <experiment_name>`

    Runs all the above targets for the specified experiment.

* `make all`

    Compiles, verifies and generates gas performance `.csv` file for all experiments.

### Experiments

- `asset_transfer`
- `etherdelta`
- `westlake`
- `erc20`
- `wrapped_ether`
- `multisig`
- `blind_auction`