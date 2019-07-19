pragma solidity >=0.4.24 <0.6.0;

contract ReturnMult {

    mapping (uint => uint) m;
    mapping (uint => bool) n;

    constructor () public {}

    function A() public returns (address)
    {
        return address(0x0);
    }

    function B() public returns (int)
    {
        return 55;
    }

    function C() public 
    {
        return;
    }
}
