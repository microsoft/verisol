pragma solidity ^0.4.24;

// An example inspired by the virtual dispatch bug in PoA.sol and Tests.sol

contract A {
  uint x;
  function A() {x = 0;}
  function Foo() returns (uint) {
      return 33;
  }
  function Bar() {
      uint t = Foo(); //should invoke either A or B's Foo, but not C
  }
}

contract B is A {
  function B()  public  {
       uint t;
       t = Foo(); // should only invoke from B
       x = t; 
       assert(x == 1);
       assert (false);
  }
  
  function Foo() public returns (uint) {
       x = x + 1;
       return x;
  }

}

contract C {
   B b;

   function C(address a) 
   { 
      uint t;
      address c = a;
      b = new B();
      t = b.Foo(); //should only invoke B's method (TODO)
   }
   function Foo() public returns (uint) {
       return 77;
   }
}
