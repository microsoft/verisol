pragma solidity ^0.4.24;

contract Consts {

    function Consts() public {
        uint a;
        address b;
        address c;

        a = 10;
        b = 0x10;
        c = 0x12;

        assert(b != c);
    }
}
