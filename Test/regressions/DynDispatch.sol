pragma solidity >=0.4.24 <0.6.0;

// An example inspired by the virtual dispatch bug in PoA.sol and Tests.sol

contract A {
  uint x;
  constructor () public {x = 0;}
  function Foo() public returns (uint) {
      return 33;
  }
  function Bar() public {
      uint t = Foo(); //should invoke either A or B's Foo, but not C
  }
}

contract B is A {
  constructor ()  public  {
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

   constructor (address a) public
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
