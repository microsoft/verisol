pragma solidity >=0.4.24 <0.6.0;

contract Test {
  
  int z;
  uint8 a8;
  
  constructor () public {
  }
  
  function foo(uint x) public returns (uint y) 
  {
     return x;
  }  


  function test(uint8, address) public returns (uint8)
  {
     revert("");
     return 1;
  }
}