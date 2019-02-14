pragma solidity ^0.4.24;

contract A {
    uint a=1;
    uint b;
    bool c=true;
    constructor() public
    {
		assert(a==1);
		assert(c);
    }
}