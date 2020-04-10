pragma solidity >=0.4.24 <0.6.0;

// This test demonstrates constructor chaining with arguments
// with non-trivial dependencies between the arguments
// ctor A is only called once

// Example of the trace:
// C(a), where a is 119
// B(2*a, 3*a) => B(238, 357)
// A(a+b) => A(5*a) => A(595)
// ctor A {x = a} => x = 595
// ctor B
// ctor C: a = 119, x = 595

contract A {
    uint x;
    constructor (uint a) public {x = a;}
}

contract B is A {
    constructor (uint a, uint b) public A(a + b) {}
}

contract C is A, B {
    constructor (uint a) B(2*a, 3*a) public {assert (x == 5*a);}
}
