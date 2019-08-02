pragma solidity >=0.4.24<0.6.0;
import "./Libraries/VeriSolContracts.sol";


contract LoopFor {

    int x;
    int y;

    function ContractInvariant () private view {
        VeriSol.ContractInvariant(x == y);
        VeriSol.ContractInvariant(y >= 0);
    }

    // test Loop invariant with for loop
    constructor(int n) public {
        require (n >= 0);
        x = n;
        y = x;
    }

    function Foo() public {
        if ( x > 0 ) 
        {
           x--;
           y--;
        }
        assert (y >= 0); 
    } 
}
