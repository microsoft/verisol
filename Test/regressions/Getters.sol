pragma solidity ^0.4.24;

contract A {
    uint public x;
    constructor () {x = 11;}
}

contract B {
    A a;
    function Foo() public {
        a = new A();
        uint y = a.x();
        assert(y == 11);
    }
}