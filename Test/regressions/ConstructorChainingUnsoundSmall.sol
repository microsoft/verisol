pragma solidity ^0.5.2;

contract A {
    // need a struct as a 
    //   scalar variable is explicitly initialized
    //   map variables get a new address 
    struct A {
        mapping (int => bool) x;	
    } 
    A a;

    constructor () internal {
       require(!a.x[0]);
       a.x[0] = true;
    }
}

contract B is A {
}

contract C is A, B {
    constructor()
    public {
	assert(false);
    }   
}
