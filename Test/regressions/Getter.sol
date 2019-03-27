pragma solidity ^0.4.24;

contract A {
    address public x;
}

contract B {
    A a;
    function Foo() public {
        a = new A();
        address y = a.x();
    }
}