pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint public x;
    constructor () public {x = 11;}
}

contract B {
    A a;
    function Foo() public {
        a = new A();
        uint y = a.x();
        assert(y == 11);
    }
}