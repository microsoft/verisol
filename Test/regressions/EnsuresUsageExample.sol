pragma solidity >=0.4.24<0.6.0;
import "./Libraries/VeriSolContracts.sol";


contract A {

    int x;
    int y;
    int n;


    constructor(int a) public {
        require (a >= 0);
        n = a;
        x = n;
        y = 0;
        foo();
        assert (x == 0);
        assert(x + y == n);
    }
 
    function foo() private {
        VeriSol.Ensures(x + y == n);
        VeriSol.Ensures(y == n);
        if (y < n) {
           x--;
           y++;
           foo();
        }
        baz();
    }
    
    function baz() private {
        // assert (false);
    }

    function bar() private {
      assert (false); //unreachable from BoogieEntry_*
      //however with /contractInfer there is a failure in the command line
    }
}
