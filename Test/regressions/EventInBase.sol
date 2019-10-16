pragma solidity >=0.4.24 <0.6.0;

contract C {
   event FF();
}

contract A is C{
    event EE();
}

contract B is A {
   function Foo() public {
       emit EE();
       emit FF();
   }
}