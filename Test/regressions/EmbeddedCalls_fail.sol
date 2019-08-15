pragma solidity >=0.4.24 <0.6.0;
contract B {
   function funcB() public pure returns (uint) {
       return 42;
   }
   constructor() public {}
}

contract A {
   function funcA1() public pure returns (uint) {
       return 11;
   }
   function funcA2(uint x) public pure returns (uint) {
       return x+1;
   }
   function funcA3() public returns (B) {
       B retVal= new B();
       return retVal;
   }
   constructor() public
    {
		assert(funcA2(funcA1())==12);
	    assert(funcA3().funcB()!=42);
    }
}