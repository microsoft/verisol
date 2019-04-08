pragma solidity ^0.4.24;

//simple library with no internal state

library Lib {
  struct A{
     uint x;
     string y;
  }  

}

contract C {
    // using Lib for uint;

    function foo() public {
        Lib.A memory aa;
        bar(aa);
        assert(aa.x == 22);
    }
   
    function bar(Lib.A xx) private {
        xx.x = 22;
    }

}
