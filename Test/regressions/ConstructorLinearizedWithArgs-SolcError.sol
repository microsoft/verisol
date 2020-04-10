pragma solidity >=0.4.24 <0.6.0;

// This test demonstrates that solc.exe detects
// inconsistencies in chained constructors' arguments
// This test results in solc error: see lines 13 and 21
// compare with ConstructorLinearizedWithArgs.sol, which compiles

contract A {
    uint a;
    constructor (uint x) public {
        a = x;
    }
}

contract B is A {
    uint b;
    constructor (uint x) A(x+1) public {  //solc error: "Base constructor arguments given twice" (for A)
        b = x + 1;
		assert(a == x + 1);
    }
}

contract C is A {
    uint c;
    constructor (uint x) A(x+1) public {  //solc error: "Base constructor arguments given twice" (for A)
        c = x + 2;
		assert(a == x + 2);
    }
}

contract D is B, C {
	uint d;
    constructor (uint x) B(x+3) C(x+4) public
    {	
		d = x + 3;
        //assert (a == x + 4);  //after calling B(x+3)
        //assert (b == x + 4);  //after calling B(x+3)
		assert(a == x + 6);     // after calling C(x+4)
        assert (c == x + 6);    // after calling C(x+4)
    }
}
