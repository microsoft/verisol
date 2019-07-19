pragma solidity >=0.4.24 <0.6.0;

contract A {

    uint[3][2] a;
    uint[2]    b;
    
    function A() public {
        a[1][0] = 10;
        b[0] = 20;
        // Need forall i. a[i] != b
        //      forall i. M_int_ref[this][a][i] != b
        // assume forall i. Alloc(M_int_ref[this][a][i]);
        assert (a[1][0] == 10);
    }
}
