pragma solidity >=0.4.24 <0.6.0;

// Test uint implementation
contract UintTest {
  uint x;
  uint256 x256;
  uint16 x16;
  uint16 y16;
  uint8 x8;
  uint8 y8;
  uint8 z8;
  uint y;
  uint256 y256;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
	 z8 = uint8(256) - 2;
	 require(x > z8);
     //require(x > 256 - 2);    // 256 = 2 ^ 8;   

	 if (!foo(fint(x)))
	 {
		y8 = x + 3;              //						            
		require(y8 >= x);        //fails with mod arithm; holds otherwise (checked) 
		assert (false);          //not reachable with mod arithm; reachable otherwise (checked)
	 }
	 //else if (
	 
	 assert(false);
  }
  
  function foo(uint8 x) internal returns (bool) {
        if (x == 1) {
            return true;
        }
		else
		    return false;
   }
   
   function fint(uint8 x) internal returns (uint8) {
		return x;
   }
   
   //function fbool(bool x) internal returns (u) {
           
   //}
}