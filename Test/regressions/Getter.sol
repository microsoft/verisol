pragma solidity >=0.4.24 <0.6.0;

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