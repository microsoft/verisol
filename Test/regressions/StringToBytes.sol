pragma solidity >=0.4.24 <0.6.0;

//VeriSol complains about "bytes" arg being called with "string" value (uncomment line 18)
//VeriSol translation error when "bytes" argument is called with new bytes(0)" (uncomment line 23)
contract Test {
 
  bytes z; 
  uint[2] a;
  uint[2] b;
  constructor () public {
  }
  
  function foo(bytes memory x) public returns (bytes memory y)
  {
	y = x;
  }  

  function test() public 
  {
	 //foo("");           //VeriSol translation error
	 //Case #1:
	 //bytes memory a = new bytes(0);
	 //foo(a); 
	 //Case #2:
	 //foo(new bytes(0));      //VeriSol translation error in TranslateNewStatement
	 
	 //foo(0x);                //solc error
	 //foo(0x0);               //solc error
	 
	 z = foo(bytes(""));           //compiles
	 //assert (z[0] == 0x0);
	 //assert (z == bytes(""));      //solc error
	 //assert (z == 0x0);             //solc error
	 
	 //assert (z.length == 0);       //solc compiles, but corral crashes:
									//"Invalid type for argument 0 in map select: int (expected: Ref)"
	 //bytes1 a = z[0];               //same corral crash as above
  }
}