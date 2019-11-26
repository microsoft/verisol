pragma solidity >=0.4.24 <0.6.0;

// Tests return value of a function in an expression, with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  int z;
  int u;
  uint8 a8;
  uint8 b8;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
     require(x > 256 - 2);     
	 a8 = x;
	 b8 = 0;
	 a8 = addition(a8+1, b8+2);
	 //a8 = addition(a8+1, b8+500);
     require(a8 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
  
  function addition(uint8 _a, uint8 _b) public pure returns (uint8 c) {
        c = _a + _b;
        assert (c == _a + _b);
    }
}