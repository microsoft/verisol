pragma solidity ^0.4.24;

contract ArrayInit {

    uint[2] a;
    uint[2] b;
    
    function ArrayInit() public {
       b[1] = 22;
       a[1] = 33;
       assert (b[1] == 22);
    }
}
