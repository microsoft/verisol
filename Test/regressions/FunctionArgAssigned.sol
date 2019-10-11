pragma solidity >=0.4.24 <0.6.0;

// corral complains: .bpl does not typecheck
contract Test {
  
  int z;
  uint8 a8;
  
  constructor () public {
  }
  
  function test(uint8 x) public {
	 x = x + 255;
	 a8 = x;
	 require(a8 >= 256);      
     assert(false);         
  }
}