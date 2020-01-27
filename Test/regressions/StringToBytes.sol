pragma solidity >=0.4.24 <0.6.0;

//VeriSol complains about "bytes" arg being called with "string" value
contract Test {
 
  //string z; 
  constructor () public {
  }
  
  function foo(bytes memory x) public 
  {
	//z = "a";
  }  

  function test() public 
  {
	 //foo("");
	 //Case #1:
	 //bytes memory a = new bytes(0);
	 //foo(a);
	 
	 //Case #2:
	 //foo(new bytes(0));      //VeriSol translation error in TranslateNewStatement
	 
	 //foo(0x);                //solc error
	 foo(bytes(""));           //works
	 //foo(0x0);                  //solc error
	 
	 //////////////////////////////////////////////////////////////
	 //bytes memory a;
	 //bytes is same as byte[]
	 //assert (a.length == 0);
	 //bytes memory b = delete a;
	 //foo(a);	 
	 //z = foo("");     //solc detects error
  }
}