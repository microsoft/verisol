pragma solidity >=0.4.24 <0.6.0;

// This test passes, but assertion on line 35 is only proved
// up to 4 transactions
// This result means that ctor A is only called once, for B, as A(x+1)
// Compare this test with ConstructorChaining2_fail.sol: no ctor args there,
// so the workaround in the compiler doesn't work, and the base ctor is called twice 


// Example of the trace:
// D(x) is called, where x is 716
// B(x+3) called, where x is 716, B's arg is 719
// A(x+1) called, where x is 720. A's arg is 720
// ctor A {a = x} , where a is 720
// ctor B  {b = x+1 } , where x is 719, b is 720
// C(x+4) is called, where x is 716, C's arg is 720
// ctor C   { c = x + 2}, where x is 720, c is 722

contract A {
    uint a;
    constructor (uint x) public {
        a = x;
    }
}

contract B is A {
    uint b;
    constructor (uint x) A(x+1) public {  
        b = x + 1;
		assert(a == x + 1);
    }
}

contract C is A {
    uint c;
    //constructor (uint x) A(x+2) public {          // no A with ANY args here, otherwise, solc error:
													// "Base constr args given twice"
	constructor (uint x) public {
        c = x + 2;
		assert(a == x);       // passes, but proved to only 4 transactions	
    }
}

contract D is B, C {
    constructor (uint x) B(x+3) C(x+4) public
    {	
        assert (a == x + 4);  //after calling B(x+3); calling C(x+4) does not affect this assert
        assert (b == x + 4);  //after calling B(x+3)
        assert (c == x + 6);    // after calling C(x+4)
    }
}
