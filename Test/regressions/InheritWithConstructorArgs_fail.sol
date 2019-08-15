pragma solidity >=0.4.24 <0.6.0;

contract Base {
    uint x;
    constructor(uint _x) public { x = _x; }
}

contract Derived1 is Base(7) {
    constructor() public {
        assert (x != 7); //handled correctly
    }
}

contract Derived2 is Base {
    constructor(uint _y, uint _z) Base(_y + _y) public {
       assert (x != 2*_y); //correctly handled
    }
}