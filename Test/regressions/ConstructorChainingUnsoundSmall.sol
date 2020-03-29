pragma solidity ^0.5.2;

contract A {
    bool x;	
    constructor () internal {
       require(!x);
       x = true;
    }
}

contract B is A {
    constructor () internal {}
}

contract C is A, B {
    constructor()
    public {
	assert(false);
    }   
}
