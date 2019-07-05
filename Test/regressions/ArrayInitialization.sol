pragma solidity >=0.4.24 <0.6.0;

contract ArrayInit {

    uint[2] a;
    uint[2] b;
    
    constructor() public {
       b[1] = 22;
       a[1] = 33;
       assert (b[1] == 22);
    }
}
