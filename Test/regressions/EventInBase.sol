pragma solidity >=0.4.24 <0.6.0;

contract A {
    event EE();
}

contract B is A {
   function Foo() public {
       emit EE();
   }
}