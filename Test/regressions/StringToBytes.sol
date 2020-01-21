pragma solidity >=0.4.24 <0.6.0;

//VeriSol complains about "bytes" arg being called with "string" value
contract Test {
 
  string z; 
  constructor () public {
  }
  
  function foo(bytes memory x) public returns (bytes memory y) 
  {
     return x;
  }  

  function test() public returns (bytes memory)
  {
	 foo("");
	 //z = foo("");     //solc detects error
  }
}