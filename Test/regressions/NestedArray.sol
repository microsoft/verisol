pragma solidity ^0.4.24;

contract B{
   function foo() returns (uint) {return 33;}
}

contract A {
   B[] a;
   constructor() public {
        B b = new B(); //need temporary 
        a.push(b);
        assert (a[0].foo() == 33);       
   }
}