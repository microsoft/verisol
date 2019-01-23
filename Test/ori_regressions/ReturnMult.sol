pragma solidity ^0.4.24;

contract ReturnMult {

    mapping (uint => uint) m;
    mapping (uint => bool) n;

    function ReturnMult() public {}

    function A() public returns (address)
    {
        return 0x0;
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
