pragma solidity >=0.4.24 <0.6.0;

contract BaseContract {
  mapping (uint => uint) A;
  uint[2] B;
  uint[] C;  


  constructor() public {
       A[1] = 1; 
       B[0] = 2; //B.length == 2
       C.push(3); 
  }

}

contract DerivedContract is BaseContract {
  
  uint[4] D;
  mapping (uint => uint) E;

  //derived class takes an extra parameter
  constructor(uint x) public {
       assert(B.length == 2);
       assert(C.length == 1);
       assert(D.length == 4);
       assert(A[1] == 1);
       assert (false); //should be reachable
  }      
}
