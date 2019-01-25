pragma solidity ^0.4.24;

contract A{ 
    function G() {} //until empty contract bug is fixed. 
}

contract BaseContract {
    uint x;
    function Foo (A a) { }
}

contract DerivedContract is BaseContract {

    uint b;
    function Foo(A a) { b = 1;}
    function Bar(A a) {this.Foo(a);}
}
