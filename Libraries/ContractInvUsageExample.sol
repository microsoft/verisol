pragma solidity ^0.4.24;
import "./VeriSolContracts.sol";


contract LoopFor {

    uint x;
    uint y;
    uint n0; 

    function ContractInvariant () view {
        VeriSol.ContractInvariant(x + y == n0);
    }

    // test Loop invariant with for loop
    constructor(uint n) public {
        require (n >= 0);
        n0 = n;
        y = 0;
        x = n;
    }

    function Foo() public {
        if ( x > 0 ) 
        {
           y ++;
           x --;
        }
        assert (x + y == n0);
    } 
   
    // view functions are ignored by VeriSol today
    function Bar() public view {
        assert (x + y == n0);
    }

}
