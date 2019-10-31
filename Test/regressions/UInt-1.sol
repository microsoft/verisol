pragma solidity >=0.4.24 <0.6.0;

// Test uint implementation
contract UintTest {
  uint x;
  uint256 x256;
  uint16 x16;
  uint16 y16;
  uint8 x8;
  uint8 y8;
  uint y;
  uint256 y256;
  event printVal(uint x);
  //uint[] arrayUint;
  //mapping (int => uint) mapUint;
  //uint[][] nestedArrayUint;

  constructor () public {
  }
  
  function test(uint16 x1, uint16 y1) public {
      x16 = 1;
      y16 = 5;
      //x = x1 << 2;          // Verisol: unknown binary operator
      
      x16 = -y16;               // Solidity: allowed
								// NEG is used as the operation for rhs translation into Boogie; assume for non-negative result added
      assert(x16 == 65531);     // holds
      
      x = -x;                 // Solidity: allowed
	  
      //x16 = -x;               // Solidity: not allowed (size issue); Verisol reports same error
      //x16 = x;
	  //x16 = int16(x);          // Solidity error
	  x16 = uint16(x);
      x256 = x16;             // Solidity: implicit cast
      //x = +x;               // Solidity: not allowed; Verisol reports same error
      assert (x256 == x1);    // holds
      
      x8 = 1;
      y8 = 3;
      //x8 = x8 & y8;			// Solidity: allowed; Verisol: unknown binary operator
  }

  function testNonNegative() public
  {
      x--;
      // should not fail
      if(x<0)
        assert(false);
  }
}
