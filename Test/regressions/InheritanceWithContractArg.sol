pragma solidity >=0.4.24 <0.6.0;

contract A{ 
}

contract BaseContract {
    uint x;
    function Foo (A a) public { }
}

contract DerivedContract is BaseContract {

    uint b;
    function Foo(A a) public { b = 1;}
    function Bar(A a) public {this.Foo(a); assert (b == 1);}
}
