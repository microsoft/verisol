pragma solidity >=0.4.24 <0.6.0;

contract ArrayNestedFixedSize {

    // NOTE: the number of columns comes first in the declaration!
    uint[3][2] a;
    bool[2][2] aa;

    constructor() {
        assert (a[0][1] == 0);
        assert (!aa[0][1]);
    }

    function test() public {
        uint[3] b;
        a[0][0] = 0;
        a[0][1] = 1;
        uint x = a[0][0]; //print
        a[0][2] = 2;
        a[1][0] = 3; //may alias with a[0][0]
        x = a[0][0]; //print
        a[1][1] = 4;
        a[1][2] = 5;
        b[0] = 5;    //may alias with a[1][0] or a[0][0]
        x = a[0][0];  //print
        assert (a[0][0] == 0 || a[0][0] == 3);
        assert (a[0][1] == 1);
        assert (a[0][2] == 2);
        assert (a[1][0] == 3);
        assert (a[1][1] == 4);
        assert (a[1][2] == 5);

    }

}
