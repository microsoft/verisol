pragma solidity ^0.4.24;

contract NestedFunction {

    function NestedFunction() {
    } 

    function foo(uint x) internal returns (uint ret) {
        ret = x + 1;
    }

    function far(uint x) public {
        assert(foo(x) == x + 1);
    }

    function baz(uint x) public {
       uint y;
       y = foo(foo(x) + 2);
       assert (y == x + 4);
    }

    function unhandled(uint x) public {
       uint y;
       y = foo(foo(x) + foo(foo(x)));
       // assert (y == 2*x + 4); // cannot handle multiple levels of nesting
    }

}
