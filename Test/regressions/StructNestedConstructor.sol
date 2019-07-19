pragma solidity >=0.4.24 <0.6.0;

//nested struct constructors

contract A {
    uint xx;
    constructor() public {
       xx = 55;
    }
    function XX() public view returns (uint) {
       return xx;
    }

}

contract StructType {
    struct S {
        uint x;
        uint y;
        string z;
    }

    S[] ss;
    A[] a;

   constructor() public {
       ss.push(S(uint(1), uint(2),"aa")); //nested struct constructor
       assert(ss[0].y == 2);
       
       a.push(new A()); //nested contract constructor
       A aa = a[0]; 
       assert(aa.XX() == 55); 
       // assert(a[0].XX() == 55); //nested calls on arrays don't work, issue #74
    }
}
