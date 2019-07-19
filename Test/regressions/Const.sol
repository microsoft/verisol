pragma solidity >=0.4.24 <0.6.0;

contract Consts {

    constructor () public {
        uint a;
        address b;
        address c;

        a = 10;
        b = address(0x10);
        c = address(0x12);

        assert(b != c);
    }
}
