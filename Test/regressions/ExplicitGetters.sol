pragma solidity >=0.4.24 <0.6.0;

// translation of explicit getter function for public state variables

contract A {

    uint public x;
    
    function x() public view returns (uint) {require(x > 0); return x;}
}

contract B {

    uint public y;
    
    constructor(A a) {y = a.x(); assert (y > 0); }
}
