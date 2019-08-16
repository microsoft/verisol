pragma solidity >=0.4.24 <0.6.0;

contract A{ 
    //function G() public {} //until empty contract bug is fixed. 
}

contract BaseContract {
    uint b;
	uint x;
    function Foo (A a) public { b == 0; }
}

contract DerivedContract is BaseContract {

    uint b;
    function Foo(A a) public { b = 1;}
    function Bar(A a) public {this.Foo(a); assert (b == 0);}
}
