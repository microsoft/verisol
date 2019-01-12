pragma solidity ^0.4.24;

// An example inspired by the virtual dispatch bug in PoA.sol and Tests.sol

contract A {
  uint x;
  
  function A() { 
       // this.x = 0; //Solidity does not support this
       this.Foo();
       assert (x == 1);
  }
  function Foo() {x = 1;}

}
