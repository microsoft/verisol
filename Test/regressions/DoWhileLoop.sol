pragma solidity >=0.4.24 <0.6.0;

contract DoWhileLoop {

    function testDoWhileLoop() public {
        uint i = 1;
        uint sum = 0;
        do {
            sum += i;
            ++i;
        } while (i < 1);
        assert (sum == 1);
    }

}
