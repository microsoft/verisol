pragma solidity ^0.4.24;
import "./../../Libraries/VeriSolContracts.sol";
//import "./VeriSolContracts.sol";
//import "github.com/microsoft/verisol/blob/master/Libraries/VeriSolContracts.sol";

contract LoopFor {

    // test Loop invariant with for loop
    constructor(uint n) public {
        require (n >= 0);
        uint y = 0;
        for (uint x = n; x != 0; x --) {
            VeriSol.Invariant(x + y == n);
            y++;
        }
        assert (y == n);
    }

    // test Loop invariant with while loop
    function Foo(uint n) public {
        require (n >= 0);
        uint y = 0;
        uint x = n;
        while (x != 0) {
            VeriSol.Invariant(x + y == n);
            y++;
            x--;
        }
        assert (y == n);
    }

    // test Loop invariant with do-while loop    
    function Bar(uint n) public {
        require (n > 0);
        uint y = 0;
        uint x = n;
        do {
            VeriSol.Invariant(x + y == n);
            y++;
            x--;
        } while (x != 0);
        assert (y == n);
    }


}
