pragma solidity ^0.4.24;

contract A {

   function get_a()  public returns (uint) {
       return 2;
   }
}

contract NestedFunction {

    A a;

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

    function bar() public {
       assert(a.get_a() == 2);
    }

    function unhandled(uint x) public {
       uint y;
       y = foo(foo(x) + foo(foo(x)));
       assert (y == 2*x + 4); 
    }

}
