pragma solidity >=0.4.24 <0.6.0;

contract Logical {

    function testEq(uint a, uint b) public {
        require (a == b);
        assert (a == b);
    }

    function testNeq(uint a, uint b) public {
        require (a != b);
        assert (a != b);
    }

    function testGt(uint a, uint b) public {
        require (a > b);
        assert (a > b);
    }

    function testGte(uint a, uint b) public {
        require (a >= b);
        assert (a >= b);
    }

    function testLt(uint a, uint b) public {
        require (a < b);
        assert (a < b);
    }

    function testLte(uint a, uint b) public {
        require (a <= b);
        assert (a <= b);
    }

    function testAnd(uint a, uint b) public {
        require (a >= 10);
        require (b >= 20);
        assert (a >= 10 && b >= 20);
    }

    function testOr(uint a) public {
        require (a <= 5 || a > 10);
        assert (a <= 5 || a > 10);
    }

    function testNeg(uint a) public {
        require (!(a == 10));
        assert (a != 10);
    }

}
