pragma solidity ^0.4.24;
//import "./../../Libraries/VeriSolContracts.sol";
import "./VeriSolContracts.sol";

contract LoopFor {

    uint[2] a;

    constructor(uint n) public {
        require (n >= 0);
        uint y = 0;
        for (uint x = n; x != 0; x --) {
            VeriSol.Invariant(x + y == n);
            y++;
        }
        assert (y == n);
    }
}
