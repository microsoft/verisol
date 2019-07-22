pragma solidity >=0.4.24 <0.6.0;

contract Branch {

    function testIf(bool b) public returns (uint a) {
        if (b) {
            a = 1;
        } else {
            a = 2;
        }
        assert (a == 1 || a == 2);
    }

    function testTernary(bool b) public returns (uint a) {
        a = b ? 1 : 2;
        assert (a == 1 || a == 2);
    }

}
